using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FinalProject{
    public class User{
        [Key]
        public int UserID {get;set;}
        public string Full_Name {get;set;} = string.Empty;
        public DateOnly Birth_Date {get;set;} 
        public int Age => CalculateAge(Birth_Date);//calculated from Birthdate
        public double HeightFT {get;set;} //inputed as feet and inches, but claculated to inches
        public double BodyWeightLbs {get;set;} // pounds
        public double BMI => CalculateBMI(); //caluclated from weight and height
        
        //navigators
        public List<ScheduledLift> ScheduledLift {get;set;} = default!;
        public List<Maxes> Maxes {get;set;} = default!;
        public List<Goals> Goals {get;set;} = default!;

        //methods
        private int CalculateAge(DateOnly birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;
            return age;
        }

        private double CalculateBMI()
        {
            // BMI formula: BMI = weight(kg) / (height(m) * height(m))
            double heightInMeters = HeightFT * 0.3048; // Convert feet to meters
            double weightInKg = BodyWeightLbs * 0.453592; // Convert pounds to kg

            double bmi = weightInKg / (heightInMeters * heightInMeters);

            return Math.Round(bmi, 2);
        }
    } 
    public class Goals{
        [Key]
        public int GoalID {get;set;}
        public User User {get;set;}
        public int UserID {get;set;}
        public string GoalName {get;set;} = string.Empty;
        public double WeightLbs {get;set;}
    }
    public class Maxes{
        [Key]
        public int MaxID {get;set;}
        public User User {get;set;} = default!;
        public int UserID {get;set;}
        public string MaxName {get;set;} = string.Empty;
        public double WeightLbs {get;set;}
        
    }
    public class Workouts{
        [Key]
        public int WorkoutID {get;set;}
        public string Name {get;set;} = string.Empty;
        public bool Weighted {get;set;}
        public List<Difficulty> Difficulty {get;set;} = default!;
        public List<MuscleGroup> MuscleGroups {get;set;} = default!;
        public ICollection<WorkoutMuscleGroup> WorkoutMuscleGroups { get; set; } = default!;
        public ICollection<WorkoutDifficulty> WorkoutDifficulties { get; set; } = default!;
        
    }
    public class MuscleGroup{
        [Key]
        public int MuscleID {get;set;}
        public string Muscle {get;set;} = string.Empty;
        public ICollection<WorkoutMuscleGroup> WorkoutMuscleGroups { get; set; } = default!;
    }

    public class Difficulty{
        [Key]
        public int DiffID {get;set;}
        public string Diff_Name {get;set;} = string.Empty;
        public double Percentage {get;set;}
        public double RepWeight => CalculateRepWeight(); //empty unless they put in a max for this work out
        public double DefaultWeight {get;set;} 
        public double CalBurn => CalculateCalBurn();
        public int Sets {get;set;}
        public int Reps {get;set;}
        public Maxes Max {get;set;} = default!;
        public ICollection<WorkoutDifficulty> WorkoutDifficulties { get; set; } = default!;

        //Methods
        public double CalculateRepWeight(){
            if (Max != null){
                return Max.WeightLbs * (Percentage/100.0);
            }
            if (Max == null && DefaultWeight != 0){
                return DefaultWeight;
            }
            else{
                return 0;
            }
            
            
        }
        public double CalculateCalBurn(){
            return (((RepWeight/150)*5)*Reps)*Sets;
        }
    }
    public class WorkoutMuscleGroup
    {
        public int WorkoutID { get; set; }
        public Workouts Workout { get; set; } = default!;
        public int MuscleID { get; set; }
        public MuscleGroup MuscleGroup { get; set; } = default!;
    }

    public class WorkoutDifficulty
    {
        public int WorkoutID { get; set; }
        public Workouts Workout { get; set; } = default!;
        public int DiffID { get; set; }
        public Difficulty Difficulty { get; set; } = default!;
    }

    public class ScheduledLift{
        /*probably going to make this into a dictionary visable to the user 
        associating the day to the workout*/
        [Key]
        public int UserID {get;set;}
        [Key]
        public int WorkoutID {get;set;}
        public User User {get;set;}
        public Workouts Workout {get;set;}
        public DateOnly LiftDate {get;set;}
        public bool Complete {get;set;} //once presed it add proper callory information 
    }
}

