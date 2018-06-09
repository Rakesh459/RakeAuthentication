using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagement
{
    public class Program : Rakesh
    {

        static void Main(string[] args)
        {
            //int sum1 =  Exec(() => sum(1, 2));
            //  string sumInString = Exec(() => sumToString(1, 3));

            Student s = Exec(() => RakeshFactoryManagement<IStudent>
                                   .Create()
                                   .GetStudent(3)
                              );

        }

        public static int sum(int x, int y)
        {
            return x + y;
        }
        public static string sumToString(int x, int y)
        {
            return (x + y).ToString();
        }
    }

    public class Rakesh
    {
        public static Student Exec<Student>(Func<Student> func)
        {
            return func();
        }
    }

    public class Student : IStudent
    {

        private List<Student> students = new List<Student>();

        public int Id { get; set; }
        public string Name { get; set; }


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
        Student GetStudent(int id);
    }

    public class RakeshFactoryManagement<T>
    {
        readonly static Dictionary<string, Func<T>> dictionary = new Dictionary<string, Func<T>>();

        private static void Register(string imple, Func<T> ctor)
        {
            string implementation = typeof(T).Name;

            dictionary.Add(implementation, ctor);

        }

        public static T Create()
        {
            string implementation = typeof(T).Name;
            Func<T> cons;


            RakeshFactoryManagement<IStudent>.Register(implementation, () => new Student());

            if (dictionary.TryGetValue(implementation, out cons))
            {
                return cons();
            }

            throw new NotImplementedException();

        }
    }




}
