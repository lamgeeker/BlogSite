using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfo
{
    public class Author
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public Author() { }
        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
