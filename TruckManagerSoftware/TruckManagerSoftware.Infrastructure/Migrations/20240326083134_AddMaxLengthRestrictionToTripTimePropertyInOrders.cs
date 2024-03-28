using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckManagerSoftware.Infrastructure.Migrations
{
    public partial class AddMaxLengthRestrictionToTripTimePropertyInOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("35e9b241-fbb8-401a-99b0-67e06e5cbde6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f6bea029-72b5-4368-addb-f6852285f978"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("a204b0ca-adb3-472e-93ab-765ae5c507b4"));

            migrationBuilder.DeleteData(
                table: "BankContacts",
                keyColumn: "Id",
                keyValue: new Guid("e2dd672e-6c70-4dd4-adb0-005c6fa69fa2"));

            migrationBuilder.AlterColumn<string>(
                name: "TripTime",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "TripTime",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "GarageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrderId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TruckId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("35e9b241-fbb8-401a-99b0-67e06e5cbde6"), 0, null, "0c8aa95c-1cd3-41d0-9e82-2e608c8adef7", "user@mail.com", false, null, false, null, "USER@MAIL.COM", "USER@MAIL.COM", null, "AQAAAAEAACcQAAAAEPH/O4l/jV467uv24Nsyh5QkwMQPB8dekJejXqSYyCrnbz1vQ51voB4WOOV2isWbcg==", null, false, null, "roaming", null, false, "user@mail.com" },
                    { new Guid("f6bea029-72b5-4368-addb-f6852285f978"), 0, null, "5bdf669a-1adc-4431-8b0b-519e74cd26b1", "administrator@mail.com", false, null, false, null, "ADMINISTRATOR@MAIL.COM", "ADMINISTRATOR@MAIL.COM", null, "AQAAAAEAACcQAAAAEJ4FnEJKV27O+maVFK8+xDXTYNePqi/JQMVd/JXqaTU8OmE4P0Lg55hN4Fj5ed/JAA==", null, false, null, "roaming", null, false, "administrator@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "BankContacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("a204b0ca-adb3-472e-93ab-765ae5c507b4"), "unicredit@unicredit.com", "Unicredi Bulbank", "1234567890" },
                    { new Guid("e2dd672e-6c70-4dd4-adb0-005c6fa69fa2"), "dsk@dsk.com", "DSK", "0123456789" }
                });
        }
    }
}
