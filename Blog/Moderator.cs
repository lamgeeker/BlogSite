using PostInfo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog
{
    public class Moderator
    {
        private List<ContentItem> _mainContent;
        private readonly object _locker;
        public ConcurrentQueue<ContentItem> ToModerate;
       

        public Moderator(List<ContentItem> mainContent, object locker)
        {
            _mainContent = mainContent;
            _locker = locker;
            ToModerate = new ConcurrentQueue<ContentItem>();
            Task.Run(Moderate);
        }

        public async Task Moderate()
        {
            while (true)
            {
                if (ToModerate.TryDequeue(out ContentItem post))
                {
                    await Task.Delay(500);
                    if (post.Content != null && post.Title != null && post.Author != null)
                    {
                        post.Status = Status.Approved;

                        
                        lock (_locker)
                        {
                            _mainContent.Add(post);
                        }
                    }
                    else
                    {
                        post.Status = Status.Rejected;
                    }
                }
                else
                {
                    await Task.Delay(200);
                }
            }
        }

    }
}
