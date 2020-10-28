using System;

namespace TestTask.Models
{
    public class Person : Entity
    {
        public string IIN { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string PatreonicName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}