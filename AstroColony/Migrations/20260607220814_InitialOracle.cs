using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstroColony.Migrations
{
    /// <inheritdoc />
    public partial class InitialOracle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tripulantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Funcao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    StatusSaude = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tripulantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    QuantidadeAtual = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ConsumoMedioDiario = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    ResponsavelId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensEstoque_Tripulantes_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Tripulantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensEstoque_ResponsavelId",
                table: "ItensEstoque",
                column: "ResponsavelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensEstoque");

            migrationBuilder.DropTable(
                name: "Tripulantes");
        }
    }
}
