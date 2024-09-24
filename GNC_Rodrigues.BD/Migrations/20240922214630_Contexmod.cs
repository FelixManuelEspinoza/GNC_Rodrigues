using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GNC_Rodrigues.BD.Migrations
{
    /// <inheritdoc />
    public partial class Contexmod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Dominio = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ClienteDNI = table.Column<string>(type: "nvarchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Dominio);
                    table.ForeignKey(
                        name: "FK_Vehiculo_clientes_ClienteDNI",
                        column: x => x.ClienteDNI,
                        principalTable: "clientes",
                        principalColumn: "DNI");
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoDominio = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    ClienteDNI = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fallas = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Presupuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresupuestoConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    Reparacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvisadoRetirar = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaOrden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionaNafta = table.Column<bool>(type: "bit", nullable: false),
                    CortaCorriente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orden_Vehiculo_VehiculoDominio",
                        column: x => x.VehiculoDominio,
                        principalTable: "Vehiculo",
                        principalColumn: "Dominio");
                    table.ForeignKey(
                        name: "FK_Orden_clientes_ClienteDNI",
                        column: x => x.ClienteDNI,
                        principalTable: "clientes",
                        principalColumn: "DNI");
                });

            migrationBuilder.CreateIndex(
                name: "Cliente_UQ_DNI",
                table: "clientes",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orden_ClienteDNI",
                table: "Orden",
                column: "ClienteDNI");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_VehiculoDominio",
                table: "Orden",
                column: "VehiculoDominio");

            migrationBuilder.CreateIndex(
                name: "Orden_UQ_Id",
                table: "Orden",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_ClienteDNI",
                table: "Vehiculo",
                column: "ClienteDNI");

            migrationBuilder.CreateIndex(
                name: "Vehiculo_UQ_Dominio",
                table: "Vehiculo",
                column: "Dominio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
