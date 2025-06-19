using Blog;
using PostInfo;
using System.Text;
Console.OutputEncoding = UTF8Encoding.UTF8;
List<Post> posts = new List<Post>();
BlogSite blog = new BlogSite();
while (true)
{
    Console.WriteLine("1----Додати новий пост    2-----Вивести всі пости   3-----Посортувати пости за датою створення і вивсети відсортований список");
    string choice = Console.ReadLine();
    Console.WriteLine();
    switch (choice)
    {
        case "1":
            Console.WriteLine("Введіть заголовок статті");
            string title = Console.ReadLine();
           
            Console.WriteLine("Напишіть контент статті");
            string content = Console.ReadLine();
            
            Console.WriteLine("Напишіть ім'я та прізвище автора");
            string Name = Console.ReadLine();
            string Surname = Console.ReadLine();
           
            Console.WriteLine("Напишіть дату створення поста (день.місяць.рік 12:30:39(на приклад)");

            string createdAt = Console.ReadLine();
            try
            {
                blog.AddPost(title, content, new Author(Name, Surname), posts, createdAt, posts.Count+1);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
                
            
            break;
        case "2":
            blog.PostList(posts);
            break;

        case "3":
            blog.ShowSortedList(blog.SortByTime(posts));
            break;
            default:
            Console.WriteLine("Оберіть існуючий варіант");
            break;
    }
}
