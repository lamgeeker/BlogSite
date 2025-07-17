using Blog;
using PostInfo;
using System.Text;
using file;
using Delegates;

static void Notificate()
{
    Console.WriteLine($"Оновлено {DateTime.Now}");
}

Logger logger = new Logger();
Console.OutputEncoding = UTF8Encoding.UTF8;
var notifier = new UpdateNotification();
notifier.OnNotification += Notificate;
InvertedIndexManager invertedIndex = new InvertedIndexManager();
List<ContentItem> posts = new();
BlogSite blog = new BlogSite();
ISaveService<ContentItem> service = new ContentItemSaveService();
int nextId = service.Load().Count() + 1;

while (true)
{
   
        Console.WriteLine("1----Додати новий пост\n2-----Вивести всі пости\n3-----Посортувати пости за датою створення і вивсети відсортований список\n4-----Зберегти всі пости у файл\n5-----Завантажити пости з файлу\n6------Знайти контент за ключовим словом\n7------Знайти контент за ID\n8------Видалити контент за ID\n9-------Змінити за id");
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
                        continue;
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
                    bool urgent = Console.ReadLine().Trim().ToLower() == "так";


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
            if (item != null)
            {

                posts.Add(item);
                logger.AddLog($"{item.Title} створено у {item.CreatedAt}");
                Console.WriteLine("Контент успішно створено.");
            }


                break;

            case "2":
                blog.ShowList(posts);
                break;

            case "3":
                blog.ShowSortedList(posts);
                break;
            case "4":
            try
            {
                service.Save(posts);
                Console.WriteLine("Пости успішно збережено у файл.");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Помилка при збереженні постів: {ex.Message}");
            }
            break;
        case "5":

                var loaded = service.Load();
                foreach (var Item in service.Load())
                {
                    if (!loaded.Contains(Item))
                    {
                        posts.Add(Item);
                    }
                }
                break;


            case "6":
                Console.WriteLine("Введіть ключове слово, за яким буде здійснено пошук контенту");
                string keyWord = Console.ReadLine();
                invertedIndex.BuildIndex(posts);
                blog.ShowList(invertedIndex.Search(keyWord).ToList());
                break;
            case "7":
            if (!posts.Any())
            {
                Console.WriteLine("Порожній список");
                break;
            }
                Console.WriteLine("Введіть ID, за яким буде здійснений пошук контенту");
                int n = Convert.ToInt32(Console.ReadLine());
                if(SearchHelper.SearchById(posts, n) != null)
                {
                SearchHelper.SearchById(posts, n).Display();
                }
                else
                Console.WriteLine("Неправильно введене id");
            break;
            case "8":
                Console.WriteLine("Введіть ID, за яким буде видалений контент");
                int m = Convert.ToInt32(Console.ReadLine());
                blog.DeleteById(posts, m);
            logger.AddLog($"Видалено у {DateTime.Now}");
            break;
            case "9":
            if (!posts.Any())
            {
                Console.WriteLine("Список порожній!");
                
            }
           else
            {
                Console.WriteLine("Оберіть id поста, дані якого хочете редагувати");
                int idi = Convert.ToInt32(Console.ReadLine());
                blog.ChangeById(idi, posts);
                if (posts.Any(a => a.ID == idi))
                {
                    logger.AddLog($"Редаговано у {DateTime.Now}");
                    notifier.Notify();
                }
                
            }
            break;
            default:
                Console.WriteLine("Оберіть існуючий варіант");
                break;
        }

        }
    
