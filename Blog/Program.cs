using Blog;
using PostInfo;
using System.Text;
using file;

Console.OutputEncoding = UTF8Encoding.UTF8;
FileManager fileManager = new FileManager();
List<ContentItem> posts = new();
BlogSite blog = new BlogSite();

while (true)
{
    Console.WriteLine("1----Додати новий пост  2-----Вивести всі пости  3-----Посортувати пости за датою створення і вивсети відсортований список\n4-----Зберегти всі пости у файл   5-----Завантажити пости з файлу");
    string choice = Console.ReadLine();
    Console.WriteLine();
    switch (choice)
    {
        case "1":
            Console.WriteLine("Оберіть тип контенту:");
            Console.WriteLine("1. Звичайний пост");
            Console.WriteLine("2. Новина");
            Console.WriteLine("3. Оголошення");

            string Choice = Console.ReadLine();
            ContentFactory factory;

            switch (Choice)
            {
                case "1":
                    factory = new PostFactory();
                    break;
                case "2":
                    factory = new NewsItemFactory();
                    break;
                case "3":
                    factory = new AnnouncmentFactory();
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    return;
            }
            Console.WriteLine("Введіть заголовок статті");
            string title = Console.ReadLine();
           
            Console.WriteLine("Напишіть контент статті");
            string content = Console.ReadLine();

            Console.Write("Ім’я автора: ");
            string name = Console.ReadLine();

            Console.Write("Прізвище автора: ");
            string surname = Console.ReadLine();
            Author author = new Author(name, surname);
            int id = posts.Count + 1;
            ContentItem item = null;
            if (factory is AnnouncmentFactory)
            {
                Console.Write("Це термінове оголошення? (так/ні): ");
                bool urgent = Console.ReadLine().Trim().ToLower() == "y";

                // Перевантажений метод з extra параметром
                item = ((AnnouncmentFactory)factory).CreateContent(title, content, author, id, urgent);
            }
            else if(factory is NewsItemFactory)
            {
                Console.WriteLine("Напишіть категорію новини");
                string category = Console.ReadLine();
                // Базовий метод
                item = ((NewsItemFactory)factory).CreateContent(title, content, author, id, category);
            }
            else if(factory is PostFactory)
            {
                Console.WriteLine("Напишіть хештег до поста");
                string hashteg = Console.ReadLine();
                item = ((PostFactory)factory).CreateContent(title, content, author, id, hashteg);
            }

            posts.Add(item);
            Console.WriteLine("Контент успішно створено.");



            break;

        case "2":
          blog.ShowList(posts);
            break;

        case "3":
            blog.ShowSortedList(posts);
            break;
             case "4":
            fileManager.SaveToFile(posts);
            break;
            case "5":
            posts = fileManager.Load();
            break;
            default:
            Console.WriteLine("Оберіть існуючий варіант");
            break;
    }
}
