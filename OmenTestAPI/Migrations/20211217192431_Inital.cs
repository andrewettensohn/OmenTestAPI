using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmenTestAPI.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SlotSpacesRequired = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerRequirement = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageType = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    Armor = table.Column<int>(type: "INTEGER", nullable: false),
                    Shield = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Volley = table.Column<int>(type: "INTEGER", nullable: false),
                    Range = table.Column<int>(type: "INTEGER", nullable: false),
                    MissileSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Stealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Sensor = table.Column<int>(type: "INTEGER", nullable: false),
                    IsIllegal = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarshipClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Slots = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseStealth = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarshipClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarshipHulls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Hitpoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialAbility = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarshipHulls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Starships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HullId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StarshipClassId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Starships_StarshipClasses_StarshipClassId",
                        column: x => x.StarshipClassId,
                        principalTable: "StarshipClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Starships_StarshipHulls_HullId",
                        column: x => x.HullId,
                        principalTable: "StarshipHulls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipModuleStarship",
                columns: table => new
                {
                    ModulesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StarshipsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipModuleStarship", x => new { x.ModulesId, x.StarshipsId });
                    table.ForeignKey(
                        name: "FK_ShipModuleStarship_ShipModules_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "ShipModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipModuleStarship_Starships_StarshipsId",
                        column: x => x.StarshipsId,
                        principalTable: "Starships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShipModules",
                columns: new[] { "Id", "Armor", "Category", "Damage", "DamageType", "Description", "IsIllegal", "MissileSpeed", "Name", "PowerRequirement", "Range", "Sensor", "Shield", "SlotSpacesRequired", "Speed", "Stealth", "Value", "Volley" },
                values: new object[] { new Guid("6df60c58-d182-4a3b-88b1-6453d12648f2"), 0, 7, 0, 0, "A section of the ship's hull that is meant to have a module installed.", false, 0, "Empty Module Slot", 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_ShipModuleStarship_StarshipsId",
                table: "ShipModuleStarship",
                column: "StarshipsId");

            migrationBuilder.CreateIndex(
                name: "IX_Starships_HullId",
                table: "Starships",
                column: "HullId");

            migrationBuilder.CreateIndex(
                name: "IX_Starships_StarshipClassId",
                table: "Starships",
                column: "StarshipClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipModuleStarship");

            migrationBuilder.DropTable(
                name: "ShipModules");

            migrationBuilder.DropTable(
                name: "Starships");

            migrationBuilder.DropTable(
                name: "StarshipClasses");

            migrationBuilder.DropTable(
                name: "StarshipHulls");
        }
    }
}
