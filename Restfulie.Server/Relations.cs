using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.DynamicProxy;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server
{
    public class Relations
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly TransitionInterceptor interceptor;
        private readonly ProxyGenerator proxifier;
        private string currentName;
        private readonly IList<Relation> all;

        public Relations(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
            proxifier = new ProxyGenerator();
            all = new List<Relation>();
            interceptor = new TransitionInterceptor(this);
        }

        public T Uses<T>() where T : Controller
        {
            if(string.IsNullOrEmpty(currentName)) throw new ArgumentException("missing name for transition");
            return proxifier.CreateClassProxy<T>(interceptor);
        }

        public void AddToAction(string controller, string action, IDictionary<string, object> values)
        {
            all.Add(new Relation(currentName, urlGenerator.For(controller, action, values)));
            currentName = string.Empty;
        } 

        public Relations Named(string name)
        {
            currentName = name;
            return this;
        }

        public virtual IList<Relation> GetAll()
        {
            var allRelations = new List<Relation>(all);
            all.Clear();

            return allRelations;
        }

        public void At(string url)
        {
            all.Add(new Relation(currentName, url));
            currentName = string.Empty;
        }
    }
}