using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfo

{
    public abstract class ContentFactory
    {
        public abstract ContentItem CreateContent(string title, string content, Author author, int id);

    }

    public class AnnouncmentFactory : ContentFactory
    {
        public ContentItem CreateContent(string title, string content, Author author, int id, bool urgent)
        {
            return new Announcment(title, content, author, id, urgent);
        }
        public override ContentItem CreateContent(string title, string content, Author author, int id)
        {

            return new Announcment(title, content, author, id, false);
        }
    }

    public class NewsItemFactory : ContentFactory
    {
        public ContentItem CreateContent(string title, string content, Author author, int id, Category category)
        {
            return new NewsItem(title, content, author, id, category);
        }
        public override ContentItem CreateContent(string title, string content, Author author, int id)
        {

            return new NewsItem(title, content, author, id, Category.Спорт);
        }
    }

    public class PostFactory : ContentFactory
    {
        public ContentItem CreateContent(string title, string content, Author author, int id, string heshtag)
        {
            return new Post(title, content, author, id, heshtag);
        }
        public override ContentItem CreateContent(string title, string content, Author author, int id)
        {

            return new Post(title, content, author, id, "");
        }
    }
}