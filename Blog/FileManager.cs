using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using PostInfo;
namespace file
{
    public class ContentWrapper
    {
        public string Type { get; set; }
        public JsonElement Data { get; set; }
    }


    public class ContentItemSaveService : ISaveService<ContentItem>
    {
        private const string filePath = "content_items.json";

        public void Save(List<ContentItem> items)
        {
            List<ContentWrapper> existing = new List<ContentWrapper>();

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(existingJson))
                {
                    existing = JsonSerializer.Deserialize<List<ContentWrapper>>(existingJson) ?? new List<ContentWrapper>();
                }
            }

            var newItems = items.Select(item => new ContentWrapper
            {
                Type = item.GetType().Name,
                Data = JsonSerializer.SerializeToElement(item, item.GetType())
            });

            existing.AddRange(newItems);

            string finalJson = JsonSerializer.Serialize(existing, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, finalJson); 
        }






        public List<ContentItem> Load()
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return new List<ContentItem>(); 
            }

            string json = File.ReadAllText(filePath);

            var wrappers = JsonSerializer.Deserialize<List<ContentWrapper>>(json);

            List<ContentItem> items = new List<ContentItem>();

            foreach (var wrapper in wrappers)
            {
                ContentItem item = wrapper.Type switch
                {
                    "Post" => wrapper.Data.Deserialize<Post>(),
                    "NewsItem" => wrapper.Data.Deserialize<NewsItem>(),
                    "Announcment" => wrapper.Data.Deserialize<Announcment>(),
                    _ => null
                };

                if (item != null)
                    items.Add(item);
            }

            return items;
        }

    }

}