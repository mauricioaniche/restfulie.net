using System;

namespace Restfulie.Server.Marshalling
{
    public class DefaultInflections : IInflections
    {
        public string Inflect(string name)
        {
            return name + "s";
        }
    }
}
