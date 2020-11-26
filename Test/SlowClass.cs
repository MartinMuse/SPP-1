using System;
using System.Threading;
using Main;

namespace Test
{
    public class SlowClass
    {
        private ITracer tracer;
    
        public SlowClass(ITracer tracer)
        {
            this.tracer = tracer;
        }
    
        public void Slow()
        {
            tracer.StartTrace();
            Console.WriteLine("Slow");
            Thread.Sleep(150);
            tracer.StopTrace();
        }
    
        public void SuperSlow()
        {
            tracer.StartTrace();
            Console.WriteLine("SuperSlow");
            Thread.Sleep(100);
            Slow();
            tracer.StopTrace();
        }
    }
}