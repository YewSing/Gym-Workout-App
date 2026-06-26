using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWorkoutApp.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkoutName",
                table: "Workouts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "Workouts",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Workouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Exercises",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Workouts",
                newName: "WorkoutName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workouts",
                newName: "WorkoutId");
        }
    }
}
