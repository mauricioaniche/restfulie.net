using System;

namespace Restfulie.Server
{
    public class Transition
    {
        public string Name { get; private set;}
        public string Url { get; private set; }

        public Transition(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
