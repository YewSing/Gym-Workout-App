using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyWorkoutApp.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutVariations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Workouts_WorkoutId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "ExerciseWorkout");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "Sessions",
                newName: "WorkoutVariationId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_WorkoutId",
                table: "Sessions",
                newName: "IX_Sessions_WorkoutVariationId");

            migrationBuilder.CreateTable(
                name: "WorkoutVariations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkoutId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutVariations_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseWorkoutVariation",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "integer", nullable: false),
                    WorkoutVariationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkoutVariation", x => new { x.ExercisesId, x.WorkoutVariationsId });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkoutVariation_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkoutVariation_WorkoutVariations_WorkoutVariation~",
                        column: x => x.WorkoutVariationsId,
                        principalTable: "WorkoutVariations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkoutVariation_WorkoutVariationsId",
                table: "ExerciseWorkoutVariation",
                column: "WorkoutVariationsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutVariations_WorkoutId",
                table: "WorkoutVariations",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_WorkoutVariations_WorkoutVariationId",
                table: "Sessions",
                column: "WorkoutVariationId",
                principalTable: "WorkoutVariations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_WorkoutVariations_WorkoutVariationId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "ExerciseWorkoutVariation");

            migrationBuilder.DropTable(
                name: "WorkoutVariations");

            migrationBuilder.RenameColumn(
                name: "WorkoutVariationId",
                table: "Sessions",
                newName: "WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_WorkoutVariationId",
                table: "Sessions",
                newName: "IX_Sessions_WorkoutId");

            migrationBuilder.CreateTable(
                name: "ExerciseWorkout",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "integer", nullable: false),
                    WorkoutsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkout", x => new { x.ExercisesId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkout_WorkoutsId",
                table: "ExerciseWorkout",
                column: "WorkoutsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Workouts_WorkoutId",
                table: "Sessions",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
