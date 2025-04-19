using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetworkingReferenceBasics.Migrations
{
    /// <inheritdoc />
    public partial class Addingprotocolsinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ethernetProtocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceHardwareAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationHardwareAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ethernetProtocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ipProtocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderLenght = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeToLive = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ipProtocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tcpProtocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourcePort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationPort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tcpProtocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "udpProtocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourcePort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationPort = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udpProtocols", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ethernetProtocols");

            migrationBuilder.DropTable(
                name: "ipProtocols");

            migrationBuilder.DropTable(
                name: "tcpProtocols");

            migrationBuilder.DropTable(
                name: "udpProtocols");
        }
    }
}
