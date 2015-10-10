using System;
using System.Threading.Tasks;
using MyMvvm.Messaging;
using MyMvvm.Threading;
using NSubstitute;
using NUnit.Framework;

namespace MyMvvm.Tests
{
    [TestFixture]
    public class MessageAggregatorTests
    {
        #region subscribing tests...

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task A_null_subscriber_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator(dispatcher);
            await eventAggregator.SubscribeAsync(null);
        }

        [Test]
        public async Task A_valid_subscriber_is_assigned_as_a_handler_for_its_message_type()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator(dispatcher);

            Assert.False(eventAggregator.HandlerExistsFor(typeof (object)));

            await eventAggregator.SubscribeAsync(handlerStub);

            Assert.True(eventAggregator.HandlerExistsFor(typeof (object)));
        }

        #endregion

        #region unsubscribing tests...

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task A_null_unsubscriber_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator(dispatcher);
            await eventAggregator.UnsubscribeAsync(null);
        }

        [Test]
        public async Task A_valid_subscriber_gets_removed_from_the_handler_list()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator(dispatcher);

            await eventAggregator.SubscribeAsync(handlerStub);
            Assert.True(eventAggregator.HandlerExistsFor(typeof (object)));

            await eventAggregator.UnsubscribeAsync(handlerStub);
            Assert.False(eventAggregator.HandlerExistsFor(typeof (object)));
        }

        #endregion

        #region publishing tests...

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task A_null_message_causes_an_ArgumentNullException()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator(dispatcher);

            await eventAggregator.PublishAsync(null);
        }

        [Test]
        public async Task A_valid_message_is_published_to_all_handlers_on_current_thread()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator(dispatcher);
            var handler1 = Substitute.For<IHandleAsync<object>>();
            var handler2 = Substitute.For<IHandleAsync<object>>();
            await eventAggregator.SubscribeAsync(handler1);
            await eventAggregator.SubscribeAsync(handler2);
            var message = new object();
            await eventAggregator.PublishAsync(message);
#pragma warning disable 4014
            handler1.Received().HandleAsync(message);
            handler2.Received().HandleAsync(message);
#pragma warning restore 4014
        }

        [Test]
        public async Task A_valid_message_is_published_to_all_handlers_on_background_thread()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var eventAggregator = new MessageAggregator(dispatcher);
            var handler1 = Substitute.For<IHandleAsync<object>>();
            var handler2 = Substitute.For<IHandleAsync<object>>();
            await eventAggregator.SubscribeAsync(handler1);
            await eventAggregator.SubscribeAsync(handler2);
            var message = new object();
            await eventAggregator.PublishOnBackgroundThreadAsync(message);
#pragma warning disable 4014
            handler1.Received().HandleAsync(message);
            handler2.Received().HandleAsync(message);
#pragma warning restore 4014
        }

        [Test]
        public async Task A_valid_message_is_published_to_all_handlers_on_ui_thread()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            dispatcher.InvokeOnUIThreadAsync(Arg.Any<Action>()).Returns(action=> Task.Run(action.Arg<Action>()));
            var eventAggregator = new MessageAggregator(dispatcher);
            var handler1 = Substitute.For<IHandleAsync<object>>();
            var handler2 = Substitute.For<IHandleAsync<object>>();
            await eventAggregator.SubscribeAsync(handler1);
            await eventAggregator.SubscribeAsync(handler2);
            var message = new object();
            await eventAggregator.PublishOnUIThreadAsync(message);
#pragma warning disable 4014
            handler1.Received().HandleAsync(message);
            handler2.Received().HandleAsync(message);
#pragma warning restore 4014
        }

        #endregion

        #region handle existence tests...

         [Test]
        public async Task True_returned_when_a_handler_exists_for_a_given_message()
        {
            var dispatcher = Substitute.For<IDispatcher>();
            var handlerStub = Substitute.For<IHandleAsync<object>>();
            var eventAggregator = new MessageAggregator(dispatcher);

            await eventAggregator.SubscribeAsync(handlerStub);

            Assert.True(eventAggregator.HandlerExistsFor(typeof (object)));
        }

         [Test]
         public async Task False_returned_when_no_handlers_exist_for_a_given_message()
         {
             var dispatcher = Substitute.For<IDispatcher>();
             var handlerStub = Substitute.For<IHandleAsync<object>>();
             var eventAggregator = new MessageAggregator(dispatcher);

             Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
         }

        #endregion
    }
}
