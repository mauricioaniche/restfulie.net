using System;
using System.Collections.Generic;

namespace Restfulie.Server.Tests.Fixtures
{
    [Serializable]
    public class SomeResource : IBehaveAsResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public IList<Relation> GetRelations(Relations relations)
        {
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();

            return relations.GetAll();
        }
    }
}