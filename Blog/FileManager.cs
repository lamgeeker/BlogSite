using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using PostInfo;
namespace file
{
     class FileManager
    {
        private const string filePath = @"C:\\Users\\Dream Machines\\Documents\\mCompany.txt";
        public void SaveToFile(List<ContentItem> posts)
        {

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            List<ContentItem> existingPosts = Load(); // завантажити старі

            existingPosts.AddRange(posts); // додати нові

            string json = JsonSerializer.Serialize(existingPosts, options);

            File.WriteAllText(filePath, json); // перезаписати весь файл

            Console.WriteLine("Пости успішно додано у файл.");
        }
        public  List<ContentItem> Load()
        {
            
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(json))
            {
                Console.WriteLine("Файл порожній!");
                return new List<ContentItem> { };
            }
            Console.WriteLine("Дані успішно виведені з файлу");
            return JsonSerializer.Deserialize<List<ContentItem>>(json) ?? new List<ContentItem>();
        }
    }
}
