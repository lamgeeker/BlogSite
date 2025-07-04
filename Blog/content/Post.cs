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

        [JsonConstructor]
        public Post(string title, string content, Author author, int id, string heshTag) : base(title, content, author, id)
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

        
    }
}