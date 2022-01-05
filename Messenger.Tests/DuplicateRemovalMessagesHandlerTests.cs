using System;
using System.Linq;
using NUnit.Framework;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;

namespace Messenger.Tests
{
    public class DuplicateRemovalMessagesHandlerTests
    {
        private DuplicateRemovalMessagesHandler handler { get; set; }
        private static readonly Message message1 = new Message { Sender = new User {Id = 0} , Content = "similar"};
        private static readonly Message message2 = new Message { Sender = new User {Id = 1} , Content = "similar"};
        private static readonly Message message3 = new Message { Sender = new User {Id = 1} , Content = "different"};

        private static readonly TestCaseData[] expectedValues =
        {
            new TestCaseData(null, null).Returns(null),
            new TestCaseData(null, message1 ).Returns(null),
            new TestCaseData(Array.Empty<Message>(), message1).Returns(message1),
            new TestCaseData(new Message[] {message1}, message1).Returns(null),
            new TestCaseData(new Message[] {message2}, message1).Returns(message1),
            new TestCaseData(new Message[] {message1, message2}, message1).Returns(message1),
            new TestCaseData(new Message[] {message1}, null).Returns(null),
            new TestCaseData(new Message[] {null}, message1).Returns(message1),
            new TestCaseData(new Message[] {message2}, message3).Returns(message3),
        };

        [SetUp]
        public void Setup()
        {
            handler = new DuplicateRemovalMessagesHandler();
        }
        
        [TestCaseSource(nameof(expectedValues))]
        public Message TestMessageIsTheSameAsExpectedValue(Message[] oldMessages, Message newMessage)
        {
            var message = handler.HandleMessage(oldMessages, newMessage);
            return message;
        }
    }
}