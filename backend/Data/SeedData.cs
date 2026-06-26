using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Data
{
    public static class SeedData
    {
        // Test credentials: email = test@workout.com / password = Test1234
        public static async Task SeedAsync(AppDbContext context)
        {
            // ── Block 1: Always seed exercises if the table is empty ──────────────
            if (!await context.Exercises.AnyAsync())
            {
                context.Exercises.AddRange(
                    // Chest
                    new Exercise
                    {
                        Name = "Bench Press", MuscleGroup = "Chest",
                        Description = "A compound pushing movement on a flat bench targeting the pectorals, anterior deltoids, and triceps."
                    },
                    new Exercise
                    {
                        Name = "Incline Bench Press", MuscleGroup = "Chest",
                        Description = "A pressing movement on an inclined bench (30–45°) that emphasises the upper chest and anterior deltoids."
                    },
                    new Exercise
                    {
                        Name = "Decline Bench Press", MuscleGroup = "Chest",
                        Description = "A pressing movement on a declined bench that places more emphasis on the lower chest."
                    },
                    new Exercise
                    {
                        Name = "Dumbbell Fly", MuscleGroup = "Chest",
                        Description = "An isolation movement using dumbbells in a wide arc to stretch and contract the pectoral muscles."
                    },
                    new Exercise
                    {
                        Name = "Cable Crossover", MuscleGroup = "Chest",
                        Description = "A cable-based fly that keeps constant tension on the chest throughout the full range of motion."
                    },
                    new Exercise
                    {
                        Name = "Chest Dip", MuscleGroup = "Chest",
                        Description = "A bodyweight pushing exercise performed with a forward lean to target the lower chest and triceps."
                    },
                    new Exercise
                    {
                        Name = "Push-Up", MuscleGroup = "Chest",
                        Description = "A fundamental bodyweight exercise targeting the chest, shoulders, and triceps by pressing against the floor."
                    },
                    // Back
                    new Exercise
                    {
                        Name = "Deadlift", MuscleGroup = "Back",
                        Description = "A foundational compound lift targeting the entire posterior chain: glutes, hamstrings, spinal erectors, and upper back."
                    },
                    new Exercise
                    {
                        Name = "Pull-Up", MuscleGroup = "Back",
                        Description = "A bodyweight vertical pulling exercise targeting the latissimus dorsi, biceps, and upper back."
                    },
                    new Exercise
                    {
                        Name = "Bent-Over Row", MuscleGroup = "Back",
                        Description = "A horizontal pulling compound movement with the torso hinged forward, targeting the lats, rhomboids, and rear deltoids."
                    },
                    new Exercise
                    {
                        Name = "Lat Pulldown", MuscleGroup = "Back",
                        Description = "A cable machine exercise that mimics the pull-up, targeting the latissimus dorsi and biceps."
                    },
                    new Exercise
                    {
                        Name = "Seated Cable Row", MuscleGroup = "Back",
                        Description = "A horizontal pulling cable movement targeting the mid-back, rhomboids, and lats."
                    },
                    new Exercise
                    {
                        Name = "T-Bar Row", MuscleGroup = "Back",
                        Description = "A compound rowing movement with a barbell loaded on one end, targeting the mid and lower back."
                    },
                    new Exercise
                    {
                        Name = "Single-Arm Dumbbell Row", MuscleGroup = "Back",
                        Description = "A unilateral rowing exercise targeting the lats and upper back while reducing lower back strain."
                    },
                    // Shoulders
                    new Exercise
                    {
                        Name = "Overhead Press", MuscleGroup = "Shoulders",
                        Description = "A compound pressing movement overhead targeting the deltoids, upper traps, and triceps."
                    },
                    new Exercise
                    {
                        Name = "Lateral Raise", MuscleGroup = "Shoulders",
                        Description = "An isolation exercise using dumbbells raised to the sides to target the medial (side) deltoid head."
                    },
                    new Exercise
                    {
                        Name = "Front Raise", MuscleGroup = "Shoulders",
                        Description = "An isolation exercise targeting the anterior (front) deltoid by raising weights directly in front of the body."
                    },
                    new Exercise
                    {
                        Name = "Rear Delt Fly", MuscleGroup = "Shoulders",
                        Description = "An isolation exercise targeting the posterior (rear) deltoid, performed bent-over or on a reverse pec-deck."
                    },
                    new Exercise
                    {
                        Name = "Arnold Press", MuscleGroup = "Shoulders",
                        Description = "A dumbbell pressing variation with an internal-to-external rotation that recruits all three deltoid heads."
                    },
                    new Exercise
                    {
                        Name = "Upright Row", MuscleGroup = "Shoulders",
                        Description = "A pulling movement where the bar or dumbbells are drawn up along the torso, targeting the upper traps and lateral deltoids."
                    },
                    // Legs
                    new Exercise
                    {
                        Name = "Squat", MuscleGroup = "Legs",
                        Description = "The foundational lower-body compound exercise with a barbell on the back, targeting the quads, glutes, and hamstrings."
                    },
                    new Exercise
                    {
                        Name = "Leg Press", MuscleGroup = "Legs",
                        Description = "A machine-based lower-body pushing exercise targeting the quads, glutes, and hamstrings with reduced spinal load."
                    },
                    new Exercise
                    {
                        Name = "Romanian Deadlift", MuscleGroup = "Legs",
                        Description = "A hip-hinge movement with a slight knee bend that primarily targets the hamstrings and glutes."
                    },
                    new Exercise
                    {
                        Name = "Leg Curl", MuscleGroup = "Legs",
                        Description = "A machine isolation exercise that flexes the knee to directly target the hamstrings."
                    },
                    new Exercise
                    {
                        Name = "Leg Extension", MuscleGroup = "Legs",
                        Description = "A machine isolation exercise that extends the knee to directly target the quadriceps."
                    },
                    new Exercise
                    {
                        Name = "Calf Raise", MuscleGroup = "Legs",
                        Description = "An isolation exercise targeting the gastrocnemius and soleus by plantarflexing the ankle (rising onto the toes)."
                    },
                    new Exercise
                    {
                        Name = "Lunges", MuscleGroup = "Legs",
                        Description = "A unilateral lower-body exercise targeting the quads, glutes, and hamstrings while challenging balance and coordination."
                    },
                    new Exercise
                    {
                        Name = "Bulgarian Split Squat", MuscleGroup = "Legs",
                        Description = "A unilateral squat variation with the rear foot elevated, placing intense emphasis on the quads and glutes."
                    },
                    // Biceps
                    new Exercise
                    {
                        Name = "Barbell Curl", MuscleGroup = "Biceps",
                        Description = "A classic barbell curl that allows heavy loading of the biceps brachii with a supinated grip."
                    },
                    new Exercise
                    {
                        Name = "Dumbbell Curl", MuscleGroup = "Biceps",
                        Description = "A standard bicep curl with dumbbells that allows supination at the top for peak bicep contraction."
                    },
                    new Exercise
                    {
                        Name = "Hammer Curl", MuscleGroup = "Biceps",
                        Description = "A bicep curl with a neutral (hammer) grip that also targets the brachialis and brachioradialis."
                    },
                    new Exercise
                    {
                        Name = "Preacher Curl", MuscleGroup = "Biceps",
                        Description = "A bicep curl on a preacher bench that braces the upper arm to eliminate momentum and maximise the stretch."
                    },
                    new Exercise
                    {
                        Name = "Cable Curl", MuscleGroup = "Biceps",
                        Description = "A bicep curl using a cable machine that maintains constant tension throughout the entire movement."
                    },
                    // Triceps
                    new Exercise
                    {
                        Name = "Tricep Pushdown", MuscleGroup = "Triceps",
                        Description = "A cable isolation exercise pushing a bar or rope downward to extend the elbow and target all three tricep heads."
                    },
                    new Exercise
                    {
                        Name = "Skull Crusher", MuscleGroup = "Triceps",
                        Description = "A lying tricep extension with a barbell or dumbbells targeting the long and lateral heads of the triceps."
                    },
                    new Exercise
                    {
                        Name = "Overhead Tricep Extension", MuscleGroup = "Triceps",
                        Description = "A tricep isolation exercise performed overhead that places maximum stretch on the long head of the triceps."
                    },
                    new Exercise
                    {
                        Name = "Tricep Dip", MuscleGroup = "Triceps",
                        Description = "A bodyweight pressing exercise with hands on a bench or parallel bars behind the body, targeting the triceps."
                    },
                    new Exercise
                    {
                        Name = "Close-Grip Bench Press", MuscleGroup = "Triceps",
                        Description = "A bench press variation with a narrow grip that shifts emphasis from the chest to the triceps."
                    },
                    // Core
                    new Exercise
                    {
                        Name = "Plank", MuscleGroup = "Core",
                        Description = "An isometric hold that builds stability and endurance in the abdominals, obliques, and lower back."
                    },
                    new Exercise
                    {
                        Name = "Crunch", MuscleGroup = "Core",
                        Description = "A classic abdominal exercise that flexes the spine to contract the rectus abdominis."
                    },
                    new Exercise
                    {
                        Name = "Russian Twist", MuscleGroup = "Core",
                        Description = "A rotational core exercise targeting the obliques, performed by twisting side-to-side while seated with feet off the floor."
                    },
                    new Exercise
                    {
                        Name = "Leg Raise", MuscleGroup = "Core",
                        Description = "A lower-abdominal exercise performed by raising the legs while hanging from a bar or lying flat."
                    },
                    new Exercise
                    {
                        Name = "Ab Wheel Rollout", MuscleGroup = "Core",
                        Description = "An advanced core exercise using an ab wheel that extends the body forward to challenge the entire anterior core."
                    }
                );
                await context.SaveChangesAsync();
            }
            else
            {
                // ── Block 1b: Add missing exercises and back-fill null descriptions ────
                var catalog = new Dictionary<string, (string MuscleGroup, string Description)>
                {
                    ["Bench Press"]               = ("Chest",     "A compound pushing movement on a flat bench targeting the pectorals, anterior deltoids, and triceps."),
                    ["Incline Bench Press"]        = ("Chest",     "A pressing movement on an inclined bench (30–45°) that emphasises the upper chest and anterior deltoids."),
                    ["Decline Bench Press"]        = ("Chest",     "A pressing movement on a declined bench that places more emphasis on the lower chest."),
                    ["Dumbbell Fly"]               = ("Chest",     "An isolation movement using dumbbells in a wide arc to stretch and contract the pectoral muscles."),
                    ["Cable Crossover"]            = ("Chest",     "A cable-based fly that keeps constant tension on the chest throughout the full range of motion."),
                    ["Chest Dip"]                  = ("Chest",     "A bodyweight pushing exercise performed with a forward lean to target the lower chest and triceps."),
                    ["Push-Up"]                    = ("Chest",     "A fundamental bodyweight exercise targeting the chest, shoulders, and triceps by pressing against the floor."),
                    ["Deadlift"]                   = ("Back",      "A foundational compound lift targeting the entire posterior chain: glutes, hamstrings, spinal erectors, and upper back."),
                    ["Pull-Up"]                    = ("Back",      "A bodyweight vertical pulling exercise targeting the latissimus dorsi, biceps, and upper back."),
                    ["Bent-Over Row"]              = ("Back",      "A horizontal pulling compound movement with the torso hinged forward, targeting the lats, rhomboids, and rear deltoids."),
                    ["Lat Pulldown"]               = ("Back",      "A cable machine exercise that mimics the pull-up, targeting the latissimus dorsi and biceps."),
                    ["Seated Cable Row"]           = ("Back",      "A horizontal pulling cable movement targeting the mid-back, rhomboids, and lats."),
                    ["T-Bar Row"]                  = ("Back",      "A compound rowing movement with a barbell loaded on one end, targeting the mid and lower back."),
                    ["Single-Arm Dumbbell Row"]    = ("Back",      "A unilateral rowing exercise targeting the lats and upper back while reducing lower back strain."),
                    ["Overhead Press"]             = ("Shoulders", "A compound pressing movement overhead targeting the deltoids, upper traps, and triceps."),
                    ["Lateral Raise"]              = ("Shoulders", "An isolation exercise using dumbbells raised to the sides to target the medial (side) deltoid head."),
                    ["Front Raise"]                = ("Shoulders", "An isolation exercise targeting the anterior (front) deltoid by raising weights directly in front of the body."),
                    ["Rear Delt Fly"]              = ("Shoulders", "An isolation exercise targeting the posterior (rear) deltoid, performed bent-over or on a reverse pec-deck."),
                    ["Arnold Press"]               = ("Shoulders", "A dumbbell pressing variation with an internal-to-external rotation that recruits all three deltoid heads."),
                    ["Upright Row"]                = ("Shoulders", "A pulling movement where the bar or dumbbells are drawn up along the torso, targeting the upper traps and lateral deltoids."),
                    ["Squat"]                      = ("Legs",      "The foundational lower-body compound exercise with a barbell on the back, targeting the quads, glutes, and hamstrings."),
                    ["Leg Press"]                  = ("Legs",      "A machine-based lower-body pushing exercise targeting the quads, glutes, and hamstrings with reduced spinal load."),
                    ["Romanian Deadlift"]          = ("Legs",      "A hip-hinge movement with a slight knee bend that primarily targets the hamstrings and glutes."),
                    ["Leg Curl"]                   = ("Legs",      "A machine isolation exercise that flexes the knee to directly target the hamstrings."),
                    ["Leg Extension"]              = ("Legs",      "A machine isolation exercise that extends the knee to directly target the quadriceps."),
                    ["Calf Raise"]                 = ("Legs",      "An isolation exercise targeting the gastrocnemius and soleus by plantarflexing the ankle (rising onto the toes)."),
                    ["Lunges"]                     = ("Legs",      "A unilateral lower-body exercise targeting the quads, glutes, and hamstrings while challenging balance and coordination."),
                    ["Bulgarian Split Squat"]      = ("Legs",      "A unilateral squat variation with the rear foot elevated, placing intense emphasis on the quads and glutes."),
                    ["Barbell Curl"]               = ("Biceps",    "A classic barbell curl that allows heavy loading of the biceps brachii with a supinated grip."),
                    ["Dumbbell Curl"]              = ("Biceps",    "A standard bicep curl with dumbbells that allows supination at the top for peak bicep contraction."),
                    ["Hammer Curl"]                = ("Biceps",    "A bicep curl with a neutral (hammer) grip that also targets the brachialis and brachioradialis."),
                    ["Preacher Curl"]              = ("Biceps",    "A bicep curl on a preacher bench that braces the upper arm to eliminate momentum and maximise the stretch."),
                    ["Cable Curl"]                 = ("Biceps",    "A bicep curl using a cable machine that maintains constant tension throughout the entire movement."),
                    ["Tricep Pushdown"]            = ("Triceps",   "A cable isolation exercise pushing a bar or rope downward to extend the elbow and target all three tricep heads."),
                    ["Skull Crusher"]              = ("Triceps",   "A lying tricep extension with a barbell or dumbbells targeting the long and lateral heads of the triceps."),
                    ["Overhead Tricep Extension"]  = ("Triceps",   "A tricep isolation exercise performed overhead that places maximum stretch on the long head of the triceps."),
                    ["Tricep Dip"]                 = ("Triceps",   "A bodyweight pressing exercise with hands on a bench or parallel bars behind the body, targeting the triceps."),
                    ["Close-Grip Bench Press"]     = ("Triceps",   "A bench press variation with a narrow grip that shifts emphasis from the chest to the triceps."),
                    ["Plank"]                      = ("Core",      "An isometric hold that builds stability and endurance in the abdominals, obliques, and lower back."),
                    ["Crunch"]                     = ("Core",      "A classic abdominal exercise that flexes the spine to contract the rectus abdominis."),
                    ["Russian Twist"]              = ("Core",      "A rotational core exercise targeting the obliques, performed by twisting side-to-side while seated with feet off the floor."),
                    ["Leg Raise"]                  = ("Core",      "A lower-abdominal exercise performed by raising the legs while hanging from a bar or lying flat."),
                    ["Ab Wheel Rollout"]           = ("Core",      "An advanced core exercise using an ab wheel that extends the body forward to challenge the entire anterior core."),
                };

                var existingNames = (await context.Exercises.Select(e => e.Name).ToListAsync()).ToHashSet();

                var toAdd = catalog
                    .Where(kv => !existingNames.Contains(kv.Key))
                    .Select(kv => new Exercise
                    {
                        Name        = kv.Key,
                        MuscleGroup = kv.Value.MuscleGroup,
                        Description = kv.Value.Description,
                    })
                    .ToList();

                if (toAdd.Count > 0)
                    context.Exercises.AddRange(toAdd);

                var toUpdate = await context.Exercises
                    .Where(e => e.Description == null)
                    .ToListAsync();

                foreach (var exercise in toUpdate)
                {
                    if (catalog.TryGetValue(exercise.Name, out var entry))
                        exercise.Description = entry.Description;
                }

                if (toAdd.Count > 0 || toUpdate.Count > 0)
                    await context.SaveChangesAsync();
            }

            // ── Block 2: Seed test user + sample workout only if not present ─────
            if (await context.Users.AnyAsync(u => u.Email == "test@workout.com"))
                return;

            var user = new User
            {
                Email = "test@workout.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test1234"),
                UserName = "TestUser",
                CreatedAt = DateTime.UtcNow,
            };
            context.Users.Add(user);

            // Re-query exercises so we reference the persisted entities.
            var benchPress = await context.Exercises.FirstAsync(e => e.Name == "Bench Press");
            var ohPress    = await context.Exercises.FirstAsync(e => e.Name == "Overhead Press");

            var pushWorkout = new Workout
            {
                Name = "Push Day",
                Description = "Chest, shoulders and triceps",
                UserId = user.UserId,
                User = user,
            };
            context.Workouts.Add(pushWorkout);

            var mainGym = new WorkoutVariation
            {
                Name = "Main",
                Workout = pushWorkout,
                VariationExercises = new List<VariationExercise>
                {
                    new VariationExercise { Exercise = benchPress },
                    new VariationExercise { Exercise = ohPress },
                },
            };
            context.WorkoutVariations.Add(mainGym);

            var session = new Session
            {
                WorkoutVariation = mainGym,
                DateTime = DateTime.UtcNow.AddDays(-2),
                Duration = TimeSpan.FromMinutes(55),
            };
            context.Sessions.Add(session);

            var seBench = new SessionExercise
            {
                Session = session,
                Exercise = benchPress,
                Note = "Felt strong today",
                Sets = new List<Set>
                {
                    new Set { Reps = 10, Weight = 60,  BreakTime = TimeSpan.FromSeconds(90) },
                    new Set { Reps = 8,  Weight = 70,  BreakTime = TimeSpan.FromSeconds(90) },
                    new Set { Reps = 6,  Weight = 80,  BreakTime = TimeSpan.FromSeconds(120) },
                }
            };
            var seOhp = new SessionExercise
            {
                Session = session,
                Exercise = ohPress,
                Note = "",
                Sets = new List<Set>
                {
                    new Set { Reps = 10, Weight = 40, BreakTime = TimeSpan.FromSeconds(90) },
                    new Set { Reps = 8,  Weight = 45, BreakTime = TimeSpan.FromSeconds(90) },
                }
            };
            context.SessionExercises.AddRange(seBench, seOhp);

            var prBench = new PersonalRecord
            {
                User = user,
                Exercise = benchPress,
                BestReps = 6,
                BestWeight = 80,
                AchivedDate = DateTime.UtcNow.AddDays(-2),
            };
            context.PersonalRecords.Add(prBench);

            await context.SaveChangesAsync();
        }
    }
}
