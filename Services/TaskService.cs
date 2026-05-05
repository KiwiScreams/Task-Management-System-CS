using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TaskManagementSystem.Models;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Services;

public class TaskService
{
    private FileService fileService = new FileService();
    private TaskItem FindTaskById(List<TaskItem> tasks, Guid taskId)
    {
        return tasks.FirstOrDefault(t => t.Id == taskId);
    }
    private bool TextValidation(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Task title and description cannot be empty.");
            return false;
        }

        return true;
    }

    private string CleanText(string text)
    {
        if (text == null)
        {
            return "";
        }

        return text.Trim();
    }
    // Task management
    public void CreateTask(string title, string description)
    {
        if (!TextValidation(title, description))
        {
            return;
        }

        title = CleanText(title);
        description = CleanText(description);
        List<TaskItem> tasks = fileService.ReadTasks();

        TaskItem newTask = new TaskItem();

        newTask.Id = Guid.NewGuid();
        newTask.Title = title;
        newTask.Description = description;
        newTask.Status = TaskItemStatus.ToDo;
        newTask.CreatedAt = DateTime.Now;

        tasks.Add(newTask);
        fileService.WriteTasks(tasks);

        AddLog(newTask.Id, ActionType.Created, $"Task created: {newTask.Title}");
    }

    public List<TaskItem> GetAllTasks()
    {
        return fileService.ReadTasks();
    }
    public TaskItem GetTaskById(Guid taskId)
    {
        List<TaskItem> tasks = fileService.ReadTasks();
        TaskItem task = FindTaskById(tasks, taskId);
        return task;
    }
    public void UpdateTask(Guid taskId, string newTitle, string newDescription)
    {
        if (!TextValidation(newTitle, newDescription))
        {
            return;
        }

        newTitle = CleanText(newTitle);
        newDescription = CleanText(newDescription);
        List<TaskItem> tasks = fileService.ReadTasks();
        TaskItem task = FindTaskById(tasks, taskId);
        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }
        if (task.Title == newTitle && task.Description == newDescription)
        {
            Console.WriteLine("Nothing changed.");
            return;
        }
        task.Title = newTitle;
        task.Description = newDescription;

        fileService.WriteTasks(tasks);
        AddLog(taskId, ActionType.Updated, $"Task updated: {task.Title}");
    }
    public List<TaskItem> FilterByStatus(TaskItemStatus status)
    {
        List<TaskItem> tasks = fileService.ReadTasks();
        List<TaskItem> filteredTasks = tasks.Where(t => t.Status == status).ToList();
        return filteredTasks;
    }
    public void ChangeStatus(Guid taskId, TaskItemStatus newStatus)
    {
        List<TaskItem> tasks = fileService.ReadTasks();

        TaskItem task = FindTaskById(tasks, taskId);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }
        if (task.Status == newStatus)
        {
            Console.WriteLine("Status is already the same. Nothing changed.");
            return;
        }

        task.Status = newStatus;
        fileService.WriteTasks(tasks);
        AddLog(taskId, ActionType.StatusChanged, $"Task status changed to: {newStatus}");
    }
    
    public void DeleteTask(Guid taskId)
    {
        List<TaskItem> tasks = fileService.ReadTasks();

        TaskItem task = FindTaskById(tasks, taskId);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }

        tasks.Remove(task);
        fileService.WriteTasks(tasks);
        AddLog(taskId, ActionType.Deleted, $"Task deleted: {task.Title}");
    }
    // Logs management
    public List<Log> GetLogsByTaskId(Guid taskId)
    {
        List<Log> logs = fileService.ReadLogs();
        List<Log> taskLogs = logs.Where(l => l.TaskId == taskId).ToList();
        return taskLogs;
    }
    private void AddLog(Guid taskId, ActionType actionType, string message)
    {
        List<Log> logs = fileService.ReadLogs();

        Log newLog = new Log();

        newLog.Id = Guid.NewGuid();
        newLog.TaskId = taskId;
        newLog.ActionType = actionType;
        newLog.Message = message;
        newLog.TimesTamp = DateTime.Now;

        logs.Add(newLog);
        fileService.WriteLogs(logs);
        Console.WriteLine(message);
    }
}