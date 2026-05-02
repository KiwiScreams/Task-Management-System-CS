using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models;

public class Log
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public ActionType ActionType { get; set; }
    public string Message { get; set; }
    public DateTime TimesTamp { get; set; }
}
