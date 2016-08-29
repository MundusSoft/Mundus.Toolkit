using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MToolkit.Messaging;
using MToolkit.Threading;
using NSubstitute;
using NUnit.Framework;

namespace MToolkit.Tests
{
    [TestFixture]
    public class ExtensionMethodTests
    {
        #region sequential guid tests...

        [Test]
        public void ToSequentialAtEndTest()
        {
            var list = new List<Guid>();
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 800; i++)
            {
                list.Add(Guid.NewGuid().ToSequentialAtEnd());
                Thread.Sleep(4);
            }
            watch.Stop();
            Console.WriteLine($"generated 800 sequential at end guids in {watch.Elapsed} ");
            for (int i = 0; i < 799; i++)
            {
                var current = list[i].ToString("N");
                var next = list[i + 1].ToString("N");
                Assert.AreNotEqual(next, current);
                Assert.Greater(next.Substring(next.Length - 12), current.Substring(current.Length - 12));
            }

        }

        [Test]
        public void ToSequentialAsStringTest()
        {
            var list = new List<Guid>();
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 800; i++)
            {
                list.Add(Guid.NewGuid().ToSequentialAsString());
                Thread.Sleep(1);
            }
            watch.Stop();
            Console.WriteLine($"generated 800 sequential as string guids in {watch.Elapsed} ");
            for (int i = 0; i < 799; i++)
            {
                var current = list[i].ToString("N");
                var next = list[i + 1].ToString("N");
                Assert.AreNotEqual(next, current);
                Assert.Greater(next.Substring(0, 12), current.Substring(0, 12));
            }

        }

        [Test]
        public void ToSequentialAsBinaryTest()
        {
            var list = new List<Guid>();
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 800; i++)
            {
                list.Add(Guid.NewGuid().ToSequentialAsBinary());
                Thread.Sleep(1);
            }
            watch.Stop();
            Console.WriteLine($"generated 800 sequential as binary guids in {watch.Elapsed} ");
            for (int i = 0; i < 799; i++)
            {
                var current = list[i];
                var next = list[i + 1];
                Assert.AreNotEqual(next, current);
                Assert.GreaterOrEqual(next, current);
            }

        }

        #endregion

    }
}
