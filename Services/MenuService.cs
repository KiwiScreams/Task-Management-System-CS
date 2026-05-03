using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementSystem.Services;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services;

public class MenuService
{
    private TaskService taskService = new TaskService();
    public void Start()
    {
        while(true)
        {
            Console.WriteLine("==== Task Management ====");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Show all tasks");
            Console.WriteLine("3. Show task by ID");
            Console.WriteLine("4. Update task");
            Console.WriteLine("5. Change status");
            Console.WriteLine("6. Delete task");
            Console.WriteLine("7. Filter task by status");
            Console.WriteLine("8. Get logs of task");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    Console.Write("Create Task");
                    CreateTask();
                    break;
                case "2":
                    Console.WriteLine("All tasks:");
                    break;
                case "3":
                    Console.WriteLine("status change:");
                    break;
                case "4":
                    Console.WriteLine("delete:");
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }
    private void CreateTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();

        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        taskService.CreateTask(title, description);
        Console.WriteLine("Task created successfully!");
    }
}
