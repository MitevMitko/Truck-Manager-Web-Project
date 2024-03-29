using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckManagerSoftware.Infrastructure.Migrations
{
    public partial class ChangeIdPropertyValuesForBankContactAndUserEntitiesInConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7532cb3c-9fbc-4438-b6b0-94c70683dc74"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("de63b802-02b2-4e92-9a85-79093bf0b3c1"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("c0fec262-d6b1-41fc-8967-727671284857"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("f40a3e7f-8b27-44cd-93e5-735ca247be61"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "GarageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrderId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TruckId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("119ca1f9-3f45-4391-a92e-408dce588da6"), 0, null, "501af2d3-d709-4a1c-923f-157fde09e423", "user@mail.com", false, null, false, null, "USER@MAIL.COM", "USER@MAIL.COM", null, "AQAAAAEAACcQAAAAEIhMSXSpyYdjxcYT98AlUdtSqFmcc45r2Wm/lCer5QNGJf7vytMR4DVHrs11aFRlZQ==", null, false, null, "roaming", null, false, "user@mail.com" },
                    { new Guid("71fb597c-02f6-4faa-909d-e25e60e8e4e7"), 0, null, "80d61c85-b4c8-42d0-9be0-57b6ea26dcfc", "administrator@mail.com", false, null, false, null, "ADMINISTRATOR@MAIL.COM", "ADMINISTRATOR@MAIL.COM", null, "AQAAAAEAACcQAAAAECEnxouloCK7lzluwlYzmokFhy36ZCbTvvhtoS35S447JPfMnA5oXzeZmRfpQfo3mQ==", null, false, null, "roaming", null, false, "administrator@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "BankContacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("bfd81dcf-f66a-4e88-a534-7b58ba4681b6"), "dsk@dsk.com", "DSK", "0123456789" },
                    { new Guid("f0b0dc9a-5826-4dfd-9aa4-cad5a902268b"), "unicredit@unicredit.com", "Unicredi Bulbank", "1234567890" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("119ca1f9-3f45-4391-a92e-408dce588da6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("71fb597c-02f6-4faa-909d-e25e60e8e4e7"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("bfd81dcf-f66a-4e88-a534-7b58ba4681b6"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("f0b0dc9a-5826-4dfd-9aa4-cad5a902268b"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "GarageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrderId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TruckId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("7532cb3c-9fbc-4438-b6b0-94c70683dc74"), 0, null, "b2829ee6-ed7a-4050-a1c3-5bc7cd0111f6", "user@mail.com", false, null, false, null, "USER@MAIL.COM", "USER@MAIL.COM", null, "AQAAAAEAACcQAAAAEFCRausQGYeM78c/9nAOzRKJZnWnqOr62xgnBWajU8vDkrIBLz3DyuuQLMZC3NZXUA==", null, false, null, "roaming", null, false, "user@mail.com" },
                    { new Guid("de63b802-02b2-4e92-9a85-79093bf0b3c1"), 0, null, "43fc7ac3-1abc-45ea-a9b4-4741e6577177", "administrator@mail.com", false, null, false, null, "ADMINISTRATOR@MAIL.COM", "ADMINISTRATOR@MAIL.COM", null, "AQAAAAEAACcQAAAAEMWs+MYeNQL9JnGqBRxW8khv433p8FSloLsVFh9CUzIOFxR1Yv5knoMCxW/heY2RlA==", null, false, null, "roaming", null, false, "administrator@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "BankContacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("c0fec262-d6b1-41fc-8967-727671284857"), "dsk@dsk.com", "DSK", "0123456789" },
                    { new Guid("f40a3e7f-8b27-44cd-93e5-735ca247be61"), "unicredit@unicredit.com", "Unicredi Bulbank", "1234567890" }
                });
        }
    }
}
