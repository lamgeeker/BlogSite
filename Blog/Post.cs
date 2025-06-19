using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PostInfo
{
    class Post
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Author Author { get; private set; }

        public Post(string title, string content, string createdat, Author author, int id)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || !DateTime.TryParse(createdat,out DateTime createdAt) || string.IsNullOrWhiteSpace(author.Name )|| string.IsNullOrWhiteSpace(author.Surname))
            {
                throw new ArgumentException("Неправильно введена дата і час або не всі поля заповнені!");
            }
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Author = author;
            Id = id;
        }
       
    }
}
