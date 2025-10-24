using Itmo.ObjectOrientedProgramming.Lab2.Core.Enums;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

public class UserMessage
{
    public Message Curentmessage { get; set; }

    public MessageStatus Status { get; set; }

    public void Read()
    {
        Status = MessageStatus.Read;
    }

    public bool IsRead()
    {
        return Status == MessageStatus.Read;
    }

    public UserMessage(Message message)
    {
        Curentmessage = message;
        Status = MessageStatus.Unread;
    }
}