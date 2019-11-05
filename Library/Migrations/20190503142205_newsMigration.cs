using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class newsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    newsTitle = table.Column<string>(nullable: false),
                    newsContent = table.Column<string>(nullable: false),
                    newsDate = table.Column<string>(nullable: false),
                    newsImage = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
