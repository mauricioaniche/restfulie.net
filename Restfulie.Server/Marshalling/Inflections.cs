using System;

namespace Restfulie.Server.Marshalling
{
    public class Inflections : IInflections
    {
        public string Inflect(string name)
        {
            return name + "s";
        }
    }
}
