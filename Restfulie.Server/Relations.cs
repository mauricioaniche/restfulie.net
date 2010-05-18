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
        public virtual IList<Relation> All { get; private set; }

        public Relations(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
            proxifier = new ProxyGenerator();
            All = new List<Relation>();
            interceptor = new TransitionInterceptor(this);
        }

        public T Uses<T>() where T : Controller
        {
            if(string.IsNullOrEmpty(currentName)) throw new ArgumentException("missing name for transition");
            return proxifier.CreateClassProxy<T>(interceptor);
        }

        public void AddTransition(string controller, string action)
        {
            All.Add(new Relation(currentName, controller, action, urlGenerator.For(controller, action)));
            currentName = string.Empty;
        } 

        public Relations Named(string name)
        {
            currentName = name;
            return this;
        }

        public void Reset()
        {
            All.Clear();
        }
    }
}