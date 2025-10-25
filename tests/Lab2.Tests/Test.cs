using Itmo.ObjectOrientedProgramming.Lab2.Adresses;
using Itmo.ObjectOrientedProgramming.Lab2.Arhivators;
using Itmo.ObjectOrientedProgramming.Lab2.Component;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Enums;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class Test
{
    [Fact]
    public void User_ReceiveMessage_ShouldBeUnread()
    {
        var user = new User("TestUser", 1);
        var message = new Message("Test", "Body", MessagePriority.Low);

        user.GetMessage(message);

        Assert.Single(user.Messages);
        Assert.False(user.Messages[0].IsRead());
    }

    [Fact]
    public void User_ReadUnreadMessage_ShouldMarkAsRead()
    {
        var user = new User("TestUser", 1);
        var message = new Message("Test", "Body", MessagePriority.Low);
        user.GetMessage(message);

        user.ReadMessage(0);

        Assert.True(user.Messages[0].IsRead());
    }

    [Fact]
    public void User_ReadAlreadyReadMessage_ShouldNotChangeState()
    {
        var user = new User("TestUser", 1);
        var message = new Message("Test", "Body", MessagePriority.Low);
        user.GetMessage(message);
        user.ReadMessage(0);

        user.ReadMessage(0);

        Assert.True(user.Messages[0].IsRead());
    }

    [Fact]
    public void ProxyFilter_ShouldBlockLowPriorityMessages()
    {
        var mockAdress = new MockAdress();
        var filter = new ProxyFilter(mockAdress, MessagePriority.Hard);
        var lowPriorityMessage = new Message("Test", "Body", MessagePriority.Low);

        filter.GetMessage(lowPriorityMessage);

        Assert.Empty(mockAdress.ReceivedMessages);
    }

    [Fact]
    public void ProxyFilter_ShouldAllowHighPriorityMessages()
    {
        var mockAdress = new MockAdress();
        var filter = new ProxyFilter(mockAdress, MessagePriority.Medium);
        var highPriorityMessage = new Message("Test", "Body", MessagePriority.Hard);

        filter.GetMessage(highPriorityMessage);

        Assert.Single(mockAdress.ReceivedMessages);
        Assert.Equal(highPriorityMessage, mockAdress.ReceivedMessages[0]);
    }

    [Fact]
    public void DecoratorLogger_ShouldLogAndForwardMessage()
    {
        var mockAdress = new MockAdress();
        var mockLogger = new MockLogger();
        var decorator = new DecaratorLogger(mockAdress, mockLogger);
        var message = new Message("Test", "Body", MessagePriority.Low);

        decorator.GetMessage(message);

        Assert.Single(mockLogger.LoggedMessages);
        Assert.Single(mockAdress.ReceivedMessages);
        Assert.Equal(message, mockLogger.LoggedMessages[0]);
        Assert.Equal(message, mockAdress.ReceivedMessages[0]);
    }

    [Fact]
    public void FormattedArchiver_ShouldCallFormatter()
    {
        var mockFormatter = new MockMessageFormatter();
        var archiver = new FormattedArchiver(mockFormatter);
        var message = new Message("Test", "Body", MessagePriority.Low);

        archiver.Arhivating(message);

        Assert.Single(mockFormatter.FormattedMessages);
        Assert.Equal(message, mockFormatter.FormattedMessages[0]);
    }

    [Fact]
    public void Topic_WithFilteredAddresses_ShouldDeliverMessageOnce()
    {
        var user = new User("TestUser", 1);
        var mockAdress = new MockAdress();

        var topic = new Topic(1, "TestTopic");
        var filteredAddress = new ProxyFilter(mockAdress, MessagePriority.Hard);
        var directAddress = new ComponentAdres(user);

        topic.AddAddress(filteredAddress);
        topic.AddAddress(directAddress);

        var lowPriorityMessage = new Message("Test", "Body", MessagePriority.Low);

        topic.GetMessage(lowPriorityMessage);

        Assert.Single(user.Messages);
        Assert.Empty(mockAdress.ReceivedMessages);
    }

    [Fact]
    public void NotifyAdresses_WithTriggerWord_ShouldNotify()
    {
        var mockNotificationSystem = new MockNotificationSystem();
        var triggerWords = new List<string> { "urgent", "important" };
        var notifyAddress = new NotifyAdresses(mockNotificationSystem, triggerWords);
        var message = new Message("Test", "This is urgent", MessagePriority.Low);

        notifyAddress.Receive(message);

        Assert.Single(mockNotificationSystem.NotifiedMessages);
        Assert.Equal(message, mockNotificationSystem.NotifiedMessages[0]);
    }

    [Fact]
    public void NotifyAdresses_WithoutTriggerWord_ShouldNotNotify()
    {
        var mockNotificationSystem = new MockNotificationSystem();
        var triggerWords = new List<string> { "urgent", "important" };
        var notifyAddress = new NotifyAdresses(mockNotificationSystem, triggerWords);
        var message = new Message("Test", "This is normal", MessagePriority.Low);

        notifyAddress.Receive(message);

        Assert.Empty(mockNotificationSystem.NotifiedMessages);
    }

    [Fact]
    public void CollectionArchiver_ShouldStoreMessages()
    {
        var archiver = new CollectionArhivator();
        var message1 = new Message("Test1", "Body1", MessagePriority.Low);
        var message2 = new Message("Test2", "Body2", MessagePriority.Medium);

        archiver.Arhivating(message1);
        archiver.Arhivating(message2);

        Assert.Equal(2, archiver.Messages.Count);
        Assert.Equal(message1, archiver.Messages[0]);
        Assert.Equal(message2, archiver.Messages[1]);
    }

    [Fact]
    public void User_SendMessage_ShouldDeliverToAddress()
    {
        var user = new User("TestUser", 1);
        var mockAdress = new MockAdress();
        var message = new Message("Test", "Body", MessagePriority.Low);

        user.SendMessage(message, mockAdress);

        Assert.Single(mockAdress.ReceivedMessages);
        Assert.Equal(message, mockAdress.ReceivedMessages[0]);
    }

    [Fact]
    public void Topic_AddMultipleAddresses_ShouldStoreAll()
    {
        var topic = new Topic(1, "TestTopic");
        var mockAdress1 = new MockAdress();
        var mockAdress2 = new MockAdress();

        topic.AddAddress(mockAdress1);
        topic.AddAddress(mockAdress2);

        var message = new Message("Test", "Body", MessagePriority.Low);
        topic.GetMessage(message);

        Assert.Single(mockAdress1.ReceivedMessages);
        Assert.Single(mockAdress2.ReceivedMessages);
        Assert.Equal(message, mockAdress1.ReceivedMessages[0]);
        Assert.Equal(message, mockAdress2.ReceivedMessages[0]);
    }

    [Fact]
    public void CollectionArchiver_Clear_ShouldRemoveAllMessages()
    {
        var archiver = new CollectionArhivator();
        var message1 = new Message("Test1", "Body1", MessagePriority.Low);
        var message2 = new Message("Test2", "Body2", MessagePriority.Medium);
        archiver.Arhivating(message1);
        archiver.Arhivating(message2);

        archiver.Clear();

        Assert.Empty(archiver.Messages);
    }
}