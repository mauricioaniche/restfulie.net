using System.Collections.Generic;

namespace Restfulie.Server
{
    public class Relation
    {
        public string Name { get; private set;}
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Url { get; private set; }
        public IDictionary<string, object> Values { get; private set; }

        public Relation(string name, string controller, string action, IDictionary<string, object> values, string url)
        {
            Name = name;
            Controller = controller;
            Action = action;
            Values = values;
            Url = url;
        }
    }
}
