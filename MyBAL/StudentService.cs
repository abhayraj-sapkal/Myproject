using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MyDAL;

namespace MyBAL
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo studentRepo;

        public StudentService(IStudentRepo studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        public int SaveStudent(Students students)
        {
            return studentRepo.SaveStudent(students);
        }
        public List<Students> GetAllStudent()
        {
            return studentRepo.GetAllStudents();
        }

        public Students GetStudent(int id)
        {
            return studentRepo.GetStudent(id);
        }
        public Students UpdateStudentByid(Students std)
        {
            return studentRepo.UpdateStudentByid(std);
        }
        public string DeleteStdById(int id)
        {
            return studentRepo.DeleteStdById(id);

        }
        public Students ValidateUser(string username, string password)
        {
            return studentRepo.ValidateUser(username, password);
        }
    }
}
