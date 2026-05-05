using System.Threading.Tasks;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services;

public class MenuService
{
    private TaskService taskService = new TaskService();

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\n==== Task Management ====");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Show all tasks");
            Console.WriteLine("3. Get task by Id");
            Console.WriteLine("4. Update task");
            Console.WriteLine("5. Change status");
            Console.WriteLine("6. Delete task");
            Console.WriteLine("7. Filter by status");
            Console.WriteLine("8. Show task logs");
            Console.WriteLine("0. Exit");

            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            if (choice == "1") CreateTaskMenu();
            else if (choice == "2") ShowAllTasksMenu();
            else if (choice == "3") GetTaskByIdMenu();
            else if (choice == "4") UpdateTaskMenu();
            else if (choice == "5") ChangeStatusMenu();
            else if (choice == "6") DeleteTaskMenu();
            else if (choice == "7") FilterByStatusMenu();
            else if (choice == "8") ShowLogsMenu();
            else if (choice == "0") break;
            else Console.WriteLine("Wrong option!");
        }
    }

    private void CreateTaskMenu()
    {
        Console.WriteLine("=== Create Task ===");
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        taskService.CreateTask(title, description);
    }

    private void ShowAllTasksMenu()
    {
        Console.WriteLine("=== All Tasks ===");
        if (taskService.GetAllTasks().Count == 0)
        {
            Console.WriteLine("No tasks found!");
            return;
        }   
        List<TaskItem> tasks = taskService.GetAllTasks();
        PrintTasks(tasks);
    }

    private void GetTaskByIdMenu()
    {
        Console.WriteLine("=== Get Task By Id ===");
        Guid id = ReadGuid();

        TaskItem? task = taskService.GetTaskById(id);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }

        Console.WriteLine($"{task.Id} | {task.Title} | {task.Description} | {task.Status}");
    }

    private void UpdateTaskMenu()
    {
        Console.WriteLine("=== Update Task ===");
        Guid id = ReadGuid();

        Console.Write("New title: ");
        string newTitle = Console.ReadLine();

        Console.Write("New description: ");
        string newDescription = Console.ReadLine();

        taskService.UpdateTask(id, newTitle, newDescription);
    }

    private void ChangeStatusMenu()
    {
        Console.WriteLine("=== Change Task Status ===");
        Guid id = ReadGuid();

        Console.WriteLine("Choose new status:");
        TaskItemStatus newStatus = ReadStatus();

        taskService.ChangeStatus(id, newStatus);
    }

    private void DeleteTaskMenu()
    {
        Console.WriteLine("=== Delete Task ===");
        Guid id = ReadGuid();
        TaskItem task = taskService.GetTaskById(id);
        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }
        Console.WriteLine($"Are you sure you want to delete task {task.Title}?");
        Console.Write("Write yes or no: ");

        string answer = Console.ReadLine();

        if (answer == "yes")
        {
            taskService.DeleteTask(id);
        }
        else
        {
            Console.WriteLine("Delete canceled.");
        }
    }

    private void FilterByStatusMenu()
    {
        Console.WriteLine("=== Filter Tasks By Status ===");
        Console.WriteLine("Choose status:");
        TaskItemStatus status = ReadStatus();

        List<TaskItem> tasks = taskService.FilterByStatus(status);

        PrintTasks(tasks);
    }

    private void ShowLogsMenu()
    {
        Console.WriteLine("=== Show Task Logs ===");
        Guid id = ReadGuid();

        List<Log> logs = taskService.GetLogsByTaskId(id);

        foreach (Log log in logs)
        {
            Console.WriteLine($"{log.TaskId} | {log.ActionType} | {log.Message} | {log.TimesTamp}");
        }
    }

    private Guid ReadGuid()
    {
        while (true)
        {
            Console.Write("Task Id: ");
            string input = Console.ReadLine();

            try
            {
                Guid id = Guid.Parse(input);
                return id;
            }
            catch
            {
                Console.WriteLine("Wrong Id format. Try again.");
            }
        }
    }

    private TaskItemStatus ReadStatus()
    {
        Console.WriteLine("1. ToDo");
        Console.WriteLine("2. InProgress");
        Console.WriteLine("3. Testing");
        Console.WriteLine("4. Done");

        Console.Write("Choose status: ");
        string choice = Console.ReadLine();

        if (choice == "1") return TaskItemStatus.ToDo;
        if (choice == "2") return TaskItemStatus.InProgress;
        if (choice == "3") return TaskItemStatus.Testing;
        if (choice == "4") return TaskItemStatus.Done;

        return TaskItemStatus.ToDo;
    }

    private void PrintTasks(List<TaskItem> tasks)
    {
        foreach (TaskItem task in tasks)
        {
            Console.WriteLine($"{task.Id} | {task.Title} | {task.Description} | {task.Status}");
        }
    }
}