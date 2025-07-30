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
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private const string filePath = "content_items.json";

        // Helper class for serialization/deserialization of polymorphic types
        private class ContentWrapper
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
        }

        public async Task SaveToFileAsync(List<ContentItem> itemsToSave)
        {
           await  _semaphore.WaitAsync();
            try
            {
                var existingItems = await LoadFromFileAsync(false);


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


                await File.WriteAllTextAsync(filePath, json);
                Task.WaitAll();
            }
            finally { _semaphore.Release(); }
            
        }

        public async Task<List<ContentItem>> LoadFromFileAsync(bool useLock = true)
        {
            if (useLock)
                await _semaphore.WaitAsync();
            try
            {
                if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                    return new List<ContentItem>();

                string json = await File.ReadAllTextAsync(filePath);
                var wrappers = JsonSerializer.Deserialize<List<ContentWrapper>>(json);
                List<ContentItem> items = new List<ContentItem>();

                foreach (var wrapper in wrappers)
                {
                    try
                    {
                        ContentItem item = wrapper.Type switch
                        {
                            nameof(Post) => wrapper.Data.Deserialize<Post>(),
                            nameof(NewsItem) => wrapper.Data.Deserialize<NewsItem>(),
                            nameof(Announcment) => wrapper.Data.Deserialize<Announcment>(),
                            _ => null
                        };
                        if (item != null) items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                return items;
            }
            finally
            {
                if (useLock)
                    _semaphore.Release();
            }
        }


    }

}