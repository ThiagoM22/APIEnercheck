using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIEnercheck.Migrations
{
    /// <inheritdoc />
    public partial class Correcoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Planos_PlanoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosPagos_AspNetUsers_UsuarioId",
                table: "PlanosPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosPagos_Planos_PlanoId",
                table: "PlanosPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto");

            migrationBuilder.DropIndex(
                name: "IX_PlanosPagos_PlanoId",
                table: "PlanosPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planos",
                table: "Planos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PlanoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "QuantidadeReq",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "DataInicioPlano",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataVencimentoPlano",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlanoAtivo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserReq",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "dataInicio",
                table: "Projeto",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Projeto",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlanoPagoId",
                table: "PlanosPagos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuantidadeUsers",
                table: "Planos",
                newName: "QtdUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Projeto",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Projeto",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Progresso",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "PlanosPagos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PlanosPagos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanosPagos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PlanoId1",
                table: "PlanosPagos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Planos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Planos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Planos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Planos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Planos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QtdRequisicao",
                table: "Planos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlanoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssinaturaPlanoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanoId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planos",
                table: "Planos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssinaturaPlano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssinaturaPlano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssinaturaPlano_Planos_PlanoId1",
                        column: x => x.PlanoId1,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanosPagos_PlanoId1",
                table: "PlanosPagos",
                column: "PlanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AssinaturaPlanoId",
                table: "AspNetUsers",
                column: "AssinaturaPlanoId",
                unique: true,
                filter: "[AssinaturaPlanoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlanoId1",
                table: "AspNetUsers",
                column: "PlanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturaPlano_PlanoId1",
                table: "AssinaturaPlano",
                column: "PlanoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AssinaturaPlano_AssinaturaPlanoId",
                table: "AspNetUsers",
                column: "AssinaturaPlanoId",
                principalTable: "AssinaturaPlano",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Planos_PlanoId1",
                table: "AspNetUsers",
                column: "PlanoId1",
                principalTable: "Planos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosPagos_AspNetUsers_UsuarioId",
                table: "PlanosPagos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosPagos_Planos_PlanoId1",
                table: "PlanosPagos",
                column: "PlanoId1",
                principalTable: "Planos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AssinaturaPlano_AssinaturaPlanoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Planos_PlanoId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosPagos_AspNetUsers_UsuarioId",
                table: "PlanosPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanosPagos_Planos_PlanoId1",
                table: "PlanosPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto");

            migrationBuilder.DropTable(
                name: "AssinaturaPlano");

            migrationBuilder.DropIndex(
                name: "IX_PlanosPagos_PlanoId1",
                table: "PlanosPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planos",
                table: "Planos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AssinaturaPlanoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PlanoId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PlanosPagos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanosPagos");

            migrationBuilder.DropColumn(
                name: "PlanoId1",
                table: "PlanosPagos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "QtdRequisicao",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "AssinaturaPlanoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlanoId1",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Projeto",
                newName: "dataInicio");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projeto",
                newName: "ProjetoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlanosPagos",
                newName: "PlanoPagoId");

            migrationBuilder.RenameColumn(
                name: "QtdUsers",
                table: "Planos",
                newName: "QuantidadeUsers");

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
                name: "Status",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Progresso",
                table: "Projeto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projeto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "PlanosPagos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Planos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Planos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Planos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeReq",
                table: "Planos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PlanoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicioPlano",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimentoPlano",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoAtivo",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserReq",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planos",
                table: "Planos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosPagos_PlanoId",
                table: "PlanosPagos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlanoId",
                table: "AspNetUsers",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Planos_PlanoId",
                table: "AspNetUsers",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosPagos_AspNetUsers_UsuarioId",
                table: "PlanosPagos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanosPagos_Planos_PlanoId",
                table: "PlanosPagos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "PlanoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projeto_AspNetUsers_UsuarioId",
                table: "Projeto",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
