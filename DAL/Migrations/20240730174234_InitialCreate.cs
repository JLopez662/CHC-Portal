using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administrativos",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacturacionBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IVU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserSuri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassSuri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEftps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassEftps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCFSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassCFSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCofim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassCofim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poliza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegComerciante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Choferil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dir = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Patronal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SSN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Incorporacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Operaciones = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Industria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NAICS = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirFisica = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DirPostal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accionista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSNA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicConducir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumRuta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankClientS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumRutaS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameBankS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCuentaS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarjeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoTarjeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostalBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameCardS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarjetaS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoTarjetaS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiracionS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostalBankS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MID = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Users");

            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
