using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyWorkoutApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVariationExerciseJoinEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariationExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkoutVariationId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariationExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariationExercises_WorkoutVariations_WorkoutVariationId",
                        column: x => x.WorkoutVariationId,
                        principalTable: "WorkoutVariations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Carry existing gym/exercise assignments over from the old implicit join
            // table before dropping it, so no data is lost.
            migrationBuilder.Sql(@"
                INSERT INTO ""VariationExercises"" (""WorkoutVariationId"", ""ExerciseId"")
                SELECT ""WorkoutVariationsId"", ""ExercisesId""
                FROM ""ExerciseWorkoutVariation""
                ORDER BY ""WorkoutVariationsId"", ""ExercisesId"";
            ");

            migrationBuilder.DropTable(
                name: "ExerciseWorkoutVariation");

            migrationBuilder.CreateIndex(
                name: "IX_VariationExercises_ExerciseId",
                table: "VariationExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationExercises_WorkoutVariationId",
                table: "VariationExercises",
                column: "WorkoutVariationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.Sql(@"
                INSERT INTO ""ExerciseWorkoutVariation"" (""ExercisesId"", ""WorkoutVariationsId"")
                SELECT ""ExerciseId"", ""WorkoutVariationId""
                FROM ""VariationExercises""
                ON CONFLICT DO NOTHING;
            ");

            migrationBuilder.DropTable(
                name: "VariationExercises");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkoutVariation_WorkoutVariationsId",
                table: "ExerciseWorkoutVariation",
                column: "WorkoutVariationsId");
        }
    }
}
