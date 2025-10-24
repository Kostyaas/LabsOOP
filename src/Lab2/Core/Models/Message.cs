using Itmo.ObjectOrientedProgramming.Lab2.Core.Enums;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

public class Message
{
    public Message(string title = "", string body = "", MessagePriority priority = MessagePriority.Medium)
    {
        Title = title;
        Body = body;
        Priority = priority;
    }

    public string Title { get; set; }

    public string Body { get; set; }

    public MessagePriority Priority { get; set; }
}