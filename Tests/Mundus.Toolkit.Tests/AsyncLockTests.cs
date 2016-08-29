using System.Threading.Tasks;
using MToolkit.Threading;
using NUnit.Framework;

namespace MToolkit.Tests
{
    [TestFixture]
    public class AsyncLockTests
    {
        [Test]
        public async Task AsyncLockTest()
        {
            var result = 0;
            var asyncLock = new AsyncLock();
            var task1 = Task.Run(async () =>
                                       {
                                           using (await GetLockAsync(asyncLock, TaskScheduler.Default))
                                           {
                                               Assert.AreEqual(0, result);
                                               await Task.Delay(1500);
                                               result++;
                                           }
                                       });
            var task2 = Task.Run(async () =>
                                       {
                                           await Task.Delay(500);
                                           using (await GetLockAsync(asyncLock, TaskScheduler.Default))
                                           {
                                               Assert.AreEqual(1, result);
                                           }
                                       });
            await Task.WhenAll(task1, task2);
        }

        private async Task<AsyncLock.LockSubscription> GetLockAsync(AsyncLock asyncLock, TaskScheduler taskScheduler)
        {
            return await asyncLock.LockAsync(taskScheduler);
        }
    }
}
