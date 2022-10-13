namespace GradeApi.Persistence
{
    using System;
    using System.Collections.Generic;
    using GradeApi.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public partial class GradeDbContext : DbContext
    {
        public GradeDbContext()
        {
        }

        public GradeDbContext(DbContextOptions<GradeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.HasIndex(e => e.GradeId, "ix_courses_grade_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.GradeId).HasColumnName("grade_id");

                entity.Property(e => e.Mandatory).HasColumnName("mandatory");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("fk_courses_grades_grade_id");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("grades");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Section).HasColumnName("section");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");

                entity.HasIndex(e => e.AddressId, "ix_students_address_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name")
                    .HasComment("Nombre y apellidos");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("fk_student_courses_courses_course_id"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_student_courses_students_student_id"),
                        j =>
                        {
                            j.HasKey("StudentId", "CourseId").HasName("pk_student_courses");

                            j.ToTable("student_courses");

                            j.HasIndex(new[] { "CourseId" }, "ix_student_courses_course_id");

                            j.IndexerProperty<int>("StudentId").HasColumnName("student_id");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        });
            });

            modelBuilder.Entity<StudentAddress>(entity =>
            {
                entity.ToTable("student_address");

                entity.HasIndex(e => e.StudentId, "ix_student_address_student_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1");

                entity.Property(e => e.Address2).HasColumnName("address2");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.ZipCode).HasColumnName("zip_code");

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentAddress)
                    .HasForeignKey<StudentAddress>(d => d.StudentId)
                    .HasConstraintName("fk_student_address_students_student_id1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
