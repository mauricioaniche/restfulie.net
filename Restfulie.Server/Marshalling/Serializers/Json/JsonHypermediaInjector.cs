using System;
using System.Collections.Generic;
using System.Linq;
using Restfulie.Server.Request;
using System.Text;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonHypermediaInjector : IHypermediaInjector
    {
        public string Inject(string content, Relations relations, IRequestInfoFinder requestInfo)
        {
            return this.InjectJsonTransitions(content, relations);
        }

        public string Inject(string content, IList<Relations> relations, IRequestInfoFinder requestInfo)
        {
            var listOfSingleObjects = this.SplitContentIntoListOfSingleJsonObjects(content);

            StringBuilder injectedContent = new StringBuilder();
            injectedContent.Append("[");

            for(var index=0; index < listOfSingleObjects.Count; index++)
            {
                var singleObject = listOfSingleObjects[index];

                try
                {
                    var singleObjectWithTransition = this.InjectJsonTransitions(singleObject, relations[index]);
                    injectedContent.Append(singleObjectWithTransition);
                }
                catch (IndexOutOfRangeException)
                {
                    injectedContent.Append(singleObject);
                }
                catch (ArgumentOutOfRangeException)
                {
                    injectedContent.Append(singleObject);
                }

                injectedContent.Append(",");
            }
            if (injectedContent.Length > 1)
            {
                // Removes last comma ','
                injectedContent.Remove(injectedContent.Length - 1, 1);
            }

            injectedContent.Append("]");

            return injectedContent.ToString();
        }

        private IList<string> SplitContentIntoListOfSingleJsonObjects(string content)
        {
            var list = new List<string>();

            if (IsContentAnArray(content))
            {
                int firstJsonObjDelimiterIndex = 0;
                var contentAsArrayOfString = content.ToCharArray();
                Stack<char> stackOfDelimiters = new Stack<char>();

                for (var index = firstJsonObjDelimiterIndex; index < contentAsArrayOfString.Length; index++)
                {
                    var currentChar = contentAsArrayOfString[index];

                    if (currentChar == '{')
                    {
                        if (stackOfDelimiters.Count == 0)
                        {
                            firstJsonObjDelimiterIndex = index;
                        }

                        stackOfDelimiters.Push(currentChar);
                    }
                    else if (currentChar == '}')
                    {
                        stackOfDelimiters.Pop();

                        if (stackOfDelimiters.Count == 0)
                        {
                            list.Add(content.Substring(firstJsonObjDelimiterIndex, (index - firstJsonObjDelimiterIndex) + 1));
                        }

                    }
                }
            }
            else 
            {
                // Content is single json object
                list.Add(content);
            }

            return list;
        }

        private bool IsContentAnArray(string content)
        {
            return content.StartsWith("[");
        }

        private string InjectJsonTransitions(string content, Relations relations)
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
