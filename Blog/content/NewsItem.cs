using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostInfo
{
    class NewsItem: ContentItem
    {
        public string Category { get; private set; }
        [JsonConstructor]
        public NewsItem(int id, string title, string content, Author author) : base(id, title, content, author)
        {


        }

        public NewsItem(string title, string content, Author author, int id, string category) : base(title, content, author, id)
        {
            Category = category;
        }
        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Новина!");
            Console.WriteLine($"Категорія: {Category}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Час створення: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }
    }
}
