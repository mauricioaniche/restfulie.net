using System;

namespace Restfulie.Server.Tests
{
    [Serializable]
    public class SomeResource : IBehaveAsResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public void Transitions(Server.Transitions transitions)
        {
            transitions.Named("pay").Uses<SomeController>().SomeSimpleAction();
        }
    }
}
