using Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PostInfo
{

    public abstract class ContentItem : IIdentifiable
    {
       
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Author Author { get; protected set; }
        public int ID { get; }

        public ContentItem(string title, string content, Author author, int id)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(author.Name) || string.IsNullOrWhiteSpace(author.Surname))
            {
                throw new ArgumentException("Неправильно введена дата і час або не всі поля заповнені!");
            }
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now; // Встановлюємо поточну дату при створенні нового об'єкта
            Author = author;
            ID = id;
        }

        // Конструктор для десеріалізації (JsonConstructor)
        [JsonConstructor]
        public ContentItem(string title, string content, DateTime createdAt, Author author, int id)
        {
            Title = title;
            Content = content;
            CreatedAt = createdAt; // Встановлюємо дату з десеріалізації
            Author = author;
            ID = id;
        }

        public abstract void Display();
        public virtual void Update(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Порожній заголовок або контент");
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}