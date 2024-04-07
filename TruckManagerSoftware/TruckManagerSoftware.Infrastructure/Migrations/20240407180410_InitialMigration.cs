using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckManagerSoftware.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PowerHp = table.Column<int>(type: "int", nullable: false),
                    PowerKw = table.Column<int>(type: "int", nullable: false),
                    TorqueNm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CargoWeight = table.Column<int>(type: "int", nullable: false),
                    StartPoint = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EndPoint = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DeliveryType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TripDistance = table.Column<int>(type: "int", nullable: false),
                    TripTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DeliveryPrice = table.Column<int>(type: "int", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GearsCount = table.Column<int>(type: "int", nullable: false),
                    Retarder = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Series = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrailerType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BodyType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TareWeight = table.Column<int>(type: "int", nullable: false),
                    AxleCount = table.Column<int>(type: "int", nullable: false),
                    TotalLength = table.Column<double>(type: "float", nullable: false),
                    CargoTypes = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GarageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trailers_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Series = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DrivenDistance = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GarageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrailerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EngineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trucks_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trucks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trucks_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trucks_Transmissions_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "Transmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GarageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "GarageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrderId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TruckId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("119ca1f9-3f45-4391-a92e-408dce588da6"), 0, null, "9d578ac4-b91f-4fc0-ba10-bb92704b75cf", "user@mail.com", false, null, false, null, "USER@MAIL.COM", "USER", null, "AQAAAAEAACcQAAAAEJFrjv3kX0JrSEfH9Ul0cNz2J/fBagHiDOD/5gI32v8MPJWUeLspdIK33/CI1F9S9g==", null, false, "AQAAAAEAACcQAAAAEHRPDwxLCimalXWz3qV0/+CY2jextE/Pw9RTwtEO8aBcYhUe00K3be8eZPy4Z23JCg==", "roaming", null, false, "user" },
                    { new Guid("71fb597c-02f6-4faa-909d-e25e60e8e4e7"), 0, null, "4c9ba500-41d9-4b73-9ded-a612d597c42a", "administrator@mail.com", false, null, false, null, "ADMINISTRATOR@MAIL.COM", "ADMINISTRATOR", null, "AQAAAAEAACcQAAAAEL0gZLK5MJUHSLk18C67ZvAeMiM3uDZJjOkuUs/eoLkxahhGCtzYqK7ahP80Yx079A==", null, false, "AQAAAAEAACcQAAAAECQHfTAwNUaRvO451trA3Hfjtvu9qJ5/n31AEUQYr+gzuHD80TDJCP7CPNo39kNveQ==", "roaming", null, false, "administrator" }
                });

            migrationBuilder.InsertData(
                table: "BankContacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("bfd81dcf-f66a-4e88-a534-7b58ba4681b6"), "dsk@dsk.com", "DSK", "0123456789" },
                    { new Guid("f0b0dc9a-5826-4dfd-9aa4-cad5a902268b"), "unicredit@unicredit.com", "Unicredi Bulbank", "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "PowerHp", "PowerKw", "Title", "TorqueNm" },
                values: new object[,]
                {
                    { new Guid("57677635-5723-437d-8a94-3d26f51cd0f8"), 580, 427, "DC16 102 580 Euro 6 V8", 2950 },
                    { new Guid("682f1317-f1d8-46c4-b7ec-13af1ee27906"), 560, 412, "DC16 18 560 Euro 5 V8", 2700 },
                    { new Guid("7f02705e-b364-4a2d-8b7a-734458317e5d"), 360, 265, "DC13 114 360 Euro 5", 1850 }
                });

            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "City", "Country", "Size" },
                values: new object[,]
                {
                    { new Guid("16d31ab1-2b09-44a0-ae5e-0c1526078157"), "Venice", "Italy", "medium" },
                    { new Guid("54779e9a-eb54-491a-b442-78dcff15462f"), "Varna", "Bulgaria", "large" },
                    { new Guid("58197c1b-2059-4382-a956-aecb0d834217"), "Burgas", "Bulgaria", "large" },
                    { new Guid("c17a0f07-e39c-4420-a338-3f7b15a15f59"), "Berlin", "Germany", "large" },
                    { new Guid("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"), "Ruse", "Bulgaria", "small" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Cargo", "CargoWeight", "DeliveryPrice", "DeliveryType", "EndPoint", "StartPoint", "TripDistance", "TripTime", "TruckId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3496d7fc-c71a-43e6-b1bf-cf4c4082feac"), "Oil", 23, 32273, "important", "Mannheim", "Vienna", 1045, "15h 18m", new Guid("6a647b36-271f-4434-a152-2548f8a2ff0e"), null },
                    { new Guid("432f8be9-b405-4cce-a090-df244c49beda"), "Pesto", 18, 28663, "standard", "Frankfurt", "Dresden", 864, "12h 40m", null, null },
                    { new Guid("5ccb7fe1-1c13-4dd0-8dec-12867917581c"), "Beans", 12, 95493, "urgent", "Grimsby", "Bratislava", 1824, "26h 12m", new Guid("7d380a02-1932-4c39-b2d6-cd58678fd442"), null }
                });

            migrationBuilder.InsertData(
                table: "Transmissions",
                columns: new[] { "Id", "GearsCount", "Retarder", "Title" },
                values: new object[,]
                {
                    { new Guid("b5f61d07-9576-491a-a337-809a31268a17"), 14, false, "Opticruise GRSO 925" },
                    { new Guid("e1663ec8-2b8c-4782-910c-435081921fac"), 12, false, "Opticruise GRS 905" },
                    { new Guid("e71a4b60-1ed1-4982-b900-70a76f0706a8"), 14, true, "Opticruise GRSO 925R" }
                });

            migrationBuilder.InsertData(
                table: "Trailers",
                columns: new[] { "Id", "AxleCount", "BodyType", "CargoTypes", "GarageId", "Image", "Series", "TareWeight", "Title", "TotalLength", "TrailerType", "TruckId" },
                values: new object[,]
                {
                    { new Guid("35328ace-d3cb-4208-8e48-358eb5905ae1"), 3, "Dumper", "Bulk cargo and materials", new Guid("16d31ab1-2b09-44a0-ae5e-0c1526078157"), "02403d2f-9448-4e4a-b84c-6ca9377ccb7d", "DMP", 6650, "Steel Dumper", 9.0999999999999996, "Single", null },
                    { new Guid("3c7e1a88-4c69-46c2-915f-3763f97c7fe5"), 3, "Container Carrier", "Containers and container tanks", new Guid("16d31ab1-2b09-44a0-ae5e-0c1526078157"), "6dcf5eef-e16d-4fbb-98c0-7b4d145e7fa6", "CNT", 5100, "Container Carrier", 12.4, "Single", new Guid("6a647b36-271f-4434-a152-2548f8a2ff0e") },
                    { new Guid("3ce51feb-0d77-4f61-aeb0-c44a4b0540d3"), 2, "Flatbed", "Construction equipment and materials", new Guid("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"), "fadd6754-0ce5-430a-a13d-bdaed1e201c4", "FLB", 5300, "Wooden Floor Flatbed", 13.699999999999999, "Single", new Guid("7d380a02-1932-4c39-b2d6-cd58678fd442") },
                    { new Guid("928604bb-8f63-4b15-8bb6-fda54428c3a8"), 2, "Curtainsider", "General, Dry goods", new Guid("c17a0f07-e39c-4420-a338-3f7b15a15f59"), "ada888de-4db3-4212-b50f-92fed8ca5874", "STD", 5860, "Curtainsider", 13.699999999999999, "single", new Guid("4ccf808a-2db5-4d36-82f0-e6ff4a1f8b4b") }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "Brand", "DrivenDistance", "EngineId", "GarageId", "Image", "OrderId", "Series", "TrailerId", "TransmissionId", "UserId" },
                values: new object[] { new Guid("4ccf808a-2db5-4d36-82f0-e6ff4a1f8b4b"), "Renault", 25.0, new Guid("57677635-5723-437d-8a94-3d26f51cd0f8"), new Guid("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"), "0692f00d-1c3e-49c1-be3f-804b2bae48d7", null, "Premium", new Guid("928604bb-8f63-4b15-8bb6-fda54428c3a8"), new Guid("e71a4b60-1ed1-4982-b900-70a76f0706a8"), null });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "Brand", "DrivenDistance", "EngineId", "GarageId", "Image", "OrderId", "Series", "TrailerId", "TransmissionId", "UserId" },
                values: new object[] { new Guid("6a647b36-271f-4434-a152-2548f8a2ff0e"), "DAF", 20.0, new Guid("682f1317-f1d8-46c4-b7ec-13af1ee27906"), new Guid("16d31ab1-2b09-44a0-ae5e-0c1526078157"), "16a38f92-f19f-4480-bdc9-769e3f660456", new Guid("3496d7fc-c71a-43e6-b1bf-cf4c4082feac"), "XD", new Guid("3c7e1a88-4c69-46c2-915f-3763f97c7fe5"), new Guid("b5f61d07-9576-491a-a337-809a31268a17"), null });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "Brand", "DrivenDistance", "EngineId", "GarageId", "Image", "OrderId", "Series", "TrailerId", "TransmissionId", "UserId" },
                values: new object[] { new Guid("7d380a02-1932-4c39-b2d6-cd58678fd442"), "Scania", 15.0, new Guid("7f02705e-b364-4a2d-8b7a-734458317e5d"), new Guid("e1945cc7-f084-4c9c-b0a1-0e7824d6bc9b"), "22266a8f-caf6-404f-bd66-d87228595cda", new Guid("5ccb7fe1-1c13-4dd0-8dec-12867917581c"), "R", new Guid("3ce51feb-0d77-4f61-aeb0-c44a4b0540d3"), new Guid("e1663ec8-2b8c-4782-910c-435081921fac"), null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GarageId",
                table: "AspNetUsers",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrderId",
                table: "AspNetUsers",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TruckId",
                table: "AspNetUsers",
                column: "TruckId",
                unique: true,
                filter: "[TruckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_GarageId",
                table: "Trailers",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_EngineId",
                table: "Trucks",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_GarageId",
                table: "Trucks",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_OrderId",
                table: "Trucks",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TrailerId",
                table: "Trucks",
                column: "TrailerId",
                unique: true,
                filter: "[TrailerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TransmissionId",
                table: "Trucks",
                column: "TransmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BankContacts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Transmissions");

            migrationBuilder.DropTable(
                name: "Garages");
        }
    }
}
