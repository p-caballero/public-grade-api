namespace GradeApi.Infrastructure
{
    using GradeApi.Persistence;
    using GradeApi.Persistence.Entitites;
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
                ApplySeed(dbContext);
                transaction.Commit();
            } catch(Exception)
            {
                transaction.Rollback();
                throw;
            }
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
