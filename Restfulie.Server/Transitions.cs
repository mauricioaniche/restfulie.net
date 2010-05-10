using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.DynamicProxy;
using Restfulie.Server.ResourceRepresentation.UrlGenerators;

namespace Restfulie.Server
{
    public class Transitions
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly TransitionInterceptor interceptor;
        private readonly ProxyGenerator generator;
        private string currentName;
        public IList<Transition> All { get; private set; }

        public Transitions(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
            generator = new ProxyGenerator();
            All = new List<Transition>();
            interceptor = new TransitionInterceptor(this);
        }

        public T Uses<T>() where T : Controller
        {
            if(string.IsNullOrEmpty(currentName)) throw new ArgumentException("missing name for transition");
            return generator.CreateClassProxy<T>(interceptor);
        }

        public void AddTransition(string controller, string action)
        {
            All.Add(new Transition(currentName, controller, action, urlGenerator.For(controller, action)));
            currentName = string.Empty;
        } 

        public Transitions Named(string name)
        {
            currentName = name;
            return this;
        }
    }
}