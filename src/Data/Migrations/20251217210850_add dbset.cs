using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class adddbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStep_Books_BookId",
                table: "BookStep");

            migrationBuilder.DropForeignKey(
                name: "FK_BookStep_ProductionStep_ProductionStepId",
                table: "BookStep");

            migrationBuilder.DropForeignKey(
                name: "FK_BookStep_Status_StatusId",
                table: "BookStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionStep",
                table: "ProductionStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookStep",
                table: "BookStep");

            migrationBuilder.RenameTable(
                name: "ProductionStep",
                newName: "ProductionSteps");

            migrationBuilder.RenameTable(
                name: "BookStep",
                newName: "BookSteps");

            migrationBuilder.RenameIndex(
                name: "IX_BookStep_StatusId",
                table: "BookSteps",
                newName: "IX_BookSteps_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_BookStep_ProductionStepId",
                table: "BookSteps",
                newName: "IX_BookSteps_ProductionStepId");

            migrationBuilder.RenameIndex(
                name: "IX_BookStep_BookId_ProductionStepId",
                table: "BookSteps",
                newName: "IX_BookSteps_BookId_ProductionStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionSteps",
                table: "ProductionSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSteps",
                table: "BookSteps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSteps_Books_BookId",
                table: "BookSteps",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSteps_ProductionSteps_ProductionStepId",
                table: "BookSteps",
                column: "ProductionStepId",
                principalTable: "ProductionSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSteps_Status_StatusId",
                table: "BookSteps",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSteps_Books_BookId",
                table: "BookSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSteps_ProductionSteps_ProductionStepId",
                table: "BookSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSteps_Status_StatusId",
                table: "BookSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionSteps",
                table: "ProductionSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSteps",
                table: "BookSteps");

            migrationBuilder.RenameTable(
                name: "ProductionSteps",
                newName: "ProductionStep");

            migrationBuilder.RenameTable(
                name: "BookSteps",
                newName: "BookStep");

            migrationBuilder.RenameIndex(
                name: "IX_BookSteps_StatusId",
                table: "BookStep",
                newName: "IX_BookStep_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_BookSteps_ProductionStepId",
                table: "BookStep",
                newName: "IX_BookStep_ProductionStepId");

            migrationBuilder.RenameIndex(
                name: "IX_BookSteps_BookId_ProductionStepId",
                table: "BookStep",
                newName: "IX_BookStep_BookId_ProductionStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionStep",
                table: "ProductionStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookStep",
                table: "BookStep",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStep_Books_BookId",
                table: "BookStep",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookStep_ProductionStep_ProductionStepId",
                table: "BookStep",
                column: "ProductionStepId",
                principalTable: "ProductionStep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookStep_Status_StatusId",
                table: "BookStep",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
