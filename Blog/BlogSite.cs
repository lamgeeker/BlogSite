using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PostInfo;

namespace Blog
{
    class BlogSite
    {
        

        public void AddPost(string Title, string Content, Author author, List<Post> Posts, string created, int id)
        {
                
                Post post = new Post(Title, Content, created, author, id);
                Posts.Add(post);
                Console.WriteLine("Пост успішно додано!");
        
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
