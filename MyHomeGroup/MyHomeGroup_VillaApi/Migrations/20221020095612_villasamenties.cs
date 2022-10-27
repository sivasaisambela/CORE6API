using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHomeGroup_VillaApi.Migrations
{
    public partial class villasamenties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JaggingCycleTrack = table.Column<bool>(type: "bit", nullable: false),
                    ClubHouseWithWater = table.Column<bool>(type: "bit", nullable: false),
                    PartyLawn = table.Column<bool>(type: "bit", nullable: false),
                    SwimmingPool = table.Column<bool>(type: "bit", nullable: false),
                    FitnessStation = table.Column<bool>(type: "bit", nullable: false),
                    AmphiTheater = table.Column<bool>(type: "bit", nullable: false),
                    SkatingRank = table.Column<bool>(type: "bit", nullable: false),
                    ChildrensPlayArea = table.Column<bool>(type: "bit", nullable: false),
                    TennisCourt = table.Column<bool>(type: "bit", nullable: false),
                    CricketNets = table.Column<bool>(type: "bit", nullable: false),
                    BasketballCourt = table.Column<bool>(type: "bit", nullable: false),
                    YogaMeditationLawn = table.Column<bool>(type: "bit", nullable: false),
                    DogsPark = table.Column<bool>(type: "bit", nullable: false),
                    Duplex = table.Column<bool>(type: "bit", nullable: false),
                    Triplex = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceRangeStarts = table.Column<double>(type: "float", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    AmentiesId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villas_Amenties_AmentiesId",
                        column: x => x.AmentiesId,
                        principalTable: "Amenties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Villas_AmentiesId",
                table: "Villas",
                column: "AmentiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");

            migrationBuilder.DropTable(
                name: "Amenties");
        }
    }
}
