using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIEnercheck.Migrations
{
    /// <inheritdoc />
    public partial class Modelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssinaturaPlano_Planos_PlanoId1",
                table: "AssinaturaPlano");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Projeto",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanoId1",
                table: "AssinaturaPlano",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AssinaturaPlano_Planos_PlanoId1",
                table: "AssinaturaPlano",
                column: "PlanoId1",
                principalTable: "Planos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssinaturaPlano_Planos_PlanoId1",
                table: "AssinaturaPlano");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Projeto",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanoId1",
                table: "AssinaturaPlano",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssinaturaPlano_Planos_PlanoId1",
                table: "AssinaturaPlano",
                column: "PlanoId1",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
