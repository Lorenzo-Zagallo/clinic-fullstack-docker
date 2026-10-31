using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$ZJJmKBXhAjcvAXEBHXlIHunpQr0GEydDHj3PlN.s8Y/bTFRV6RinW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$d.W56ePArXBfKfYViJDjK.LCqFoxmyG7PFoVqQjTnMTIDttLapcVS");
        }
    }
}
