using System;
using System.Collections.Generic;
using System.Threading;

namespace Main
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}