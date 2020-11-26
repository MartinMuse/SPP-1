using System;
using System.IO;

namespace Main
{
    public interface ISerializer
    {
        public void Serialize(Object o, StreamWriter stream);
    }
}