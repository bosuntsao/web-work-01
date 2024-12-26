using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using Web_work_01.Models;
using static Web_work_01.Connection;
using Microsoft.Extensions.Configuration;

namespace Web_work_01.Services
{
    public class DepartmentService
    {
        public static int UpdateEmployeeDepartmentID(Employee employee)
        {
            string connectionString = Connection.AllowedHosts.String;
            string updateQuery = @"
            UPDATE Employees 
            SET DepartmentId = @DepartmentId 
            WHERE Name = @Name";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                        cmd.Parameters.AddWithValue("@Name", employee.Name);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected;
                    }


                }
            }           
            catch (Exception ex)
            {
                Console.WriteLine("更新失敗: " + ex.Message);
                return -1;
            }
        }

        public static int UpdateDepartment(Department department)
        {
            string connectionString = Connection.AllowedHosts.String;
            string updateQuery = @"
            UPDATE Departments 
            SET department = @department 
            WHERE DepartmentID = @DepartmentID";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@department", department.department);
                        cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新失敗: " + ex.Message);
                return -1;
            }
        }

        public static int Insert(Department department)
        {
            string connectionString = Connection.AllowedHosts.String;
            string insertQuery = @"
            INSERT INTO Departments (department, ParentID, Level)
            VALUES (@department, @ParentID, @Level);";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@department", department.department);
                        cmd.Parameters.AddWithValue("@ParentID", department.ParentID);
                        cmd.Parameters.AddWithValue("@Level", department.Level);

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

        public static int Delete(Department department)
        {
            string connectionString = Connection.AllowedHosts.String;
            string deleteQuery = @"
            DELETE FROM Departments
            WHERE departmentId = @departmentId;";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@departmentId", department.DepartmentId);
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
