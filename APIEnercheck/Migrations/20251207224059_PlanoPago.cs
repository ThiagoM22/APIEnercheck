using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIEnercheck.Migrations
{
    /// <inheritdoc />
    public partial class PlanoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Planos");

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

            migrationBuilder.CreateTable(
                name: "PlanosPagos",
                columns: table => new
                {
                    PlanoPagoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanoId = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosPagos", x => x.PlanoPagoId);
                    table.ForeignKey(
                        name: "FK_PlanosPagos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanosPagos_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanosPagos_PlanoId",
                table: "PlanosPagos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosPagos_UsuarioId",
                table: "PlanosPagos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanosPagos");

            migrationBuilder.DropColumn(
                name: "DataInicioPlano",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataVencimentoPlano",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlanoAtivo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Planos",
                type: "bit",
                nullable: true);
        }
    }
}
