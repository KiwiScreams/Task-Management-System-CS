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
    // Task management
    public void CreateTask(string title, string description)
    {
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
    public void ChangeStatus(Guid taskId, TaskItemStatus newStatus)
    {
        List<TaskItem> tasks = fileService.ReadTasks();

        TaskItem task = FindTaskById(tasks, taskId);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
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