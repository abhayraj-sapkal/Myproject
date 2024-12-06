using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace MyBAL
{
    public interface IStudentService
    {
        public int SaveStudent(Students students);
        public Students ValidateUser(string username, string password);

        public List<Students> GetAllStudent();
        public Students GetStudent(int id);
        public Students UpdateStudentByid(Students std);
        public string DeleteStdById(int id);
    }
}
