using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillMate.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BukyoungStatuses_Patients_PatientId",
                table: "BukyoungStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_BukyoungStatuses_Patients_PatientId",
                table: "BukyoungStatuses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BukyoungStatuses_Patients_PatientId",
                table: "BukyoungStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_BukyoungStatuses_Patients_PatientId",
                table: "BukyoungStatuses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
