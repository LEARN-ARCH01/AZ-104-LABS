using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace LearnCourceApp.Pages
{
    // Changed from "CoursesModel" to "coursesModel" 
    // so it matches "courses.cshtml"
    public class CoursesModel : PageModel

    {
        public List<Course> CourseList { get; set; } = new();

        public void OnGet()
        {
            string connectionString = Environment.GetEnvironmentVariable("CourseDb");

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT id, name, price FROM courses";
            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                CourseList.Add(new Course
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Price = reader.GetDecimal("price")
                });
            }
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}
