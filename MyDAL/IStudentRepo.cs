using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace MyDAL
{
    public interface IStudentRepo
    {
        public int SaveStudent(Students students);
        
        public List<Students> GetAllStudents();
        public Students GetStudent(int id);

        public Students UpdateStudentByid(Students std);
        public string DeleteStdById(int id);
        public Students ValidateUser(string username, string password);

    }
}
