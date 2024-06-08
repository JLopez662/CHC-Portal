using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EID);
                });

            migrationBuilder.CreateTable(
                name: "Individuos",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuos", x => x.SSN);
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Administrativos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Facturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacturacionBase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IVU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrativos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Administrativos_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Confidenciales",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSuri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassSuri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEftps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassEftps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIN = table.Column<int>(type: "int", nullable: false),
                    UserCFSE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassCFSE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassDept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCofim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassCofim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confidenciales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Confidenciales_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributivos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poliza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegComerciante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Choferil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptEstado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributivos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contributivos_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demograficos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Incorporacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Operaciones = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Industria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAICS = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirFisica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demograficos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Demograficos_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identificaciones",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accionista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSNA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicConducir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identificaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Identificaciones_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumRuta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameBank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankClientS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BancoS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumRutaS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameBankS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCuentaS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarjeta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoTarjeta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostalBank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameCardS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TarjetaS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoTarjetaS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVVS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiracionS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostalBankS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pagos_Registros_ID",
                        column: x => x.ID,
                        principalTable: "Registros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrativos");

            migrationBuilder.DropTable(
                name: "Confidenciales");

            migrationBuilder.DropTable(
                name: "Contributivos");

            migrationBuilder.DropTable(
                name: "Demograficos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Identificaciones");

            migrationBuilder.DropTable(
                name: "Individuos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
