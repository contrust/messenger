using System;
using System.Linq;
using NUnit.Framework;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;

namespace Messenger.Tests
{
    public class StarReplacerMessagesHandlerTests
    {
        private StarReplacerMessagesHandler handler { get; set; }
        private static readonly Message message1 = new Message { Sender = new User {Id = 0} , Content = "lol hah"};
        private static readonly Message message2 = new Message { Sender = new User {Id = 0} , Content = "LoL hah"};
        private static readonly Message message3 = new Message { Sender = new User {Id = 0} , Content = "alol hah"};
        private static readonly TestCaseData[] testCases =
        {
            new TestCaseData(null, message1).Returns(null),
            new TestCaseData(null, null).Returns(null),
            new TestCaseData(new Message[] {message1}, null).Returns(null),
            new TestCaseData(new Message[] {message1}, message1).Returns("*** hah"),
            new TestCaseData(new Message[] {message1}, message2).Returns("*** hah"),
            new TestCaseData(new Message[] {message1}, message3).Returns("alol hah"),
        };

        [SetUp]
        public void Setup()
        {
            handler = new StarReplacerMessagesHandler();
        }
        
        [TestCaseSource(nameof(testCases))]
        public string TestMessageContentIsTheSameAsExpectedValue(Message[] oldMessages, Message newMessage)
        {
            var message = handler.HandleMessage(oldMessages, newMessage);
            return message?.Content;
        }
    }
}