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

        public void Build(ControllerContext context, IBehaveAsResource resource, ResponseInfo info)
        {
            var all = resource.GetRelations(relations);
            var content = serializer.Serialize(resource, all);

            SetContent(context, content);
            SetStatusCode(context, info.StatusCode);
            SetLocation(context, info.Location);
            SetContentType(context, serializer.Format);
        }

        public void Build(ControllerContext context, IEnumerable<IBehaveAsResource> resources, ResponseInfo info)
        {
            var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
            foreach (var resource in resources)
            {
                var allRelations = resource.GetRelations(relations);
                listOfResources.Add(resource, allRelations);
            }

            var content = serializer.Serialize(listOfResources);

            SetContent(context, content);
            SetStatusCode(context, info.StatusCode);
            SetLocation(context, info.Location);
            SetContentType(context, serializer.Format);
        }

        public void Build(ControllerContext context, string message, ResponseInfo info)
        {
            SetContent(context, message);
            SetStatusCode(context, info.StatusCode);
            SetLocation(context, info.Location);
            SetContentType(context, "text/plain");
        }

        public void Build(ControllerContext context, ResponseInfo info)
        {
            SetStatusCode(context, info.StatusCode);
            SetLocation(context, info.Location);
            SetContentType(context, serializer.Format);
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

        private void SetContent(ControllerContext context, string content)
        {
            context.HttpContext.Response.Output.Write(content);
            context.HttpContext.Response.Output.Flush();
        }
    }
}