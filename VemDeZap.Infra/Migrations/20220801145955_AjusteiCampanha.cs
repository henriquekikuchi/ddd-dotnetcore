using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemDeZap.Infra.Migrations
{
    public partial class AjusteiCampanha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campanha_Usuario_UsuarioId",
                table: "Campanha");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Campanha",
                newName: "idUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Campanha_UsuarioId",
                table: "Campanha",
                newName: "IX_Campanha_idUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campanha",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Campanha_Usuario_idUsuario",
                table: "Campanha",
                column: "idUsuario",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campanha_Usuario_idUsuario",
                table: "Campanha");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "Campanha",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Campanha_idUsuario",
                table: "Campanha",
                newName: "IX_Campanha_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campanha",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Campanha_Usuario_UsuarioId",
                table: "Campanha",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
