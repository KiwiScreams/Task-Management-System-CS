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
        Console.WriteLine("Task created successfully!");
    }
}