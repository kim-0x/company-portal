using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDepartmentManagerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""DepartmentManagers"" VALUES 
                    (10002,'d001','1985-01-01','1991-10-01'),
                    (10039,'d001','1991-10-01',null),
                    (10085,'d002','1985-01-01','1989-12-17'),
                    (10114,'d002','1989-12-17',null),
                    (10183,'d003','1985-01-01','1992-03-21'),
                    (10228,'d003','1992-03-21',null),
                    (10303,'d004','1985-01-01','1988-09-09'),
                    (10344,'d004','1988-09-09','1992-08-02'),
                    (10386,'d004','1992-08-02','1996-08-30'),
                    (10420,'d004','1996-08-30',null),
                    (10511,'d005','1985-01-01','1992-04-25'),
                    (10567,'d005','1992-04-25',null),
                    (10725,'d006','1985-01-01','1989-05-06'),
                    (10765,'d006','1989-05-06','1991-09-12'),
                    (10800,'d006','1991-09-12','1994-06-28'),
                    (10854,'d006','1994-06-28',null);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""DepartmentManagers"" WHERE ""EmployeeId"" IN 
                (10002,10039,10085,10114,10183,10228,10303,10344,10386,10420,10511,10567,10725,10765,10800,10854);
            ");
        }
    }
}
