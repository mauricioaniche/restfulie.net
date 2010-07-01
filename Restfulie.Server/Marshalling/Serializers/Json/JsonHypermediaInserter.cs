using System;
using System.Collections.Generic;
using System.Linq;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, Relations relations)
        {
            return this.InsertJsonTransitions(content, relations);
        }

        public string Insert(string content, IList<Relations> relations)
        {
            return "[" +
                    "{" +
                        "\"Name\":\"John Doe\"," +
                        "\"Amount\":123.45," +
                        "\"links\":" +
                            "[" +
                                "{" +
                                    "\"rel\":\"pay\"," +
                                    "\"href\":\"http://some/url/pay/john-doe\"" +
                                "}," +
                                "{" +
                                    "\"rel\":\"refresh\"," +
                                    "\"href\":\"http://some/url/refresh/john-doe\"" +
                                "}" +
                            "]" +
                    "}," +
                    "{" +
                        "\"Name\":\"Sally Doe\"," +
                        "\"Amount\":67.89," +
                        "\"links\":" +
                            "[" +
                                "{" +
                                    "\"rel\":\"pay\"," +
                                    "\"href\":\"http://some/url/pay/sally-doe\"" +
                                "}," +
                                "{" +
                                    "\"rel\":\"refresh\"," +
                                    "\"href\":\"http://some/url/refresh/sally-doe\"" +
                                "}" +
                            "]" +
                    "}" +
                "]";
        }

        private string InsertJsonTransitions(string content, Relations relations)
        {
            var jsonTransitions = relations.GetAll().Select((r) => this.GetTransitionOnJsonFormat(r));
            var jsonTransitionsConcatenated = string.Join(",", jsonTransitions.ToArray());

            var jsonWithTransitions = content.Insert(content.Length - 1, ",\"links\":[" + jsonTransitionsConcatenated + "]");

            return jsonWithTransitions;
        }

        private string GetTransitionOnJsonFormat(Relation state)
        {
            var jsonTransition = string.Format("{0}\"rel\":\"{1}\",\"href\":\"{2}\"{3}", "{", state.Name, state.Url, "}");

            return jsonTransition;
        }
    }
}
