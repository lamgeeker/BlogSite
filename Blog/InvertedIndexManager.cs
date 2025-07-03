using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostInfo;
namespace Blog
{
    public class InvertedIndexManager
    {
        private Dictionary<string, List<ContentItem>> _index = new(StringComparer.OrdinalIgnoreCase);

        public void BuildIndex(IEnumerable<ContentItem> allItems)
        {
            _index.Clear(); 

            foreach (var item in allItems)
            {
                var words = ExtractWords(item.Title)
                    .Concat(ExtractWords(item.Content));

                foreach (var word in words)
                {
                    if (!_index.ContainsKey(word))
                        _index[word] = new List<ContentItem>();

                   
                    if (!_index[word].Contains(item))
                        _index[word].Add(item);
                }
            }
        }

        
        private IEnumerable<string> ExtractWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Enumerable.Empty<string>();

            return text
                .ToLowerInvariant()
                .Split(new[] { ' ', '.', ',', ';', ':', '?', '!', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        
        public IEnumerable<ContentItem> Search(string keyword)
        {
            return _index.TryGetValue(keyword.ToLowerInvariant(), out var items)
                ? items
                : Enumerable.Empty<ContentItem>();
        }

       
    }

}
