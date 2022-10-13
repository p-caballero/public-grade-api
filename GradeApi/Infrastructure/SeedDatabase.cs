namespace GradeApi.Infrastructure
{
    using GradeApi.Persistence;
    using GradeApi.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    public static class SeedDatabase
    {
        private static readonly JsonSerializerOptions seedOptions = new(JsonSerializerDefaults.Web)
        {
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = false
        };

        public static void Seed(GradeDbContext  dbContext)
        {
            var transaction = dbContext.Database.BeginTransaction();

            try
            {
                AddProcedure(dbContext);
                ApplySeed(dbContext);
                transaction.Commit();
            } catch(Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private static void AddProcedure(GradeDbContext dbContext)
        {
            const string Query = @"
CREATE PROCEDURE [dbo].[sp_get_students_by_course_name] 
	@CourseName NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT s.[id], s.[name]
    FROM [dbo].[students] s
	INNER JOIN [dbo].[student_courses] sc ON s.[id] = sc.[student_id]
	INNER JOIN [dbo].[courses] c ON c.[id] = sc.[course_id]
	WHERE c.[name] = @CourseName
	ORDER BY s.[name];
END";
            dbContext.Database.ExecuteSqlRaw(Query);
        }

        private static void ApplySeed(GradeDbContext dbContext)
        {
            dbContext.Grades.AddRange(LoadSeedFromJson<Grade[]>("grades.json"));
            dbContext.SaveChanges();

            var allStudents = LoadSeedFromJson<Student[]>("students.json");
            dbContext.Students.AddRange(allStudents);

            var gradeIds = dbContext.Grades.Select(x => x.Id).ToList();
            int groupSize = allStudents.Length / gradeIds.Count;

            for(int gradeIndex = 0; gradeIndex < gradeIds.Count; gradeIndex++)
            {
                int gradeId = gradeIds[gradeIndex];

                foreach(var student in allStudents.Skip(gradeIndex * groupSize).Take(groupSize))
                {
                    foreach(var course in dbContext.Courses.Where(x => x.GradeId == gradeId))
                    {
                        dbContext.StudentCourses.Add(new StudentCourse()
                        {
                            Student = student,
                            Course = course
                        });
                    }
                }
            }

            dbContext.SaveChanges();
        }

        private static TResult LoadSeedFromJson<TResult>(string fileName)
        {
            using var stream = new FileStream(path: Path.Combine("Infrastructure", "Seeds", fileName), mode: FileMode.Open, access: FileAccess.Read);
            
            var result = JsonSerializer.Deserialize<TResult>(stream, seedOptions);

            return result;
        }
    }
}
