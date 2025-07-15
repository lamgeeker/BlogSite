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
    


    public class ContentItemSaveService : ISaveService<ContentItem>
    {
        private const string filePath = "content_items.json";

        // Helper class for serialization/deserialization of polymorphic types
        private class ContentWrapper
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
        }

        public void Save(List<ContentItem> itemsToSave)
        {
            
            List<ContentItem> existingItems = Load();

           
            Dictionary<int, ContentItem> allItemsMap = existingItems.ToDictionary(item => item.ID);

           
            foreach (var item in itemsToSave)
            {
                allItemsMap[item.ID] = item;
            }

          
            List<ContentItem> uniqueItems = allItemsMap.Values.ToList();

            
            List<ContentWrapper> wrappersToSave = uniqueItems.Select(item =>
            {
                string typeName = item.GetType().Name;
                return new ContentWrapper
                {
                    Type = typeName,
                    Data = JsonSerializer.SerializeToElement(item)
                };
            }).ToList();

           
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

           
            string json = JsonSerializer.Serialize(wrappersToSave, options);

     
            File.WriteAllText(filePath, json);
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
                ContentItem item = null; 
                try
                {
                  
                    item = wrapper.Type switch
                    {
                        nameof(Post) => wrapper.Data.Deserialize<Post>(),
                        nameof(NewsItem) => wrapper.Data.Deserialize<NewsItem>(),
                        nameof(Announcment) => wrapper.Data.Deserialize<Announcment>(),
                        _ => throw new NotSupportedException($"Unknown content type: {wrapper.Type}")
                    };
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing item of type {wrapper.Type}: {ex.Message}");
                  
                    continue; 
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                if (item != null)
                {
                    items.Add(item); 
                }
            }

            return items;
        }

    }

}