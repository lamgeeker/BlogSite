using Blog;
using PostInfo;

List<Post> posts = new List<Post>();
BlogSite blog = new BlogSite();
while (true)
{
    Console.WriteLine("1----Додати новий пост    2-----Вивести всі пости   3-----Посортувати пости за датою створення і вивсети відсортований список");
    int choice = int.Parse(Console.ReadLine());
    Console.WriteLine();
    switch (choice)
    {
        case 1:
            Console.WriteLine("Введіть заголовок статті");
            string title = Console.ReadLine();
            Console.WriteLine("Напишіть контент статті");
            string content = Console.ReadLine();
            Console.WriteLine("Напишіть ім'я та прізвище автора");
            string Name = Console.ReadLine();
            string Surname = Console.ReadLine();
            Console.WriteLine("Напишіть дату створення поста (день.місяць.рік 12:30:39(на приклад)");
            string createdAt = Console.ReadLine();
            blog.AddPost(title, content, new Author() { Name = Name, Surname = Surname }, posts, createdAt);
            break;
        case 2:
            blog.PostList(posts);
            break;

        case 3:
            blog.ShowSortedList(blog.SortByTime(posts));
            break;
    }
}
