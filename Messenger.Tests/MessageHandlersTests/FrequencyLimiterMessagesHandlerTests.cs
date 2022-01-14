using System;
using System.Linq;
using NUnit.Framework;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;

namespace Messenger.Tests
{
    public class FrequencyLimiterMessagesHandlerTests
    {
        private FrequencyLimiterMessagesHandler handler { get; set; }
        private static readonly Message message1 = new Message { Sender = new User {Id = 0} , Date = new DateTime(1,1,1,1,1, 0)};
        private static readonly Message message2 = new Message { Sender = new User {Id = 1} , Date = new DateTime(1,1,1,1,1, 0)};
        private static readonly Message message3 = new Message { Sender = new User {Id = 1} , Date = new DateTime(1,1,1,1,1, 4)};
        private static readonly Message message4 = new Message { Sender = new User {Id = 1} , Date = new DateTime(1,1,1,1,2, 0)};

        private static readonly TestCaseData[] testCases =
        {
            new TestCaseData(null, null).Returns(null),
            new TestCaseData(null, message1).Returns(null),
            new TestCaseData(Array.Empty<Message>(), message1).Returns(message1),
            new TestCaseData(new Message[] {message1}, message1).Returns(null),
            new TestCaseData(new Message[] {message2}, message1).Returns(message1),
            new TestCaseData(new Message[] {message2}, message3).Returns(null),
            new TestCaseData(new Message[] {message2, message3}, message4).Returns(message4),
            new TestCaseData(new Message[] {message1}, null).Returns(null),
            new TestCaseData(new Message[] {null}, message1).Returns(message1),
            new TestCaseData(new Message[] {message1, null}, message1).Returns(null),
            new TestCaseData(new Message[] {message2}, message4).Returns(message4),
            new TestCaseData(new Message[] {message4}, message1).Returns(message1),
        };

        [SetUp]
        public void Setup()
        {
            handler = new FrequencyLimiterMessagesHandler();
        }
        
        [TestCaseSource(nameof(testCases))]
        public Message TestMessageIsTheSameAsExpectedValue(Message[] oldMessages, Message newMessage)
        {
            var message = handler.HandleMessage(oldMessages, newMessage);
            return message;
        }
    }
}