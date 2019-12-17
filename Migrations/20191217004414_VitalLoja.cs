using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Migrations
{
    public partial class VitalLoja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoProduto",
                columns: table => new
                {
                    tipoProdutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true),
                    qtd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProduto", x => x.tipoProdutoId);
                });

            migrationBuilder.CreateTable(
                name: "Confeccao",
                columns: table => new
                {
                    confeccaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tipoProdutoId = table.Column<int>(nullable: false),
                    data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confeccao", x => x.confeccaoId);
                    table.ForeignKey(
                        name: "FK_Confeccao_TipoProduto_tipoProdutoId",
                        column: x => x.tipoProdutoId,
                        principalTable: "TipoProduto",
                        principalColumn: "tipoProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Confeccao_tipoProdutoId",
                table: "Confeccao",
                column: "tipoProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confeccao");

            migrationBuilder.DropTable(
                name: "TipoProduto");
        }
    }
}
