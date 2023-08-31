using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace umg_safety_standars.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class Migration19082023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umg_session_umg_supplier_supplier_id",
                table: "umg_session");

            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "umg_session",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_umg_session_umg_supplier_supplier_id",
                table: "umg_session",
                column: "supplier_id",
                principalTable: "umg_supplier",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_umg_session_umg_supplier_supplier_id",
                table: "umg_session");

            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "umg_session",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_umg_session_umg_supplier_supplier_id",
                table: "umg_session",
                column: "supplier_id",
                principalTable: "umg_supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
