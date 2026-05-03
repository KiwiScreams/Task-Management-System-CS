using System;
using TaskManagementSystem.Services;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        MenuService menuService = new MenuService();

        menuService.Start();
    }
}