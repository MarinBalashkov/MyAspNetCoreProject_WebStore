using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Data.Migrations
{
    public partial class FullTextIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("create fulltext catalog MyCatalog;", true);
            //migrationBuilder.Sql("create fulltext index on dbo.Products (Name, Description) key index PK_Products on MyCatalog;", true);

            //migrationBuilder.Sql(
            //                 sql: "CREATE FULLTEXT CATALOG MyCatalog AS DEFAULT;", suppressTransaction: true);
            //migrationBuilder.Sql(
            //                 sql: "CREATE FULLTEXT INDEX ON dbo.Products(Name) KEY INDEX PK_Products on MyCatalog;",
            //                 suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
