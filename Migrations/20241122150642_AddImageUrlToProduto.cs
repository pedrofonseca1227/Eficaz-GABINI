using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crudd.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_produto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    DataCadastro = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    ImagemUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__produtos__9C8800E3FBF083B6", x => x.ProdutoId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    register_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    security_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    nacionalidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F4A3E27B5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    id_endereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cep = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    rua = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    numero = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    bairro = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    complemento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    tipo_residencia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    cidade = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    pais = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    notas = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__endereco__324B959E43154A40", x => x.id_endereco);
                    table.ForeignKey(
                        name: "FK_Endereco_User",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_endereco_UserId",
                table: "endereco",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__users__D8D98E82EFBC0932",
                table: "users",
                column: "senha",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
