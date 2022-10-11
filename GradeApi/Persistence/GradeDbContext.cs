namespace GradeApi.Persistence
{
    using GradeApi.Persistence.Entitites;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;

    public partial class GradeDbContext : DbContext
    {
        public virtual DbSet<Grade> Grades { get; set; } = null!;

        public virtual DbSet<Course> Courses { get; set; } = null!;

        public virtual DbSet<Student> Students { get; set; } = null!;

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual DbSet<StudentAddress> StudentAddress { get; set; } = null!;

        public GradeDbContext()
        {
        }

        public GradeDbContext(DbContextOptions<GradeDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSnakeCaseNamingConvention();

            // https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
            optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(grade => grade.Courses)
                    .WithOne(course => course.Grade)
                    .HasForeignKey(student => student.GradeId);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            builder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.AddressId, "IX_Students_AddressId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("Nombre y apellidos");

                entity.HasOne(student => student.Address)
                    .WithOne(sa => sa.Student)
                    .HasForeignKey<StudentAddress>(sa => sa.StudentId);

                entity.HasMany(e => e.StudentCourses)
                    .WithOne(sc => sc.Student)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity.HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId);

                entity.HasOne(sc => sc.Course)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);
            });

            builder.Entity<StudentAddress>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Address1).IsRequired();

                entity.Property(e => e.Address2).IsRequired(false); // Not needed

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.HasCheckConstraint("CK_StudentAddress_ZipCode", "[zip_code] > 0");
            });
        }
    }
}
