using PostInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog
{
    

    public interface IIdentifiable
    {
        int ID { get;  }
    }

    public static class SearchHelper
    {
        

        public static List<ContentItem> SearchById(List<ContentItem> posts, int id)
        {
            List<ContentItem> result = new();
            foreach (var item in posts)
            {
                if(item is IIdentifiable identifiable && identifiable.ID == id)
                {
                    result.Add((ContentItem)item);
                }
            }
            return result;
        }
    }


    
}

