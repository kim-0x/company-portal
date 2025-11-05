using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeTitleSalaryHistoryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // For GiST range indexes
            migrationBuilder.Sql(@"CREATE EXTENSION IF NOT EXISTS btree_gist;");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW public.""EmployeeTitleSalaryHistoryView"" AS
                WITH bounds AS (
                    -- collect all change boundaries (timestamps, not dates)
                    SELECT ""EmployeeId"", ""FromDate"" AS b FROM public.""EmployeeTitles""
                    UNION
                    SELECT ""EmployeeId"", COALESCE(""ToDate"", 'infinity'::timestamptz) FROM public.""EmployeeTitles""
                    UNION
                    SELECT ""EmployeeId"", ""FromDate"" FROM public.""EmployeeSalaries""
                    UNION
                    SELECT ""EmployeeId"", COALESCE(""ToDate"", 'infinity'::timestamptz) FROM public.""EmployeeSalaries""
                ),
                dedup AS (
                    SELECT ""EmployeeId"", b AS boundary
                    FROM bounds
                    GROUP BY ""EmployeeId"", b
                ),
                ordered AS (
                    SELECT
                        ""EmployeeId"",
                        boundary AS period_start,
                        LEAD(boundary) OVER (PARTITION BY ""EmployeeId"" ORDER BY boundary) AS period_end
                    FROM dedup
                ),
                segments AS (
                    -- keep half-open [start, end); drop open-ended tail where end is NULL
                    SELECT
                        ""EmployeeId"",
                        period_start       AS ""FromDate"",
                        period_end         AS ""ToDate""
                    FROM ordered
                    WHERE period_end IS NOT NULL
                ),
                resolved AS (
                    SELECT
                        s.""EmployeeId"",
                        s.""FromDate"",
                        s.""ToDate"",
                        t.""Title"",
                        sal.""Amount""
                    FROM segments s
                    LEFT JOIN public.""EmployeeTitles"" t
                    ON t.""EmployeeId"" = s.""EmployeeId""
                    AND tstzrange(t.""FromDate"", COALESCE(t.""ToDate"", 'infinity'::timestamptz), '[)') @> s.""FromDate""
                    LEFT JOIN public.""EmployeeSalaries"" sal
                    ON sal.""EmployeeId"" = s.""EmployeeId""
                    AND tstzrange(sal.""FromDate"", COALESCE(sal.""ToDate"", 'infinity'::timestamptz), '[)') @> s.""FromDate""
                )
                SELECT
                    e.""Id""   AS ""EmployeeId"",
                    e.""FirstName"" || ' ' || e.""LastName"" AS ""EmployeeName"",
                    r.""Title"",
                    r.""Amount"" AS ""SalaryAmount"",
                    r.""FromDate"",
                    r.""ToDate""
                FROM resolved r
                JOIN public.""Employees"" e ON e.""Id"" = r.""EmployeeId""
                ORDER BY e.""Id"", r.""FromDate"";
            ");

            // Helpful indexes
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_Title_Employee_FromDate  ON public.""EmployeeTitles""  (""EmployeeId"", ""FromDate"");");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS IX_Salary_Employee_FromDate ON public.""EmployeeSalaries"" (""EmployeeId"", ""FromDate"");");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS IX_Title_Employee_Range
                ON public.""EmployeeTitles""
                USING GIST (""EmployeeId"", tstzrange(""FromDate"", COALESCE(""ToDate"", 'infinity'::timestamptz), '[)'));

                CREATE INDEX IF NOT EXISTS IX_Salary_Employee_Range
                ON public.""EmployeeSalaries""
                USING GIST (""EmployeeId"", tstzrange(""FromDate"", COALESCE(""ToDate"", 'infinity'::timestamptz), '[)'));
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS public.""EmployeeTitleSalaryHistoryView"";");

            migrationBuilder.Sql(@"DROP INDEX IF EXISTS IX_Title_Employee_FromDate;");
            migrationBuilder.Sql(@"DROP INDEX IF EXISTS IX_Salary_Employee_FromDate;");
            migrationBuilder.Sql(@"DROP INDEX IF EXISTS IX_Title_Employee_Range;");
            migrationBuilder.Sql(@"DROP INDEX IF EXISTS IX_Salary_Employee_Range;");
            // Don't drop btree_gist extension automatically (it may be used elsewhere)
        }
    }
}
