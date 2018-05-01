using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RakeAuthentication
{
    [DataContract(Name ="Student")]
   public class Student
    {
        [DataMember(Name ="Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Marks")]
        public int Marks { get; set; }
        [DataMember(Name = "Status")]
        public string Status { get; set; }
    }
}
