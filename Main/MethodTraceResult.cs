using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Main
{
    [Serializable]
    public class MethodTraceResult
    {
        public Stopwatch Stopwatch;
        public string name;
        public string clazz;
        public List<MethodTraceResult> methods;
    }
}