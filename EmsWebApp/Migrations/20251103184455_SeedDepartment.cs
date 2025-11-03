using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Departments"" VALUES 
                ('d001','Marketing'),
                ('d002','Finance'),
                ('d003','Human Resources'),
                ('d004','Production'),
                ('d005','Development'),
                ('d006','Quality Management'),
                ('d007','Sales'),
                ('d008','Research'),
                ('d009','Customer Service');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""Departments"" WHERE ""Code"" IN 
                ('d001','d002','d003','d004','d005','d006','d007','d008','d009');
            ");
        }
    }
}
