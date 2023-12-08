using Microsoft.EntityFrameworkCore;

namespace FinalProject{
    public static class SeedData{
        public static void Initialize(IServiceProvider serviceProvider){
            using(var context = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>())){
                //Look for any Users
                if(context.Users.Any()){
                    return; //DB has been seeded
                }
                context.Users.AddRange(
                    new User{
                        Full_Name = "Jason Harris", Birth_Date = new DateOnly(2003,03,20), HeightFT = 6.2, BodyWeightLbs = 210, 
                        Goals = new List<Goals>{
                            new Goals{
                                GoalName = "BenchPress", WeightLbs = 310
                            },
                            new Goals{
                                GoalName = "Squat", WeightLbs = 400
                            },
                            new Goals{
                                GoalName = "PowerClean", WeightLbs = 300
                            }
                        },
                        Maxes = new List<Maxes>{
                            new Maxes{
                                MaxName = "BenchPress", WeightLbs = 300
                            },
                            new Maxes{
                                MaxName = "Squat", WeightLbs = 320
                            },
                            new Maxes{
                                MaxName = "PowerClean", WeightLbs = 290
                            }
                        }
                    }
                );
                context.SaveChanges();

                //Seeding Workouts----------------------------------------
                if(context.Workouts.Any()){
                    return; //DB has been seeded
                }

                // Get the user for whom you want to associate the maxes
                User user = context.Users.FirstOrDefault(u => u.Full_Name == "Jason Harris");

                // Retrieve the Maxes entries for Jason Harris
                Maxes BenchPressMax = user?.Maxes.FirstOrDefault(m => m.MaxName == "BenchPress");
                Maxes SquatMax = user?.Maxes.FirstOrDefault(m => m.MaxName == "Squat");
                Maxes PowerCleanMax = user?.Maxes.FirstOrDefault(m => m.MaxName == "PowerClean");


                var workoutsSeedData = new List<Workouts>
                {
                    new Workouts{
                        Name = "BenchPress", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Chest"}, new MuscleGroup{Muscle = "Tricep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 40.00, Max = BenchPressMax 
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 60.00, Max = BenchPressMax
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 80.00, Max = BenchPressMax
                            }

                        }
                    },
                    new Workouts{
                        Name = "Squat", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quaud"}, new MuscleGroup{Muscle = "Glutes"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 40.00, Max = SquatMax 
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 60.00, Max = SquatMax
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 80.00, Max = SquatMax
                            }

                        }
                    },
                    new Workouts{
                        Name = "PowerClean", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Hamstring"}, new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Quad"},new MuscleGroup{Muscle = "Bicep"},new MuscleGroup{Muscle = "Shoulder"},new MuscleGroup{Muscle = "Core"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 40.00, Max = PowerCleanMax, Reps = 6, Sets = 5
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 60.00, Max = PowerCleanMax, Reps = 4, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 80.00, Max = PowerCleanMax, Reps = 2, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Push-UP", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Chest"}, new MuscleGroup{Muscle = "Shoulder"}, new MuscleGroup{Muscle = "Core"}, new MuscleGroup{Muscle = "Tricep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Reps = 10, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Reps = 20, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Reps = 30, Sets = 4
                            }

                        }
                    },
                    new Workouts{
                        Name = "Dumbell Bench Press", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Chest"},new MuscleGroup{Muscle = "Shoulder"},new MuscleGroup{Muscle = "Tricep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 10.00, Max = BenchPressMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 20.00, Max = BenchPressMax, Reps = 10, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 30.00, Max = BenchPressMax, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Dips", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Chest"},new MuscleGroup{Muscle = "Tricep"},new MuscleGroup{Muscle = "Shoulder"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Reps = 8 , Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Reps = 10 , Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Reps = 15, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Dumbbell Fly ", Weighted = true , MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Chest"}, new MuscleGroup{Muscle = "Shoulder"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 03.33, Max = BenchPressMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 5.00, Max = BenchPressMax, Reps = 10 , Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 6.67, Max = BenchPressMax, Reps = 10, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Dumbell Curl ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Bicep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 15, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 35, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight = 50, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Barbell Curl ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "bicep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 30, Reps = 12, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 60, Reps = 10, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight = 90, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Barbell Row ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Bicep"},new MuscleGroup{Muscle = "Back"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight =90 , Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight =135 , Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight =225 , Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Pull-Up", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Bicep"}, new MuscleGroup{Muscle = "Back"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Reps = 8, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Reps = 12, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard",  Reps = 15, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Preache Curls ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Bicep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 20, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 35, Reps = 18, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight = 50, Reps = 6, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "RDL ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Back"},new MuscleGroup{Muscle = "Hamstring"},new MuscleGroup{Muscle = "Glutes"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 90 , Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 135 , Reps = 8, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight = 225 , Reps = 6, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Bulgarian Split Squats ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quads"},new MuscleGroup{Muscle = "Hamstrings"},new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Calves"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 13.00, Max = SquatMax , Reps = 10, Sets = 4 
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 19.00, Max = SquatMax, Reps = 8, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 27.00, Max = SquatMax, Reps = 6, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Leg Extension ", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quad"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 25, Max = SquatMax, Reps = 15, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 32.00, Max = SquatMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 42.00, Max = SquatMax, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Lunges", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quad"},new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Hamstring"},new MuscleGroup{Muscle = "Claves"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy",  Reps = 8, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium",  Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard",  Reps = 15, Sets = 4
                            }

                        }
                    },
                    new Workouts{
                        Name = "Weighted Lunges", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quad"},new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Hamstring"},new MuscleGroup{Muscle = "Claves"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 10.00, Max = SquatMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 15.00, Max = SquatMax, Reps = 8, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 20.00, Max = SquatMax, Reps = 6, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Leg Press", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quad"},new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Hamstring"},new MuscleGroup{Muscle = "Claves"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 60.00, Max = SquatMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 80.00, Max = SquatMax, Reps = 10, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 110.00, Max = SquatMax, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Front Squat", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Quads"},new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Hamstrings"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 40.00, Max = SquatMax , Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 60.00, Max = SquatMax, Reps = 8, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 80.00, Max = SquatMax, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Glute Bridge", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Glutes"},new MuscleGroup{Muscle = "Hamstring"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Percentage = 50.00, Max = SquatMax, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Percentage = 70.00, Max = SquatMax, Reps = 8, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Percentage = 90.00, Max = SquatMax, Reps = 6, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Tricep Extensions", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Tricep"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 40, Reps = 15, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 60, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight =  80, Reps = 8, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Hamstring Curl", Weighted = true, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Hamstring"},new MuscleGroup{Muscle = "Glute"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", DefaultWeight = 60, Reps = 15, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Medium", DefaultWeight = 80, Reps = 10, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", DefaultWeight = 100, Reps = 10, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Crunches", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Core"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Reps = 15, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Reps = 20, Sets = 4
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Reps = 25, Sets = 5
                            }

                        }
                    },
                    new Workouts{
                        Name = "Bicycle Crunches", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Core"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy",  Reps = 10, Sets = 2
                            },
                            new Difficulty{
                                Diff_Name = "Medium",  Reps = 15, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard",  Reps = 25, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Toe Touches", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Core"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy",  Reps = 8, Sets = 2
                            },
                            new Difficulty{
                                Diff_Name = "Medium",  Reps = 10, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard",  Reps = 15, Sets = 3
                            }

                        }
                    },
                    new Workouts{
                        Name = "Russian Twists", Weighted = false, MuscleGroups = new List<MuscleGroup>{new MuscleGroup{Muscle = "Core"}},
                        Difficulty = new List<Difficulty>{
                            new Difficulty{
                                Diff_Name = "Easy", Reps = 8, Sets = 2
                            },
                            new Difficulty{
                                Diff_Name = "Medium", Reps = 12, Sets = 3
                            },
                            new Difficulty{
                                Diff_Name = "Hard", Reps = 16, Sets = 3
                            }

                        }
                    }
                };
                // Add all the Workouts entities to the context
                context.Workouts.AddRange(workoutsSeedData);

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}