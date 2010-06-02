using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class DefaultResourceMarshaller : IResourceMarshaller
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;

        public DefaultResourceMarshaller(Relations relations, IResourceSerializer serializer)
        {
            this.relations = relations;
            this.serializer = serializer;
        }

        public void Build(ControllerContext context, MarshallingInfo info)
        {
            if (info.HasResource())
            {
                var all = info.Resource.GetRelations(relations);

                var content = serializer.Serialize(info.Resource, all);
                Write(context, content);
                SetContentType(context, serializer.Format);
            }
            else if (info.HasResources())
            {
                var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
                foreach (var resource in info.Resources)
                {
                    var allRelations = resource.GetRelations(relations);
                    listOfResources.Add(resource, allRelations);
                }

                var content = serializer.Serialize(listOfResources);
                Write(context, content);
                SetContentType(context, serializer.Format);
            }
            else if (info.HasMessage())
            {
                Write(context, info.Message);
            }

            SetStatusCode(context, info.StatusCode);
            SetLocation(context, info.Location);
        }

        private void SetLocation(ControllerContext context, string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                context.HttpContext.Response.RedirectLocation = location;
            }
        }

        private void SetStatusCode(ControllerContext context, int statusCode)
        {
            context.HttpContext.Response.StatusCode = statusCode;
        }

        private void SetContentType(ControllerContext context, string type)
        {
            context.HttpContext.Response.ContentType = type;
        }

        private void Write(ControllerContext context, string content)
        {
            context.HttpContext.Response.Output.Write(content);
            context.HttpContext.Response.Output.Flush();
        }
    }
}