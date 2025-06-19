using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostInfo;

namespace Blog
{
    class BlogSite
    {
        

        public void AddPost(string Title, string Content, Author author, List<Post> Posts, string created)
        {
            Random rnd = new Random();
            //DateTime created = DateTime.Today;
            int id = rnd.Next(1, 100000);
            Posts.Add(new Post {  Title = Title, Content = Content , CreatedAt = DateTime.Parse(created), Id = id, Author = author});

        }

        public void PostList(List<Post> Posts)
        {
            foreach (Post post in Posts)
            {
                Console.WriteLine($"------------------------\nАвтор: {post.Author.Name } {post.Author.Surname} ID: {post.Id}\n{post.Title}\n{post.Content}\nЧас викладення статті:{post.CreatedAt}\n------------------------\n");
            }
        }
        public List<Post> SortByTime(List<Post> posts)
        {

            return posts.OrderBy(p => p.CreatedAt).ToList();
        }

        public void ShowSortedList(List<Post> posts)
        {
            foreach (Post post in posts)
            {
                Console.WriteLine($"------------------------\nАвтор: {post.Author.Name} {post.Author.Surname} ID: {post.Id}\n{post.Title}\n{post.Content}\nЧас викладення статті:{post.CreatedAt}\n------------------------\n");
            }
        }
        


    }
}
