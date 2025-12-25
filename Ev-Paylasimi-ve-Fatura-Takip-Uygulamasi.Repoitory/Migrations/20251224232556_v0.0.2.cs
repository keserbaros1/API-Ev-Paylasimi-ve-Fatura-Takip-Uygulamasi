using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Repoitory.Migrations
{
    /// <inheritdoc />
    public partial class v002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseShareId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ExpenseShareId",
                table: "Payments",
                column: "ExpenseShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ExpenseShares_ExpenseShareId",
                table: "Payments",
                column: "ExpenseShareId",
                principalTable: "ExpenseShares",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ExpenseShares_ExpenseShareId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ExpenseShareId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExpenseShareId",
                table: "Payments");
        }
    }
}
