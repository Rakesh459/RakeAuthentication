using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudent student = new MockStudent();
            ((MockStudent)student).SetupData(new Student { Id = 2, Name = "FakeRepo" });
           Student mockStudent = student.GetStudent(3);

             student = new Student();
             mockStudent = student.GetStudent(3);
        }
    }

    public class Student : IStudent
    {
        private List<Student> students = new List<Student>();

        public int Id { get; set ; }
        public string Name { get ; set ; }


        public Student GetStudent(int id)
        {
            students.Add(new Student { Id = 1, Name = "Original Repo" });
            students.Add(new Student { Id = 2, Name = "Original Repo1" });
            students.Add(new Student { Id = 3, Name = "Original Repo2" });
            students.Add(new Student { Id = 4, Name = "Original Repo3" });

            return students.Find(x => x.Id == id);
        }
    }

    public interface IStudent
    {
         int Id { get; set; }
         string Name { get; set; }
        Student GetStudent(int id);
    }

    public class MockStudent : MockData, IStudent
    {

        public int Id { get ; set ; }
        public string Name { get ; set ; }

        public Student GetStudent(int id)
        {
           return GetData<Student>(); 
        }
    }

    public class MockData
    {
        static Dictionary<string, object> _fakeRepo = new Dictionary<string, object>();

        public void SetupData<T>(T mockRepo) where T : class
        {
            string Type = typeof(T).Name;
            if(!_fakeRepo.ContainsKey(Type))
            _fakeRepo.Add(Type, mockRepo);

        }
        public  T GetData<T>()
        {
            string Type = typeof(T).Name;
            if (_fakeRepo.ContainsKey(Type))
            {
                return (T)_fakeRepo[Type];
            }
            throw new KeyNotFoundException();
        }
    }
}
