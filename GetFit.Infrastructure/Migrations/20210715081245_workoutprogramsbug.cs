using Microsoft.EntityFrameworkCore.Migrations;

namespace GetFit.Web.Migrations
{
#pragma warning disable IDE1006 // Naming Styles
    public partial class workoutprogramsbug : Migration
#pragma warning restore IDE1006 // Naming Styles
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutPrograms_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutWorkoutPlan");

            migrationBuilder.RenameColumn(
                name: "WorkoutProgramId",
                table: "Workouts",
                newName: "WorkoutPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutProgramId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutPlanId");

            migrationBuilder.CreateTable(
                name: "WorkoutWorkoutProgram",
                columns: table => new
                {
                    WorkoutProgramsId = table.Column<int>(type: "int", nullable: false),
                    WorkoutsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutWorkoutProgram", x => new { x.WorkoutProgramsId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutProgram_WorkoutPrograms_WorkoutProgramsId",
                        column: x => x.WorkoutProgramsId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutProgram_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutWorkoutProgram_WorkoutsId",
                table: "WorkoutWorkoutProgram",
                column: "WorkoutsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutPlans_WorkoutPlanId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutWorkoutProgram");

            migrationBuilder.RenameColumn(
                name: "WorkoutPlanId",
                table: "Workouts",
                newName: "WorkoutProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutPlanId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutProgramId");

            migrationBuilder.CreateTable(
                name: "WorkoutWorkoutPlan",
                columns: table => new
                {
                    WorkoutProgramsId = table.Column<int>(type: "int", nullable: false),
                    WorkoutsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutWorkoutPlan", x => new { x.WorkoutProgramsId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutPlan_WorkoutPlans_WorkoutProgramsId",
                        column: x => x.WorkoutProgramsId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutWorkoutPlan_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutWorkoutPlan_WorkoutsId",
                table: "WorkoutWorkoutPlan",
                column: "WorkoutsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutPrograms_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
