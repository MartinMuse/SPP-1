using System;
using System.Threading;
using Main;

namespace Test
{
    public class FastClass
    {
        private ITracer tracer;
    
        public FastClass(ITracer tracer)
        {
            this.tracer = tracer;
        }
    
        public void SuperFast()
        {
            tracer.StartTrace();
            Console.WriteLine("SuperFast");
            tracer.StopTrace();
        }
    
        public void Fast()
        {
            tracer.StartTrace();
            Console.WriteLine("Fast");
            Thread.Sleep(50);
            SuperFast();
            tracer.StopTrace();
        }
    }
    
}