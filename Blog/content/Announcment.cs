using Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostInfo
{
    [Serializable]
    class Announcment : ContentItem, ISearchable
    {
        public bool IsUrgent { get; private set; }
        [JsonConstructor]
        
        public Announcment(string title, string content, Author author, int id, bool urgent) : base(title, content, author, id)
        {
            IsUrgent = urgent;
        }



        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ОГОЛОШЕННЯ");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Терміновість: {(IsUrgent ? "❗ Терміново" : "Звичайне")}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Створено: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }

        public bool Matches(string keyword)
        {
            if (Title.Contains(keyword) || Content.Contains(keyword))
            {
                return true;
            }
            return false;
        }
    }
}