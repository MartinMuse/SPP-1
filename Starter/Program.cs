using System;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Main;
using Newtonsoft.Json;
using Test;
using System.Threading;

namespace Starter
{
    class Program
    {
        private static ITracer tracer;

        public static void Main(string[] args)
        {
            tracer = new StackTracer();
            Thread thread = new Thread(StartFastMethods);
            thread.Start();
            SlowClass slow = new SlowClass(tracer);
            slow.SuperSlow();
            thread.Join();

            Printer printer = new Printer();
            printer.AddSerializer(new JSONSerializerAdapter());
            printer.AddSerializer(new XMLSerializerAdapter());
            printer.AddStream(Console.OpenStandardOutput());
            printer.AddStream("output.txt");
            printer.Print(tracer.GetTraceResult());
        }

        public static void StartFastMethods()
        {
            FastClass fast = new FastClass(tracer);
            fast.Fast();
        }
    }
}