using System;

namespace Restfulie.Server.Tests.Fixtures
{
    [Serializable]
    public class SomeResource : IBehaveAsResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public void SetRelations(Relations relations)
        {
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();
        }
    }
}