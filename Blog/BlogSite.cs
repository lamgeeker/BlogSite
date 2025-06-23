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
        


    }
}
