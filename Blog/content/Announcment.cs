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
    class Announcment : ContentItem 
    { 
        public bool IsUrgent { get; protected set; }
        public Announcment(string title, string content, Author author, int id, bool isUrgent)
        : base(title, content, author, id)
        {
            IsUrgent = isUrgent;
        }

   
        [JsonConstructor]
        public Announcment(string title, string content, DateTime createdAt, Author author, int id, bool isUrgent)
            : base(title, content, createdAt, author, id)
        {
            IsUrgent = isUrgent;
        }


        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ОГОЛОШЕННЯ");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Терміновість: {(IsUrgent ? "! Терміново" : "Звичайне")}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Створено: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }

        public void Update(string title, string content, bool isUrgent)
        {
            base.Update(title, content);
            IsUrgent = isUrgent;
        }
    }
}