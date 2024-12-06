using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MyBAL;

namespace Myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public IActionResult SaveStudent(Students obj)
        {
            var res = _studentService.SaveStudent(obj);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllStudent()
        {
            var res = _studentService.GetAllStudent();
            return Ok(res);
        }
        [HttpGet]
        [Route("getByid")]
        public IActionResult GetStudent(int id) {
            var res=_studentService.GetStudent(id);
            return Ok(res); 
        }
        [HttpPut]
        [Route("updateStdById")]
        public Students UpdateStudentByid(Students std)
        {
            var res=_studentService.UpdateStudentByid(std);
            return res;
        }
        [HttpDelete]
        [Route("deleteStdByid")]
        public IActionResult DeleteStudent(int id) {
            if (id <= 0) {
                return BadRequest("enter correct id");
            }
        var res=_studentService.DeleteStdById(id);
            return Ok(res);
        }
    }
}
