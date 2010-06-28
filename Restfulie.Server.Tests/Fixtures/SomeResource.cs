using System;

namespace Restfulie.Server.Tests.Fixtures
{
    [Serializable]
    public class SomeResource : IBehaveAsResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void GetRelations(Relations relations)
        {
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();
        }
    }
}