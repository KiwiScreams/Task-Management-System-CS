using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskItemStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}