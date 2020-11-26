using System.Threading;
using Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ITracer tracer = new StackTracer();
            var slow = new SlowClass(tracer);
            slow.SuperSlow();
            var traceResult = tracer.GetTraceResult();
            Assert.IsNotNull(traceResult);
            Assert.AreEqual(1, traceResult.threads.Count,
                string.Format("Expected {0} threads, instead got {1}", 1, traceResult.threads.Count));
        }

        [TestMethod]
        public void TestMethod2()
        {
            ITracer tracer = new StackTracer();
            var thread = new Thread(StartFastMethods);
            thread.Start(tracer);
            StartSuperSlowMethods(tracer);
            thread.Join();


            var traceResult = tracer.GetTraceResult();
            Assert.AreEqual(2, traceResult.threads.Count,
                string.Format("Expected {0} threads, instead got {1}", 2, traceResult.threads.Count));
        }

        [TestMethod]
        public void TestMethod3()
        {
            ITracer tracer = new StackTracer();
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);

            var traceResult = tracer.GetTraceResult();
            Assert.AreEqual(4, traceResult.threads[0].methods.Count,
                string.Format("Expected {0} root methods in main thread, instead got {1}", 4,
                    traceResult.threads[0].methods.Count));

        }
        
        [TestMethod]
        public void TestMethod4()
        {
            ITracer tracer = new StackTracer();
            var thread1 = new Thread(StartSlowMethods);
            thread1.Start(tracer);
            StartSuperFastMethods(tracer);
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);
            thread1.Join();

            var traceResult = tracer.GetTraceResult();

            int methodsAtAll = 0;
            foreach (var thread in traceResult.threads)
            {
                foreach (var method in thread.methods)
                {
                    methodsAtAll += 1 + countNestedMethods(method);
                }
            }

            Assert.AreEqual(6, methodsAtAll,
                string.Format("Expected {0} methods at all, instead got {1}", 6, methodsAtAll));
        }

        private int countNestedMethods(MethodInfo method)
        {
            int nestedMethodCount = 0;
            if (method.methods != null)
            {
                foreach (var childMethod in method.methods)
                {
                    nestedMethodCount += 1 + countNestedMethods(childMethod);
                }
            }

            return nestedMethodCount;
        }

        public static void StartFastMethods(object tracer)
        {
            FastClass fast = new FastClass(tracer as ITracer);
            fast.Fast();
        }
        public static void StartSuperFastMethods(object tracer)
        {
            FastClass fast = new FastClass(tracer as ITracer);
            fast.SuperFast();
        }

        public static void StartSuperSlowMethods(object tracer)
        {
            SlowClass slow = new SlowClass(tracer as ITracer);
            slow.SuperSlow();
        }
        public static void StartSlowMethods(object tracer)
        {
            SlowClass slow = new SlowClass(tracer as ITracer);
            slow.Slow();
        }
    }
}