using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using PostInfo;

namespace Blog
{
    class BlogSite
    {
        
        public void ShowList(List<ContentItem> list)
        {
            if (!list.Any()) 
            {
                Console.WriteLine("Список порожній!");
                return; 
            }
            foreach (ContentItem item in list)
            {
                item.Display();
            }
        }
       
        public List<ContentItem> SortByTime(List<ContentItem> posts)
        {
            if (!posts.Any())
            {
                Console.WriteLine("Список порожній!");
                return posts;
            }
            return posts.OrderBy(p => p.CreatedAt).ToList();
        }

        public void ShowSortedList(List<ContentItem> posts)
        {
            if(!posts.Any())
            {
                Console.WriteLine("Список порожній!");
                return;
            }
            SortByTime(posts);
            foreach (ContentItem item in posts)
            {
                item.Display();
            }
        }

        public void ChangeById(int id, List<ContentItem> contentItems)
        {
            if (!contentItems.Any()) return;
            var item = contentItems.FirstOrDefault(c => c.ID == id);
            if (item == null)
            {
                Console.WriteLine("Елемент з таким ID не знайдено.");
                return;
            }

            Console.Write("Новий заголовок: ");
            string newTitle = Console.ReadLine();
            Console.Write("Новий контент: ");
            string newContent = Console.ReadLine();

            if (item is Post post)
            {
                Console.Write("Новий HeshTag: ");
                string newHeshTag = Console.ReadLine();
                post.Update(newTitle, newContent, newHeshTag);
            }
            else if (item is NewsItem news)
            {
               
                var categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i} - {categories[i]}");
                }
                int selectedIndex;
                while (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 0 || selectedIndex >= categories.Count)
                {
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз:");
                }
                Category selectedCategory = categories[selectedIndex];
                news.Update(newTitle, newContent, selectedCategory);
            }
            else if (item is Announcment ann)
            {
                Console.Write("Це термінове оголошення? (так/ні): ");
                bool isUrgent = Console.ReadLine().Trim().ToLower() == "так";
                ann.Update(newTitle, newContent, isUrgent);
            }
            else
            {
                item.Update(newTitle, newContent);
            }

           
        }


        public List<ContentItem> DeleteById(List<ContentItem> posts, int id) 
        {
            if (!posts.Any() || posts.All(x => x.ID != id))
            {
                Console.WriteLine("Список порожній або неіснуючий id");
                return posts;
            }
            posts.Remove(posts.Find(p => p.ID == id));
            return posts;
        }

       
    }
}
