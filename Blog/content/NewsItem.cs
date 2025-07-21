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
    class NewsItem : ContentItem
    {
        public Category Category { get; protected set; }
   
        public NewsItem(string title, string content, Author author, int id, Category category)
        : base(title, content, author, id)
        {
            Category = category;
        }

    
        [JsonConstructor]
        public NewsItem(string title, string content, DateTime createdAt, Author author, int id, Category category)
            : base(title, content, createdAt, author, id)
        {
          Category = category;
        }
        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Новина! ");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Категорія: {Category}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Час створення: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }

        public void Update(string title, string content, Category category)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(category.ToString()))
            {
                Console.WriteLine("Не вдається оновити дані, атже введено некоректні дані");
                return;
            }
            base.Update(title, content);
            Category = category;
        }
    }

    public enum Category
    {

        Спорт,
        Політика,
        Розваги,
        Культура,
        Наука,
        Кримінал
    }
}