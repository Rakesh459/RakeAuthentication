using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RakeAuthorizationManager;
using System.Security.Permissions;

namespace RakeAuthentication
{
    [RakeCodePermission(SecurityAction.Demand, _accessLevel = "Requester")]
    public class HelloService : IHelloService
    {
        List<Student> students = new List<Student>();

        [RakeCodePermission(SecurityAction.Demand, _accessLevel = "Requester")]
        public Student GetStudent(int id)
        {
            students.Add(new Student { Id = 1, Name = "Rakesh", Marks = 20, Status = "Fail" });
            students.Add(new Student { Id = 2, Name = "Rakesh1", Marks = 30, Status = "Fail" });
            students.Add(new Student { Id = 3, Name = "Rakesh2", Marks = 40, Status = "Pass" });
            students.Add(new Student { Id = 4, Name = "Rakesh3", Marks = 50, Status = "Pass" });

           Student student = students.Find(x => x.Id == id);

            return student;
        }
        [RakeCodePermission(SecurityAction.Demand, _accessLevel = "Host")]
        public Student GetStudentAuthen(int id)
        {
            return new Student { Id = 0, Name = "highly auth" };
        }
    }
}
