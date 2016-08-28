using System;
using System.Threading;
using System.Threading.Tasks;
using MToolkit.Messaging;
using MToolkit.Threading;
using NSubstitute;
using NUnit.Framework;

namespace MToolkit.Tests
{
    [TestFixture]
    public class MessageAggregatorTests
    {
        #region subscribing tests...

        [Test]
        public async Task A_null_subscriber_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await eventAggregator.SubscribeAsync(null));
        }

        [Test]
        public async Task A_valid_subscriber_is_assigned_as_a_handler_for_its_message_type()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator();

            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));

            await eventAggregator.SubscribeAsync(handlerStub);

            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        #endregion

        #region unsubscribing tests...

        [Test]
        public async Task A_null_unsubscriber_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await eventAggregator.UnsubscribeAsync(null));
        }

        [Test]
        public async Task A_valid_subscriber_gets_removed_from_the_handler_list()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator();

            await eventAggregator.SubscribeAsync(handlerStub);
            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));

            await eventAggregator.UnsubscribeAsync(handlerStub);
            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        #endregion

        #region publishing tests...

        [Test]
        public async Task A_null_message_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator();

            Assert.ThrowsAsync<ArgumentNullException>(async () => await eventAggregator.PublishAsync(null));
        }

        [Test]
        public async Task A_valid_message_is_published_to_all_handlers()
        {
            var dispatcher = new XamlDispatcher();
            var eventAggregator = new MessageAggregator();
            var handler1 = Substitute.For<IHandleAsync<object>>();
            var handler2 = Substitute.For<IHandleAsync<object>>();
            var handler3 = Substitute.For<IHandle<object>>();
            handler1.HandleAsync(Arg.Any<object>()).Returns(async (c) =>
                                                                  {
                                                                      Console.WriteLine(
                                                                                        $"{DateTime.Now} - Starting handler 1...");
                                                                      await Task.Delay(3000);
                                                                      Console.WriteLine(
                                                                                        $"{DateTime.Now} - Finished handler 1...");
                                                                  });
            handler3.When(c => c.Handle(Arg.Any<object>()))
                    .Do((c) =>
                        {
                            Thread.Sleep(500);
                            Console.WriteLine($"{DateTime.Now} - executing handler 3...");
                        });
            handler2.HandleAsync(Arg.Any<object>()).Returns(async (c) =>
                                                                  {
                                                                      Console.WriteLine(
                                                                                        $"{DateTime.Now} - Starting handler 2...");
                                                                      await Task.Delay(3000);
                                                                      Console.WriteLine(
                                                                                        $"{DateTime.Now} - Finished handler 2...");
                                                                  });
            await eventAggregator.SubscribeAsync(handler1);
            await eventAggregator.SubscribeAsync(handler2);
            await eventAggregator.SubscribeAsync(handler3);
            var message = new object();
            await eventAggregator.PublishAsync(message);
#pragma warning disable 4014
            handler1.Received().HandleAsync(message);
            handler2.Received().HandleAsync(message);
            handler3.Received().Handle(message);
#pragma warning restore 4014
        }


        #endregion

        #region handle existence tests...

        [Test]
        public async Task True_returned_when_a_handlerasync_exists_for_a_given_message()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator();

            await eventAggregator.SubscribeAsync(handlerStub);

            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        [Test]
        public async Task True_returned_when_a_handler_exists_for_a_given_message()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandle<object>>();
            var eventAggregator = new MessageAggregator();

            await eventAggregator.SubscribeAsync(handlerStub);

            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        [Test]
        public async Task False_returned_when_no_handlersasync_exist_for_a_given_message()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator();

            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        [Test]
        public async Task False_returned_when_no_handlers_exist_for_a_given_message()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandle<object>>();
            var eventAggregator = new MessageAggregator();

            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
        }

        #endregion
    }
}
