using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services;

public class FileService
{
    private const string TasksFilePath = "../../../Tasks.json";
    private const string LogFilePath = "../../../Logs.json";

    // Tasks management
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

    // Logs management
    public void WriteLogs(List<Log> logs)
    {
        string LogJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(LogFilePath, LogJson);
    }
    public List<TaskManagementSystem.Models.Log> ReadLogs()
    {
        if (!File.Exists(LogFilePath))
        {
            return new List<Log>();
        }

        string LogJson = File.ReadAllText(LogFilePath);

        if (string.IsNullOrWhiteSpace(LogJson))
        {
            return new List<Log>();
        }

        return JsonSerializer.Deserialize<List<Log>>(LogJson)
               ?? new List<Log>();
    }
}
