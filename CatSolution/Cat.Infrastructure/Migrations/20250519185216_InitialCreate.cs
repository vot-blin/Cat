using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_breeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GoldMedals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    SilverMedals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    BronzeMedals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PassportIssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Timetable = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Specializations = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ClubId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_experts_clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PedigreeNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ParentNames = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    LastVaccination = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Medal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsDisqualified = table.Column<bool>(type: "boolean", nullable: false),
                    BreedId = table.Column<int>(type: "integer", nullable: false),
                    ClubId = table.Column<int>(type: "integer", nullable: false),
                    RingId = table.Column<int>(type: "integer", nullable: true),
                    OwnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cats_breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cats_clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cats_owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cats_rings_RingId",
                        column: x => x.RingId,
                        principalTable: "rings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ring_breeds",
                columns: table => new
                {
                    RingId = table.Column<int>(type: "integer", nullable: false),
                    BreedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ring_breeds", x => new { x.RingId, x.BreedId });
                    table.ForeignKey(
                        name: "FK_ring_breeds_breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ring_breeds_rings_RingId",
                        column: x => x.RingId,
                        principalTable: "rings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expert_rings",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "integer", nullable: false),
                    RingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expert_rings", x => new { x.ExpertId, x.RingId });
                    table.ForeignKey(
                        name: "FK_expert_rings_experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expert_rings_rings_RingId",
                        column: x => x.RingId,
                        principalTable: "rings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_breeds_Name",
                table: "breeds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cats_BreedId",
                table: "cats",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_cats_ClubId",
                table: "cats",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_cats_OwnerId",
                table: "cats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_cats_PedigreeNumber",
                table: "cats",
                column: "PedigreeNumber",
                unique: true,
                filter: "\"PedigreeNumber\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_cats_RingId",
                table: "cats",
                column: "RingId");

            migrationBuilder.CreateIndex(
                name: "IX_expert_rings_RingId",
                table: "expert_rings",
                column: "RingId");

            migrationBuilder.CreateIndex(
                name: "IX_experts_ClubId",
                table: "experts",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_owners_PassportNumber",
                table: "owners",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ring_breeds_BreedId",
                table: "ring_breeds",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_rings_Number",
                table: "rings",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cats");

            migrationBuilder.DropTable(
                name: "expert_rings");

            migrationBuilder.DropTable(
                name: "ring_breeds");

            migrationBuilder.DropTable(
                name: "owners");

            migrationBuilder.DropTable(
                name: "experts");

            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "rings");

            migrationBuilder.DropTable(
                name: "clubs");
        }
    }
}
