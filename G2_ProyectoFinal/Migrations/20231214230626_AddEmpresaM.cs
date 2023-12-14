using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G2_ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresaM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    numTelefono = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__3214EC27565927F9", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lenguaje",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lenguaje__3214EC279814DD58", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MetodoPa__3214EC27B1118747", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Moneda__3214EC27C5CA662D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Provinci__3214EC279AB67F5B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Canton",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ProvinciaID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Canton__3214EC2702C0D1FE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROVINCIA",
                        column: x => x.ProvinciaID,
                        principalTable: "Provincia",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Cedula = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    numTelefono = table.Column<int>(type: "int", nullable: true),
                    EmpresaID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    ProvinciaID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    CantonID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__3214EC2716FB0813", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CANTON",
                        column: x => x.CantonID,
                        principalTable: "Canton",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EMPRESA",
                        column: x => x.EmpresaID,
                        principalTable: "Empresa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PROVINCIA_CLIENTE",
                        column: x => x.ProvinciaID,
                        principalTable: "Provincia",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    ClienteID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    LenguajeID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    MonedaID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    MetodoPagoID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transacc__3214EC2719A57EC1", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CLIENTE",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LENGUAJE",
                        column: x => x.LenguajeID,
                        principalTable: "Lenguaje",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_METODOPAGO",
                        column: x => x.MetodoPagoID,
                        principalTable: "MetodoPago",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MONEDA",
                        column: x => x.MonedaID,
                        principalTable: "Moneda",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canton_ProvinciaID",
                table: "Canton",
                column: "ProvinciaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CantonID",
                table: "Cliente",
                column: "CantonID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EmpresaID",
                table: "Cliente",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProvinciaID",
                table: "Cliente",
                column: "ProvinciaID");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClienteID",
                table: "Transacciones",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_LenguajeID",
                table: "Transacciones",
                column: "LenguajeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_MetodoPagoID",
                table: "Transacciones",
                column: "MetodoPagoID");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_MonedaID",
                table: "Transacciones",
                column: "MonedaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Lenguaje");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropTable(
                name: "Canton");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}
