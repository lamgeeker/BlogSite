using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostInfo
{
    class Announcment: ContentItem
    {
        public bool IsUrgent { get; private set; }
        [JsonConstructor]
        public Announcment(int id, string title, string content, Author author) : base(id, title, content, author)
        {


        }

        public Announcment(string title, string content, Author author, int id, bool urgent) : base(title, content, author, id)
        {
            IsUrgent = urgent;
        }

       
        
        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("ОГОЛОШЕННЯ");
            Console.WriteLine($"Терміновість: {(IsUrgent ? "❗ Терміново" : "Звичайне")}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Створено: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }
    }
}
