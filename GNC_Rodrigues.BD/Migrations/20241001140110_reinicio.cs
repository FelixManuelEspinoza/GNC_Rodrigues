using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GNC_Rodrigues.BD.Migrations
{
    /// <inheritdoc />
    public partial class reinicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Dominio = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ClienteDNI = table.Column<string>(type: "nvarchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Dominio);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Clientes_ClienteDNI",
                        column: x => x.ClienteDNI,
                        principalTable: "Clientes",
                        principalColumn: "DNI");
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
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
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Clientes_ClienteDNI",
                        column: x => x.ClienteDNI,
                        principalTable: "Clientes",
                        principalColumn: "DNI");
                    table.ForeignKey(
                        name: "FK_Ordenes_Vehiculos_VehiculoDominio",
                        column: x => x.VehiculoDominio,
                        principalTable: "Vehiculos",
                        principalColumn: "Dominio");
                });

            migrationBuilder.CreateIndex(
                name: "Cliente_UQ_DNI",
                table: "Clientes",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteDNI",
                table: "Ordenes",
                column: "ClienteDNI");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_VehiculoDominio",
                table: "Ordenes",
                column: "VehiculoDominio");

            migrationBuilder.CreateIndex(
                name: "Orden_UQ_Id",
                table: "Ordenes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ClienteDNI",
                table: "Vehiculos",
                column: "ClienteDNI");

            migrationBuilder.CreateIndex(
                name: "Vehiculo_UQ_Dominio",
                table: "Vehiculos",
                column: "Dominio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
