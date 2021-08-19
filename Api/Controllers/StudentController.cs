using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Api.Controllers
{
    #region StudentController
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private List<Student> students = new List<Student>()
            {
                new Student{StudentId =1, Fname = "Princess", Lname="Uche", UserName="PrincessU", Password ="789101"},
                new Student{StudentId =2, Fname = "Louange", Lname="Zady", UserName="Louange", Password ="234569"},
                new Student{StudentId =3, Fname = "Deborah", Lname="Jackson", UserName="DebieJ", Password ="564789"},
                new Student{StudentId =4, Fname = "Grace", Lname="Titi", UserName="GraceT", Password ="345678"},
                new Student{StudentId =5, Fname = "Shekina", Lname="George", UserName="Sheks", Password ="879563"},
                new Student{StudentId =6, Fname = "Samuel", Lname="Buchi", UserName="SamuelB", Password ="abcdfr"},
                new Student{StudentId =7, Fname = "John", Lname="Mendes", UserName="JohnM", Password ="hikjlm"},
                new Student{StudentId =8, Fname = "Mega", Lname="Ema", UserName="Emega", Password ="prstuv"},
                new Student{StudentId =9, Fname = "Sarah", Lname="Luk", UserName="Sarah", Password ="589763"},
                new Student{StudentId =10, Fname = "light", Lname="Williams", UserName="Wlight", Password ="758932"},
            };

        // GET: api/AllStudents

        // GET: api/Student
        [HttpGet]
        [Authorize]
        public List<Student> Get()
        {
            return students;
        }

        // GET: api/Student/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public Student Get(int id)
        {
            Student s = null;
            students.ForEach(delegate (Student student)
            {
                if (student.StudentId == id)
                {
                    s = student;
                }
            });
            return s;
        }
    }
}
#endregion