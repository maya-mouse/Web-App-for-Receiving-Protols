using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetworkingReferenceBasics.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DnsSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDnsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDynamicDnsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    InterfaceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuppotingIp4 = table.Column<bool>(type: "bit", nullable: false),
                    SuppotingIp6 = table.Column<bool>(type: "bit", nullable: false),
                    isRecieveOnly = table.Column<bool>(type: "bit", nullable: false),
                    Multicast = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adapters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adapters");
        }
    }
}
