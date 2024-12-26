using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using Web_work_01.Models;
using static Web_work_01.Connection;
using Microsoft.Extensions.Configuration;

namespace Web_work_01.Services
{
    public class EmployeeService
    {
        public static int Insert(Employee employee)
        {
            string connectionString = Connection.AllowedHosts.String;
            string insertQuery = @"
            INSERT INTO Employees (Name, Password, DepartmentId)
            VALUES (@Name, @Password, @DepartmentId);";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCon))
                    {   
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("新增失敗: " + ex.Message);
                return -1;
            }
        }

        public static int Delete(Employee employee)
        {
            string connectionString = Connection.AllowedHosts.String;
            string deleteQuery = @"
            DELETE FROM Employees
            WHERE Name = @Name;";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("刪除失敗: " + ex.Message);
                return -1;
            }
        }
    }
}

