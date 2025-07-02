using Blog;
using PostInfo;
using System.Text;
using file;

Console.OutputEncoding = UTF8Encoding.UTF8;

List<ContentItem> posts = new();
BlogSite blog = new BlogSite();
ISaveService<ContentItem> service = new ContentItemSaveService();
int nextId = posts.Any() ? posts.Max(p => p.ID) + 1 : 1;

while (true)
{
    Console.WriteLine("1----Додати новий пост\n2-----Вивести всі пости\n3-----Посортувати пости за датою створення і вивсети відсортований список\n4-----Зберегти всі пости у файл\n5-----Завантажити пости з файлу\n6------Знайти контент за ключовим словом\n7------Знайти контент за ID\n8------Видалити контент за ID");
    string choice = Console.ReadLine();
    Console.WriteLine();
    switch (choice)
    {
        case "1":
            int id = nextId;
            nextId++;
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
            
            ContentItem item = null;
            if (factory is AnnouncmentFactory)
            {
                Console.Write("Це термінове оголошення? (так/ні): ");
                bool urgent = Console.ReadLine().Trim().ToLower() == "y";

              
                item = ((AnnouncmentFactory)factory).CreateContent(title, content, author, id, urgent);
            }
            else if (factory is NewsItemFactory)
            {
                Console.WriteLine("Оберіть категорію новини:");
                var categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i} - {categories[i]}");
                }
                int selectedIndex;
                while (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 0 || selectedIndex >= categories.Count)
                {
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз:");
                }
                Category selectedCategory = categories[selectedIndex];
              
                item = ((NewsItemFactory)factory).CreateContent(title, content, author, id, selectedCategory);
            }
            else if (factory is PostFactory)
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
            service.Save(posts);
            break;
        case "5":
            var loaded = service.Load();
            posts.AddRange(loaded);
            Console.WriteLine($"{loaded.Count} елемент(ів) було завантажено з файлу.");
            break;




        case "6":
            Console.WriteLine("Введіть ключове слово, за яким буде здійснено пошук контенту");
            string word = Console.ReadLine();
            blog.ShowList(SearchHelper.SearchByWord(posts, word));
            break;
        case "7":
            Console.WriteLine("Введіть ID, за яким буде здійснений пошук контенту");
            int n = Convert.ToInt32(Console.ReadLine());
            blog.ShowList(SearchHelper.SearchById(posts, n));
            break;
        case "8":
            Console.WriteLine("Введіть ID, за яким буде видалений контент");
            int m = Convert.ToInt32(Console.ReadLine());
            blog.DeleteById(posts, m);
            break;
        default:
            Console.WriteLine("Оберіть існуючий варіант");
            break;
    }
}