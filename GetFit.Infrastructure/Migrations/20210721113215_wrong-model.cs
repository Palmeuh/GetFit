using Microsoft.EntityFrameworkCore.Migrations;

namespace GetFit.Web.Migrations
{
    public partial class wrongmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_WorkoutPlans_WorkoutProgramId",
                table: "WorkoutPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_WorkoutPrograms_WorkoutProgramId",
                table: "WorkoutPlans",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_WorkoutPrograms_WorkoutProgramId",
                table: "WorkoutPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_WorkoutPlans_WorkoutProgramId",
                table: "WorkoutPlans",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
