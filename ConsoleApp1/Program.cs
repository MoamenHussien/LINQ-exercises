using Microsoft.VisualBasic;
using System;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Tar;
using System.Globalization;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Xml.Serialization;
namespace ConsoleApp1322;

using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

internal class Program
{
    static void Main(string[] args)
    {
        
        var test_1 = ListGenerator.ProductList.Sum(n => n.UnitsInStock * n.UnitPrice);

        var test_2 = ListGenerator.ProductList.Where(n => n.UnitPrice > ListGenerator.ProductList.Average(n => n.UnitPrice)).First();

        var test_3 = ListGenerator.EmployeeList.Any(n => n.IsActive == false);

        var test_4 = ListGenerator.BookList.OrderBy(n => n.Title).Take(5);

        var test_5 = ListGenerator.CustomerList.Select(n => n.Country).Distinct();

        var test_6 = ListGenerator.EmployeeList.Where(n => n.HireDate > DateTime.Now.AddYears(-3));

        var test_7 = ListGenerator.CustomerList.Where(n => n.Orders?.Sum(n => n.Total) > 1000);

        var test_7_7 = ListGenerator.CustomerList.Select(n => new { Total = n.Orders?.Sum(n => n.Total), Customer = n }).Where(n => n.Total > 1000);

        var test_8 = ListGenerator.BookList.Count(n => n.CopiesInStock == 0 || n.IsAvailable == false);

        var test_9 = ListGenerator.EmployeeList.GroupBy(n => n.Department).Select(n => new { Departments = n.Key, avg_salary = n.Average(n => n.Salary) });

        var test_10 = ListGenerator.ProductList.GroupBy(n => n.Category).Select(n => new { Category = n.Key, Top3 = n.OrderByDescending(n => n.UnitPrice).Take(3) });

        var test_11 = from proj in ListGenerator.EmployeeProjectList
                      join empl in ListGenerator.EmployeeList
                      on proj.EmployeeId equals empl.Id
                      select new { empl.Name };


        var test_11_1 = test_11.GroupBy(n => n).Where(n => n.Count() > 1).Select(n => n.Key);

        var test_11_11 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Where(n => n.Select(n => n.ProjectId).Count() > 1).Select(n => new { employee = ListGenerator.EmployeeList.First(t => t.Id == n.Key), count = n.Count() }); // ال where هنا عايده على المشاريع الى موجود فيها ال employee فبلتالى بتاثر على ال key , ال count بتعود على عدد ال مشاريع

        var test_12 = ListGenerator.EmployeeProjectList.GroupBy(n => n.ProjectId).Select(n => new { project_name = ListGenerator.ProjectList.First(x => x.Id == n.Key), total_hours = n.Sum(n => n.HoursAllocated) }).OrderByDescending(n => n.total_hours);

        var test_13 = ListGenerator.ProjectList.Average(n => n.StartDate.Ticks);

        var test_13_1 = ListGenerator.ProjectList.Where(n => n.StartDate > new DateTime((long)test_13));

        var test_14 = from book_loan in ListGenerator.BookLoanList
                      join emp in ListGenerator.EmployeeList
                      on book_loan.EmployeeId equals emp.Id
                      join book_list in ListGenerator.BookList
                      on book_loan.BookId equals book_list.Id
                      select new { emp.Id, emp.Name, book_list.Genre };

        var test_14_1 = test_14.GroupBy(n => n.Name).Where(n => n.Select(n => n.Genre).Distinct().Count() > 1).Select(n => new { Genre_count = n.Count(), Emp_Name = n.Key });

        var test_15 = ListGenerator.ProjectList.Where(n => n.EndDate != null).OrderByDescending(n => n.EndDate - n.StartDate).First();
        //hard
        var test_16 = ListGenerator.CustomerList.Select(n => new { count_pro = n.Orders.Where(n => n.Date.Year == 2024).Select(n => (n.Date.Month - 1) / 3 + 1).Distinct(), customer = n }).Where(n => n.count_pro.Count() > 4).Select(n => n.customer);

        var test_17 = ListGenerator.BookLoanList.GroupBy(n => n.LoanDate.Month).Select(n => new { Months = n.Key, Total_books = n.Count() });
        //trick
        var test_18 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Where(n=> n.Any(n=>n.IsActive==true) && n.Any(n=>n.IsActive == false)).Select(n => new { employee_name = ListGenerator.EmployeeList.First(x => x.Id == n.Key) });

        var test_19 = from proj in ListGenerator.EmployeeProjectList
                      join emp in ListGenerator.EmployeeList
                      on proj.EmployeeId equals emp.Id
                      select (new { employee_status = emp.IsActive, projects = proj });

        var test_19_1 = test_19.GroupBy(n => n.projects.ProjectId).Where(n => n.All(n => n.employee_status == true)).Select(n => new { Project = n.Key });

        var test_20 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Where(n => n.Any(n => n.Role == ProjectRole.Lead )&&( n.Any(n=>n.Role == ProjectRole.Developer))).Select(n => new { Employee = n.Key, Employee_name = ListGenerator.EmployeeList.First(x => x.Id == n.Key) });

        var test_21 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Select(n => new { employyy = ListGenerator.EmployeeList.First(z=>z.Id==n.Key), Hieghst_Hours = n.Sum(n => n.HoursAllocated) }).OrderByDescending(n => n.Hieghst_Hours).First();

        var test_22 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Select(n => new { sum_of_hours = n.Sum(n => n.HoursAllocated) }).Average(n => n.sum_of_hours) ;
        var test_22_1 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Where(n => n.Sum(n => n.HoursAllocated) > test_22 ).Select(n => new { Employees = ListGenerator.EmployeeList.First(x => x.Id == n.Key), Total_hours = n.Sum(n => n.HoursAllocated) });

        var test_23 = from books_loan in ListGenerator.BookLoanList
                      join book_list in ListGenerator.BookList
                      on books_loan.BookId equals book_list.Id
                      select (new { book_genre = book_list.Genre, books_loan});

        var test_23_1 = test_23.GroupBy(n => n.book_genre).Select(n => new { Books_Borrowed_count = n.Count() ,book_genres=n.Key }).OrderByDescending(n=>n.Books_Borrowed_count).ThenBy(n=>n.book_genres).Take(3);

        var test_24 = ListGenerator.EmployeeList.Select(n => n.Id).Except(ListGenerator.BookLoanList.Select(n => n.EmployeeId)).Select(n => new { emp_never_borrowed_book = ListGenerator.EmployeeList.First(x => x.Id == n) });

        var test_24_24 = ListGenerator.EmployeeList.Where(n => !ListGenerator.BookLoanList.Any(x => x.EmployeeId == n.Id));

        var test_25 = ListGenerator.EmployeeList.Where(n => n.Department == Department.IT).All(n => n.IsActive == true);

        var test_26 = from book_loan in ListGenerator.BookLoanList
                      join book_list in ListGenerator.BookList
                      on book_loan.BookId equals book_list.Id
                      join emp in ListGenerator.EmployeeList
                      on book_loan.EmployeeId equals emp.Id
                      where emp.YearsOfExperience > 5
                      select (book_list.Rating);

        var test_26_1 = test_26.Average();

        var test_27 = ListGenerator.ProjectList.Max(n => n.Budget);
        var test_27_1 = ListGenerator.ProjectList.Min(n => n.Budget);
        var test_27_2 = test_27 - test_27_1;


        var test_28 = ListGenerator.EmployeeProjectList.Select(n => n.EmployeeId).Distinct().Count();
        var test_28_1 = Math.Ceiling((decimal)test_28/10);
        var test_28_2 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Select(n => new { Total_hours = n.Sum(n=>n.HoursAllocated) , Employee=n.Key }).OrderByDescending(n=>n.Total_hours).Take((int)test_28_1);

        var test_29 = from emp_pro_li in ListGenerator.EmployeeProjectList
                      join pro_li in ListGenerator.ProjectList
                      on emp_pro_li.ProjectId equals pro_li.Id
                      where pro_li.Category == ProjectCategory.AI_ML
                      join book_loan in ListGenerator.BookLoanList
                      on emp_pro_li.EmployeeId equals book_loan.EmployeeId
                      select (new { book_loan.BookId});

        var test_30 = ListGenerator.CustomerList.Where(n => n.Orders != null && n.Orders.Any(n => n.Date.Year == 2024) && n.Orders.Any(n => n.Date.Year == 2025)).Count();

        var test_31 = ListGenerator.BookLoanList.GroupBy(n => n.EmployeeId).Select(n=>n.Select(n=>n.BookId).Count()).Average();
        var test_31_1 = ListGenerator.BookLoanList.GroupBy(n => n.EmployeeId).Where(n => n.Count() > test_31).Select(n=> ListGenerator.EmployeeList.First(x=>x.Id==n.Key));

        var test_32 = from emp1 in ListGenerator.EmployeeList
                      join emp2 in ListGenerator.EmployeeList
                      on emp1.ManagerId equals emp2.Id
                      select (new { Employee= emp1, Manager_Hire_date = emp2.HireDate });

        var test_32_1 = test_32.Where(n => n.Employee.HireDate < n.Manager_Hire_date);

        var test_33 = ListGenerator.BookLoanList.Where(n => n.DueDate < n.ReturnDate || n.ReturnDate==null).Select(n => ListGenerator.BookList.First(x => x.Id == n.BookId));

        var test_34 = from emp_proj_list in ListGenerator.EmployeeProjectList
                      join proj_lis in ListGenerator.ProjectList
                      on emp_proj_list.ProjectId equals proj_lis.Id
                      select (new {Employee_proj_list =emp_proj_list ,Category =proj_lis.Category });

        var test_34_1 = test_34.GroupBy(n => n.Employee_proj_list.EmployeeId).Where(n => n.Select(n => n.Category).Distinct().Count() >= 2).Select(n => ListGenerator.EmployeeList.First(x => x.Id == n.Key));

        var test_35 = ListGenerator.ProjectList.Where(n => n.IsCompleted == true).Count();
        var test_35_1 = (double)  test_35 / ListGenerator.ProjectList.Count() *100  ;

        var test_36 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Select(n =>  new {emp = ListGenerator.EmployeeList.First(x=>x.Id==n.Key) ,ration = n.Sum(n => n.HoursAllocated) / ListGenerator.EmployeeList.First(x => x.Id == n.Key).YearsOfExperience }).OrderByDescending(n => n.ration).Take(3);

        var test_37 = from books_loan in ListGenerator.BookLoanList
                      join books_list in ListGenerator.BookList
                      on books_loan.BookId equals books_list.Id
                      select (new { loan_info = books_loan ,books_info =books_list });

        var test_37_1 = test_37.GroupBy(n => n.loan_info.EmployeeId).Where(n=>n.DistinctBy(n=>n.loan_info.BookId).Where(n=>n.books_info.Genre==BookGenre.Fantasy).Count()== ListGenerator.BookList.Where(n=>n.Genre == BookGenre.Fantasy).Count()).Select(n=> ListGenerator.EmployeeList.FirstOrDefault(x=>x.Id==n.Key));

        var test_38 = ListGenerator.BookLoanList.GroupBy(n => n.EmployeeId).Where(n => n.Select(n => new { n.LoanDate.Month, n.LoanDate.Year }).Distinct().Count() >= 3).Select(n => ListGenerator.EmployeeList.FirstOrDefault(x => x.Id == n.Key));

        var test_39 = ListGenerator.EmployeeProjectList.GroupBy(n => n.EmployeeId).Where(n => n.Select(n => n.ProjectId).Count() >= 2).SelectMany(n => n.Select(n => n.ProjectId).Distinct()).Distinct().Select(n => ListGenerator.ProjectList.First(h => h.Id == n));

        var test_40 = from emp_list in ListGenerator.EmployeeList
                      join manager_id in ListGenerator.EmployeeList
                      on emp_list.ManagerId equals manager_id.Id
                      where emp_list.ManagerId != null && emp_list.IsActive == true
                      orderby manager_id.HireDate 
                      select (new { Manager =manager_id});

        var test_40_1 = test_40.First();

        var test_41 = ListGenerator.BookLoanList.GroupBy(n => n.BookId).Select(n => new {Book_id = n.Key, count= n.Count() });

        var test_41_1 = test_41.Average(n => n.count);

        var test_41_2 = test_41.Where(n => n.count > test_41_1);

        var test_42 = ListGenerator.EmployeeProjectList.Where(n => !ListGenerator.ProjectList.Any(x => x.Id == n.ProjectId)).Select(n => new { Employee = n.EmployeeId });
        test_42.Print();

        //thie is the secound commit 
    }
}

