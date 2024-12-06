using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data.Common;

namespace MyDAL
{
    public class StudentRepo : IStudentRepo
    {
        private readonly string _connectionString;

        public StudentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int SaveStudent(Students students)
        {
            string procedure = "sp_UpsertStudent"; // Stored procedure name
            var parameters = new DynamicParameters();
            parameters.Add("@Id",students.Id,DbType.Int32);
            parameters.Add("@Name", students.name, DbType.String);
            parameters.Add("@City", students.city, DbType.String);

            // Assuming your stored procedure returns the new StudentId
            parameters.Add("@StudentId",dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var res = dbConnection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                int newStudentId = parameters.Get<int>("@StudentId");
                return newStudentId;
            }
        }
        public List<Students> GetAllStudents()
        {
            string procedure = "sp_studentDetails"; // Stored procedure name
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open(); // Open the database connection

                // Execute the stored procedure and map the results to the Students list
                var students = dbConnection.Query<Students>(procedure, commandType: CommandType.StoredProcedure).ToList();

                return students; // Return the list of students
            }
        }

        public Students GetStudent(int id)
        {
            string procedure = "sp_studentDetailsById"; // Corrected to remove leading space
            var parameters = new DynamicParameters();

            parameters.Add("@id", id, DbType.Int32); // Adding the parameter

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open(); // Open the database connection

                // Corrected the query execution
                var student = dbConnection.QueryFirstOrDefault<Students>(procedure, parameters, commandType: CommandType.StoredProcedure);

                return student; // Return the student object or null if not found
            }
        }

        public Students UpdateStudentByid(Students std)
        {
            String procedure = "UpdateStudentDetails";
           
            var parameters= new DynamicParameters();

            parameters.Add("@id",std.Id, DbType.Int32);
            parameters.Add("@Name", std.name, DbType.String);
            parameters.Add("@City",std.city, DbType.String);
            using (IDbConnection dbConnection= new SqlConnection(_connectionString))
            {
                dbConnection.Open();    
                var student=dbConnection.QueryFirst(procedure,parameters, commandType: CommandType.StoredProcedure);

                return student;
               
            }
           

        }
        public string DeleteStdById(int id)
        {
            string procedure = "sp_DeleteStdById";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32);
           // parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output); // Add output parameter

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                // Execute the stored procedure
                int affectedRows = dbConnection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the value of the output parameter
               // int affectedRows = parameters.Get<int>("@RowsAffected");

                // Check the affected rows to determine the result
                if (affectedRows > 0)
                {
                    return $"Successfully deleted student at id {id}.";
                }
                else
                {
                    return $"Cannot delete student. No student found with id {id}.";
                }
            }
        }


        public Students ValidateUser(string username, string password)
        {
           
        
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT Username,Password FROM tblStudentDetails WHERE Username = @username AND Password = @password";
            return connection.QuerySingleOrDefault<Students>(query, new { Username = username, Password = password });
        

    }

}
}
