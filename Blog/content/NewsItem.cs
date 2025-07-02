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
    class NewsItem : ContentItem, ISearchable
    {
        public Category Category { get; protected set; }
        [JsonConstructor]
       
        public NewsItem(string title, string content, Author author, int id, Category category)
     : base(title, content, author, id)
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

        public bool Matches(string keyword)
        {
            if (Title.Contains(keyword) || Content.Contains(keyword) || Category.ToString().Equals(keyword))
            {
                return true;
            }
            return false;
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