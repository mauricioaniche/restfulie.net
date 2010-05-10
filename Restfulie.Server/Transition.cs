using System;

namespace Restfulie.Server
{
    public class Transition
    {
        public string Name { get; private set;}
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Url { get; private set; }

        public Transition(string name, string controller, string action, string url)
        {
            Name = name;
            Controller = controller;
            Action = action;
            Url = url;
        }
    }
}
