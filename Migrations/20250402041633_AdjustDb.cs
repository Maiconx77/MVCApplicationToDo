using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCApplicationToDo.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "MilestoneChains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneChains_ProjectId",
                table: "MilestoneChains",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneChains_Projects_ProjectId",
                table: "MilestoneChains",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneChains_Projects_ProjectId",
                table: "MilestoneChains");

            migrationBuilder.DropIndex(
                name: "IX_MilestoneChains_ProjectId",
                table: "MilestoneChains");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MilestoneChains");
        }
    }
}
