using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Main
{
    [XmlRoot]
    public class TraceResult
    {
        [XmlElement("thread")]
        public List<ThreadInfo> threads { get; }

        public TraceResult()
        {
        }

        public TraceResult(Dictionary<Thread, List<MethodTraceResult>> value)
        {
            threads = new List<ThreadInfo>();
            foreach (var thread in value)
            {
                ThreadInfo threadInfo = new ThreadInfo(thread.Key, thread.Value);

                threads.Add(threadInfo);
            }
        }
    }
    
    public class ThreadInfo
    {
        [XmlAttribute]
        public int id { get; set; }
        [XmlAttribute]
        public string time { get; set;}
        
        [XmlElement("method")]
        public List<MethodInfo> methods { get; }

        public ThreadInfo()
        {
        }

        public ThreadInfo(Thread thread, List<MethodTraceResult> methodsResults)
        {
            this.id = thread.ManagedThreadId;
            this.methods = new List<MethodInfo>();
            long time = 0;
            foreach (var methodResult in methodsResults)
            {
                time += methodResult.Stopwatch.ElapsedMilliseconds;
                methods.Add(new MethodInfo(methodResult));
            }

            this.time = time + "ms";
        }

        public void AddMethod(MethodInfo method)
        {
            methods.Add(method);
        }
    }

    public class MethodInfo
    {
        [XmlAttribute]
        public string name { get; set;}
        [XmlAttribute]
        public string time { get; set;}
        [XmlAttribute("class")]
        [JsonProperty("class")]
        public string ClassName { get;set; }
        [XmlElement("method")]
        public List<MethodInfo> methods { get; }

        public MethodInfo()
        {
        }

        public MethodInfo(MethodTraceResult traceResult)
        {
            name = traceResult.name;
            time = traceResult.Stopwatch.ElapsedMilliseconds + "ms";
            ClassName = traceResult.clazz;
            methods = new List<MethodInfo>();
            if (traceResult.methods != null)
                foreach (var childTraceResult in traceResult.methods)
                {
                    methods.Add(new MethodInfo(childTraceResult));
                }
        }
    }
}