using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<VariationExercise>()
                .HasOne(ve => ve.WorkoutVariation)
                .WithMany(v => v.VariationExercises)
                .HasForeignKey(ve => ve.WorkoutVariationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VariationExercise>()
                .HasOne(ve => ve.Exercise)
                .WithMany(e => e.VariationExercises)
                .HasForeignKey(ve => ve.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.TokenHash)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutVariation> WorkoutVariations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionExercise> SessionExercises { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<VariationExercise> VariationExercises { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
