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
            foreach (ContentItem item in list)
            {
                item.Display();
            }
        }
       
        public List<ContentItem> SortByTime(List<ContentItem> posts)
        {

            return posts.OrderBy(p => p.CreatedAt).ToList();
        }

        public void ShowSortedList(List<ContentItem> posts)
        {
           SortByTime(posts);
            foreach (ContentItem item in posts)
            {
                item.Display();
            }
        }
        
        public void ChangeById(List<ContentItem> posts, int id)
        {
            if(SearchHelper.SearchById(posts, id) != null)
            {
                Console.WriteLine("Оберіть поля, які хочете змінити  (пишіть циифри через пробіл)");
                Console.WriteLine("Заголовок (1)");
                Console.WriteLine("Вміст (2)");
                Console.WriteLine("Автор (3)");
                if(SearchHelper.SearchById(posts,id) is Post)
                {
                    Console.WriteLine("Хештег (4)");
                }
                else if(SearchHelper.SearchById(posts, id) is NewsItem)
                {
                    Console.WriteLine("Категорія (4)");

                }
                else
                    Console.WriteLine("Терміновість (4)");

                string polia = Console.ReadLine();
                foreach(var i in polia.Split(' '))
                {
                    switch(i)
                    {
                        case "1":
                            Console.WriteLine("Напишіть новий заголовок");
                            SearchHelper.SearchById(posts, id).Title;
                            break;

                    }
                }
                /*Console.WriteLine("Змінити заголовок? (так/ні)");
                bool  answer = Console.ReadLine().Trim().ToLower() == "так";
                Console.WriteLine("Змінити контент? (так/ні)");
                bool answer1 = Console.ReadLine().Trim().ToLower() == "так";
                Console.WriteLine("Змінити автора? (так/ні)");
                bool answer2 = Console.ReadLine().Trim().ToLower() == "так";
                if(SearchHelper.SearchById(posts, id) is Post)
                {
                    Console.WriteLine("Змінити хештег? (так/ні)");
                    bool answer3 = Console.ReadLine().Trim().ToLower() == "так";
                }
                else if(SearchHelper.SearchById(posts,id) is NewsItem) 
                {
                    Console.WriteLine("Змінити категорію? (так/ні)");
                    bool answer4 = Console.ReadLine().Trim().ToLower() == "так";
                }
                else
                {
                    Console.WriteLine("Змінити терміновість? (так/ні)");
                    bool answer5 = Console.ReadLine().Trim().ToLower() == "так";
                }*/
            }
            else
                Console.WriteLine("Пост з таким id не знадено!");
        }

        public List<ContentItem> DeleteById(List<ContentItem> posts, int id) 
        {
            posts.Remove(posts.Find(p => p.ID == id));
            return posts;
        }

       
    }
}
