using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restfulie.Server.Http;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonHypermediaInjector : IHypermediaInjector
    {
        #region IHypermediaInjector Members

        public string Inject(string content, Relations relations, IRequestInfoFinder requestInfo)
        {
            return InjectJsonTransitions(content, relations);
        }

        public string Inject(string content, IList<Relations> relations, IRequestInfoFinder requestInfo)
        {
            var listOfSingleObjects = SplitContentIntoListOfSingleJsonObjects(content);

            var injectedContent = new StringBuilder();
            injectedContent.Append("[");

            for (var index = 0; index < listOfSingleObjects.Count; index++)
            {
                var singleObject = listOfSingleObjects[index];

                try
                {
                    var singleObjectWithTransition = InjectJsonTransitions(singleObject, relations[index]);
                    injectedContent.Append(singleObjectWithTransition);
                } catch (IndexOutOfRangeException)
                {
                    injectedContent.Append(singleObject);
                } catch (ArgumentOutOfRangeException)
                {
                    injectedContent.Append(singleObject);
                }

                injectedContent.Append(",");
            }
            if (injectedContent.Length > 1)
                // Removes last comma ','
                injectedContent.Remove(injectedContent.Length - 1, 1);

            injectedContent.Append("]");

            return injectedContent.ToString();
        }

        #endregion

        private IList<string> SplitContentIntoListOfSingleJsonObjects(string content)
        {
            var list = new List<string>();

            if (IsContentAnArray(content))
            {
                var firstJsonObjDelimiterIndex = 0;
                var contentAsArrayOfString = content.ToCharArray();
                var stackOfDelimiters = new Stack<char>();

                for (var index = firstJsonObjDelimiterIndex; index < contentAsArrayOfString.Length; index++)
                {
                    var currentChar = contentAsArrayOfString[index];

                    if (currentChar == '{')
                    {
                        if (stackOfDelimiters.Count == 0)
                            firstJsonObjDelimiterIndex = index;

                        stackOfDelimiters.Push(currentChar);
                    } else if (currentChar == '}')
                    {
                        stackOfDelimiters.Pop();

                        if (stackOfDelimiters.Count == 0)
                            list.Add(content.Substring(firstJsonObjDelimiterIndex, (index - firstJsonObjDelimiterIndex) + 1));
                    }
                }
            } else 
                // Content is single json object
                list.Add(content);

            return list;
        }

        private bool IsContentAnArray(string content)
        {
            return content.StartsWith("[");
        }

        private string InjectJsonTransitions(string content, Relations relations)
        {
            var jsonTransitions = relations.GetAll().Select(GetTransitionOnJsonFormat);
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