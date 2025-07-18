﻿using Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PostInfo
{

    [Serializable]
    class Post : ContentItem
    {

        public string HeshTag { get; private set; }

        public Post(string title, string content, Author author, int id, string heshTag)
         : base(title, content, author, id)
        {
            HeshTag = heshTag;
        }

        // JsonConstructor для Post
        [JsonConstructor]
        public Post(string title, string content, DateTime createdAt, Author author, int id, string heshTag)
            : base(title, content, createdAt, author, id)
        {
            HeshTag = heshTag;
        }

        public override void Display()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("ПОСТ");
            Console.WriteLine($"ID: {ID}  #{HeshTag}");
            Console.WriteLine($"Заголовок: {Title}");
            Console.WriteLine($"Контент: {Content}");
            Console.WriteLine($"Автор: {Author.Name} {Author.Surname}");
            Console.WriteLine($"Створено: {CreatedAt}");
            Console.WriteLine("-------------------------------------");
        }

        public void Update(string title, string content, string heshTag)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(heshTag))
                throw new ArgumentException("Порожній заголовок або контент");
            base.Update(title, content);
            HeshTag = heshTag;
        }
    }
}