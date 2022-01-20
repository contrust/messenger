using System;
using System.Linq;
using NUnit.Framework;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;

namespace Messenger.Tests
{
    public class WordDeleterMessagesHandlerTests
    {
        private WordRemoverMessagesHandler handler { get; set; }
        private static readonly Message message1 = new Message { Sender = new User {Id = 0} , Content = "an apple"};
        private static readonly Message message2 = new Message { Sender = new User {Id = 0} , Content = "An apple"};
        private static readonly Message message3 = new Message { Sender = new User {Id = 0} , Content = "anna apple"};
        private static readonly TestCaseData[] testCases =
        {
            new TestCaseData(null, message1).Returns(null),
            new TestCaseData(null, null).Returns(null),
            new TestCaseData(new Message[] {message1}, null).Returns(null),
            new TestCaseData(new Message[] {message1}, message1).Returns("apple"),
            new TestCaseData(new Message[] {message1}, message2).Returns("apple"),
            new TestCaseData(new Message[] {message1}, message3).Returns("anna apple"),
        };

        [SetUp]
        public void Setup()
        {
            handler = new WordRemoverMessagesHandler();
        }
        
        [TestCaseSource(nameof(testCases))]
        public string TestMessageContentIsTheSameAsExpectedValue(Message[] oldMessages, Message newMessage)
        {
            var message = handler.HandleMessage(oldMessages, newMessage);
            return message?.Content;
        }
    }
}