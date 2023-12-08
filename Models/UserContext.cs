using Microsoft.EntityFrameworkCore;

namespace FinalProject{
    public class UserDbContext : DbContext{
        public UserDbContext (DbContextOptions<UserDbContext> options)
            :base(options)
        {
        }


        public DbSet<User> Users {get;set;} = default!;
        public DbSet<Workouts> Workouts {get;set;} = default!;
        public DbSet<MuscleGroup> MuscleGroups {get;set;} = default!;
        public DbSet<ScheduledLift> ScheduledLifts {get;set;} = default!;
        public DbSet<Maxes> Maxes {get;set;} = default!;
        public DbSet<Goals> Goals {get;set;} = default!;
        public DbSet<Difficulty> Difficulties {get;set;} = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Goals>()
                .HasOne(g => g.User) // Each Goal has one User
                .WithMany(u => u.Goals) // Each User can have many Goals
                .HasForeignKey(g => g.UserID); // Foreign key in Goals referencing User's UserID
            modelBuilder.Entity<WorkoutMuscleGroup>()
                .HasKey(wmg => new { wmg.WorkoutID, wmg.MuscleID });

            modelBuilder.Entity<WorkoutDifficulty>()
                .HasKey(wd => new { wd.WorkoutID, wd.DiffID });

            // Define relationships
            modelBuilder.Entity<WorkoutMuscleGroup>()
                .HasOne(wmg => wmg.Workout)
                .WithMany(w => w.WorkoutMuscleGroups)
                .HasForeignKey(wmg => wmg.WorkoutID);

            modelBuilder.Entity<WorkoutMuscleGroup>()
                .HasOne(wmg => wmg.MuscleGroup)
                .WithMany(mg => mg.WorkoutMuscleGroups)
                .HasForeignKey(wmg => wmg.MuscleID);

            modelBuilder.Entity<WorkoutDifficulty>()
                .HasOne(wd => wd.Workout)
                .WithMany(w => w.WorkoutDifficulties)
                .HasForeignKey(wd => wd.WorkoutID);

            modelBuilder.Entity<WorkoutDifficulty>()
                .HasOne(wd => wd.Difficulty)
                .WithMany(d => d.WorkoutDifficulties)
                .HasForeignKey(wd => wd.DiffID);

            modelBuilder.Entity<ScheduledLift>()
                .HasKey(sl => new { sl.UserID, sl.WorkoutID });

            base.OnModelCreating(modelBuilder);
        }
        
    }   
}