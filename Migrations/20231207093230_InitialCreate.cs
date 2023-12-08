using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABP_FInalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Full_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Birth_Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HeightFT = table.Column<double>(type: "REAL", nullable: false),
                    BodyWeightLbs = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    WorkoutID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Weighted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.WorkoutID);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalName = table.Column<string>(type: "TEXT", nullable: false),
                    WeightLbs = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalID);
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maxes",
                columns: table => new
                {
                    MaxID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxName = table.Column<string>(type: "TEXT", nullable: false),
                    WeightLbs = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maxes", x => x.MaxID);
                    table.ForeignKey(
                        name: "FK_Maxes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    MuscleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Muscle = table.Column<string>(type: "TEXT", nullable: false),
                    WorkoutsWorkoutID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.MuscleID);
                    table.ForeignKey(
                        name: "FK_MuscleGroups_Workouts_WorkoutsWorkoutID",
                        column: x => x.WorkoutsWorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledLifts",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkoutID = table.Column<int>(type: "INTEGER", nullable: false),
                    LiftDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Complete = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledLifts", x => new { x.UserID, x.WorkoutID });
                    table.ForeignKey(
                        name: "FK_ScheduledLifts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledLifts_Workouts_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    DiffID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Diff_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Percentage = table.Column<double>(type: "REAL", nullable: false),
                    DefaultWeight = table.Column<double>(type: "REAL", nullable: false),
                    Sets = table.Column<int>(type: "INTEGER", nullable: false),
                    Reps = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxID = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkoutsWorkoutID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.DiffID);
                    table.ForeignKey(
                        name: "FK_Difficulties_Maxes_MaxID",
                        column: x => x.MaxID,
                        principalTable: "Maxes",
                        principalColumn: "MaxID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Difficulties_Workouts_WorkoutsWorkoutID",
                        column: x => x.WorkoutsWorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID");
                });

            migrationBuilder.CreateTable(
                name: "WorkoutMuscleGroup",
                columns: table => new
                {
                    WorkoutID = table.Column<int>(type: "INTEGER", nullable: false),
                    MuscleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutMuscleGroup", x => new { x.WorkoutID, x.MuscleID });
                    table.ForeignKey(
                        name: "FK_WorkoutMuscleGroup_MuscleGroups_MuscleID",
                        column: x => x.MuscleID,
                        principalTable: "MuscleGroups",
                        principalColumn: "MuscleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutMuscleGroup_Workouts_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDifficulty",
                columns: table => new
                {
                    WorkoutID = table.Column<int>(type: "INTEGER", nullable: false),
                    DiffID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDifficulty", x => new { x.WorkoutID, x.DiffID });
                    table.ForeignKey(
                        name: "FK_WorkoutDifficulty_Difficulties_DiffID",
                        column: x => x.DiffID,
                        principalTable: "Difficulties",
                        principalColumn: "DiffID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutDifficulty_Workouts_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Difficulties_MaxID",
                table: "Difficulties",
                column: "MaxID");

            migrationBuilder.CreateIndex(
                name: "IX_Difficulties_WorkoutsWorkoutID",
                table: "Difficulties",
                column: "WorkoutsWorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserID",
                table: "Goals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Maxes_UserID",
                table: "Maxes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroups_WorkoutsWorkoutID",
                table: "MuscleGroups",
                column: "WorkoutsWorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLifts_WorkoutID",
                table: "ScheduledLifts",
                column: "WorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDifficulty_DiffID",
                table: "WorkoutDifficulty",
                column: "DiffID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutMuscleGroup_MuscleID",
                table: "WorkoutMuscleGroup",
                column: "MuscleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "ScheduledLifts");

            migrationBuilder.DropTable(
                name: "WorkoutDifficulty");

            migrationBuilder.DropTable(
                name: "WorkoutMuscleGroup");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropTable(
                name: "Maxes");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
