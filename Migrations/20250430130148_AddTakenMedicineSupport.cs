using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillMate.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTakenMedicineSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Yank_Num",
                table: "Pills",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Yank_Name",
                table: "Pills",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Room",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_PhoneNumber",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_No",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Name",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Gender",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Bohoja_PhoneNumber",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Bohoja_Name",
                table: "Patients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Hwanja_Age",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_No",
                table: "BukyoungStatuses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Name",
                table: "BukyoungStatuses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Bukyoung_At",
                table: "BukyoungStatuses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TakenMedicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PillId = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenMedicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakenMedicines_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenMedicines_Pills_PillId",
                        column: x => x.PillId,
                        principalTable: "Pills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TakenMedicines_PatientId",
                table: "TakenMedicines",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenMedicines_PillId",
                table: "TakenMedicines",
                column: "PillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakenMedicines");

            migrationBuilder.DropColumn(
                name: "Hwanja_Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Bukyoung_At",
                table: "BukyoungStatuses");

            migrationBuilder.UpdateData(
                table: "Pills",
                keyColumn: "Yank_Num",
                keyValue: null,
                column: "Yank_Num",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Yank_Num",
                table: "Pills",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Pills",
                keyColumn: "Yank_Name",
                keyValue: null,
                column: "Yank_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Yank_Name",
                table: "Pills",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Hwanja_Room",
                keyValue: null,
                column: "Hwanja_Room",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Room",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Hwanja_PhoneNumber",
                keyValue: null,
                column: "Hwanja_PhoneNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_PhoneNumber",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Hwanja_No",
                keyValue: null,
                column: "Hwanja_No",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_No",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Hwanja_Name",
                keyValue: null,
                column: "Hwanja_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Name",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Hwanja_Gender",
                keyValue: null,
                column: "Hwanja_Gender",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Gender",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Bohoja_PhoneNumber",
                keyValue: null,
                column: "Bohoja_PhoneNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Bohoja_PhoneNumber",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Bohoja_Name",
                keyValue: null,
                column: "Bohoja_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Bohoja_Name",
                table: "Patients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "BukyoungStatuses",
                keyColumn: "Hwanja_No",
                keyValue: null,
                column: "Hwanja_No",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_No",
                table: "BukyoungStatuses",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "BukyoungStatuses",
                keyColumn: "Hwanja_Name",
                keyValue: null,
                column: "Hwanja_Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Hwanja_Name",
                table: "BukyoungStatuses",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
