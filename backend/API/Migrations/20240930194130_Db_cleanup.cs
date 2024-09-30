using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Db_cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takeaways_Books_FK_BookId",
                table: "Takeaways");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_UserId",
                table: "UserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks");

            migrationBuilder.DropTable(
                name: "BookBookChangeHistory");

            migrationBuilder.DropTable(
                name: "Takeaway");

            migrationBuilder.DropTable(
                name: "BookChangeHistory");

            migrationBuilder.DropIndex(
                name: "IX_Takeaways_FK_BookId",
                table: "Takeaways");

            migrationBuilder.DropColumn(
                name: "AuthorKey",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserBooks",
                newName: "FK_User_Id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "UserBooks",
                newName: "FK_Book_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_UserId",
                table: "UserBooks",
                newName: "IX_UserBooks_FK_User_Id");

            migrationBuilder.RenameColumn(
                name: "Heading",
                table: "Takeaways",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FK_BookId",
                table: "Takeaways",
                newName: "FK_Book_Id");

            migrationBuilder.RenameColumn(
                name: "SaveError",
                table: "Books",
                newName: "TakeawaysHeading");

            migrationBuilder.RenameColumn(
                name: "FK_TakeawaysId",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Takeaways",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Episode",
                table: "Takeaways",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lesson",
                table: "Takeaways",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Authors",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Takeaways_BookId",
                table: "Takeaways",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Takeaways_Books_BookId",
                table: "Takeaways",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_FK_User_Id",
                table: "UserBooks",
                column: "FK_User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_FK_Book_Id",
                table: "UserBooks",
                column: "FK_Book_Id",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Takeaways_Books_BookId",
                table: "Takeaways");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_FK_User_Id",
                table: "UserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_FK_Book_Id",
                table: "UserBooks");

            migrationBuilder.DropIndex(
                name: "IX_Takeaways_BookId",
                table: "Takeaways");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Takeaways");

            migrationBuilder.DropColumn(
                name: "Episode",
                table: "Takeaways");

            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "Takeaways");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "FK_User_Id",
                table: "UserBooks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "FK_Book_Id",
                table: "UserBooks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_FK_User_Id",
                table: "UserBooks",
                newName: "IX_UserBooks_UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Takeaways",
                newName: "Heading");

            migrationBuilder.RenameColumn(
                name: "FK_Book_Id",
                table: "Takeaways",
                newName: "FK_BookId");

            migrationBuilder.RenameColumn(
                name: "TakeawaysHeading",
                table: "Books",
                newName: "SaveError");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "FK_TakeawaysId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorKey",
                table: "Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "BookChangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChangeHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Takeaway",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakeawaysId = table.Column<int>(type: "int", nullable: false),
                    Episode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lesson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takeaway", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takeaway_Takeaways_TakeawaysId",
                        column: x => x.TakeawaysId,
                        principalTable: "Takeaways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBookChangeHistory",
                columns: table => new
                {
                    BookChangeHistoryId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookChangeHistory", x => new { x.BookChangeHistoryId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookBookChangeHistory_BookChangeHistory_BookChangeHistoryId",
                        column: x => x.BookChangeHistoryId,
                        principalTable: "BookChangeHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookChangeHistory_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Takeaways_FK_BookId",
                table: "Takeaways",
                column: "FK_BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookBookChangeHistory_BookId",
                table: "BookBookChangeHistory",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Takeaway_TakeawaysId",
                table: "Takeaway",
                column: "TakeawaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Takeaways_Books_FK_BookId",
                table: "Takeaways",
                column: "FK_BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_UserId",
                table: "UserBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
