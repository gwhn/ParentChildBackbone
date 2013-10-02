using System.Collections.Generic;

namespace ParentChild.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Child> Children { get; set; }
    }
}