using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
            // Method syntax
            return Emps.Where(emp => emp.Job == "Backend programmer");
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Frontend programmer" AND Salary > 1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            // Query syntax
            return from emp in Emps
                   where emp.Job == "Frontend programmer" && emp.Salary > 1000
                   orderby emp.Ename descending
                   select emp;
        }

        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            // Method syntax
            return Emps.Max(emp => emp.Salary);
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            // Query syntax
            int maxSalary = Emps.Max(emp => emp.Salary);
            return from emp in Emps
                   where emp.Salary == maxSalary
                   select emp;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            // Method syntax
            return Emps.Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job });
        }
        
        public static IEnumerable<object> Task6()
        {
            // Query syntax
            return from emp in Emps
                   join dept in Depts on emp.Deptno equals dept.Deptno
                   select new { emp.Ename, emp.Job, dept.Dname };
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            // Method syntax
            return Emps.GroupBy(emp => emp.Job)
                       .Select(group => new { Praca = group.Key, LiczbaPracownikow = group.Count() });
        }

        /// <summary>
        ///     Return the value "true" if at least one
        ///     of the elements in the collection works as a "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            // Query syntax
            return (from emp in Emps
                    where emp.Job == "Backend programmer"
                    select emp).Any();
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            // Method syntax
            return Emps.Where(emp => emp.Job == "Frontend programmer")
                       .OrderByDescending(emp => emp.HireDate)
                       .FirstOrDefault();
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            // Query syntax
            var employees = from emp in Emps
                            select new { emp.Ename, emp.Job, emp.HireDate };

            var additional = new[]
            {
                new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null }
            };

            return employees.Union(additional);
        }

        
        public static IEnumerable<object> Task11()
        {
            // Method syntax
            return Emps.GroupBy(emp => emp.Deptno)
                       .Where(group => group.Count() > 1)
                       .Select(group => new
                       {
                           name = Depts.FirstOrDefault(dept => dept.Deptno == group.Key)?.Dname,
                           numOfEmployees = group.Count()
                       });
        }

        
        public static IEnumerable<Emp> Task12()
        {
            // Method syntax
            return Emps.Where(emp => Emps.Any(e => e.Mgr == emp))
                       .OrderBy(emp => emp.Ename)
                       .ThenByDescending(emp => emp.Salary);
        }
        
        public static int Task13(int[] arr)
        {
            // Query syntax
            return (from num in arr
                    group num by num into numGroup
                    where numGroup.Count() % 2 != 0
                    select numGroup.Key).Single();
        }

        
        public static IEnumerable<Dept> Task14()
        {
            // Method syntax
            var deptWithEmpCount = Emps.GroupBy(emp => emp.Deptno)
                                       .Select(group => new { Deptno = group.Key, Count = group.Count() });

            return Depts.Where(dept => deptWithEmpCount.Any(d => d.Deptno == dept.Deptno && d.Count == 5) ||
                                       !deptWithEmpCount.Any(d => d.Deptno == dept.Deptno))
                        .OrderBy(dept => dept.Dname);
        }
    }

    public static class CustomExtensionMethods
    {
        //Put your extension methods here
    }
}