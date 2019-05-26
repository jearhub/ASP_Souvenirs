using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Souvenirs.Migrations
{
    public partial class FixCartItemModelspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Sourvenir_SourvenirSouvenirID",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "SourvenirSouvenirID",
                table: "CartItem",
                newName: "SouvenirID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_SourvenirSouvenirID",
                table: "CartItem",
                newName: "IX_CartItem_SouvenirID");

            migrationBuilder.AddColumn<string>(
                name: "CartID",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Sourvenir_SouvenirID",
                table: "CartItem",
                column: "SouvenirID",
                principalTable: "Sourvenir",
                principalColumn: "SouvenirID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Sourvenir_SouvenirID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "SouvenirID",
                table: "CartItem",
                newName: "SourvenirSouvenirID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_SouvenirID",
                table: "CartItem",
                newName: "IX_CartItem_SourvenirSouvenirID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Sourvenir_SourvenirSouvenirID",
                table: "CartItem",
                column: "SourvenirSouvenirID",
                principalTable: "Sourvenir",
                principalColumn: "SouvenirID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
