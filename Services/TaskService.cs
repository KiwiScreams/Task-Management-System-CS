using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services;

public class TaskService
{
    private const string TasksFilePath = "Tasks.json";
    private const string LogFilePath = "Logs.json";

    public void WriteTasks(List<TaskManagementSystem.Models.TaskItem> tasks)
    {
        string TaskJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(TasksFilePath, TaskJson);
    }
    public List<TaskManagementSystem.Models.TaskItem> ReadTasks()
    {
        if (!File.Exists(TasksFilePath))
        {
            return new List<TaskItem>();
        }

        string TaskJson = File.ReadAllText(TasksFilePath);

        if (string.IsNullOrWhiteSpace(TaskJson))
        {
            return new List<TaskItem>();
        }

        return JsonSerializer.Deserialize<List<TaskItem>>(TaskJson)
               ?? new List<TaskItem>();
    }
}
