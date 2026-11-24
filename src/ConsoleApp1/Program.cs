namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        // 值类型
        int a = 10;
        int b = a;

        b = 20;

        Console.WriteLine(a); // 10
        Console.WriteLine(b); // 20


        // 引用类型
        var user1 = new User { Name = "张三" };
        var user2 = user1;

        user2.Name = "李四";

        Console.WriteLine(user1.Name); // 李四
        Console.WriteLine(user2.Name); // 李四
    }

    public class User
    {
        public string Name { get; set; }
    }
}