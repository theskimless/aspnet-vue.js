using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VueJsTest.Migrations
{
    public partial class deleteManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRubric_Posts_PostId",
                table: "PostRubric");

            migrationBuilder.DropForeignKey(
                name: "FK_PostRubric_Rubrics_RubricId",
                table: "PostRubric");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRubric",
                table: "PostRubric");

            migrationBuilder.DropIndex(
                name: "IX_PostRubric_RubricId",
                table: "PostRubric");

            migrationBuilder.RenameTable(
                name: "PostRubric",
                newName: "PostRubrics");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostRubrics",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRubrics",
                table: "PostRubrics",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRubrics",
                table: "PostRubrics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostRubrics");

            migrationBuilder.RenameTable(
                name: "PostRubrics",
                newName: "PostRubric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRubric",
                table: "PostRubric",
                columns: new[] { "PostId", "RubricId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostRubric_RubricId",
                table: "PostRubric",
                column: "RubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRubric_Posts_PostId",
                table: "PostRubric",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostRubric_Rubrics_RubricId",
                table: "PostRubric",
                column: "RubricId",
                principalTable: "Rubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
