using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaClienteFinal.API.Migrations
{
    /// <inheritdoc />
    public partial class VersaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropostaCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Rendimento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostaCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proposta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ValorOperacao = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AgenteId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposta_Agente_AgenteId",
                        column: x => x.AgenteId,
                        principalTable: "Agente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposta_AgenteId",
                table: "Proposta",
                column: "AgenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposta");

            migrationBuilder.DropTable(
                name: "PropostaCliente");

            migrationBuilder.DropTable(
                name: "Agente");
        }
    }
}
