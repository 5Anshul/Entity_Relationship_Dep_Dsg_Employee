using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBApi_Dep_Dep_Employee.Migrations
{
    public partial class AddDepDsgData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert Departments Values('Accounts')");
            migrationBuilder.Sql("insert Departments Values('Sales')");
            migrationBuilder.Sql("insert Departments Values('Marketing')");
            migrationBuilder.Sql("insert Departments Values('Asp.net')");
            migrationBuilder.Sql("insert Departments Values('React')");

            migrationBuilder.Sql("insert Designations Values('Manager')");
            migrationBuilder.Sql("insert Designations Values('Team Leader')");
            migrationBuilder.Sql("insert Designations Values('Programmer')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
