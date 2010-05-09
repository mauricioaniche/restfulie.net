using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.DynamicProxy;

namespace Restfulie.Server
{
    public class Transitions
    {
        private readonly TransitionInterceptor interceptor;
        private readonly ProxyGenerator generator;
        private string currentName;
        public IList<Transition> All { get; private set; }

        public Transitions() : this(new AspNetMvcUrlGenerator())
        {
        }

        public Transitions(IUrlGenerator urlGenerator)
        {
            generator = new ProxyGenerator();
            All = new List<Transition>();
            interceptor = new TransitionInterceptor(this, urlGenerator);
        }

        public T Uses<T>() where T : Controller
        {
            return generator.CreateClassProxy<T>(interceptor);
        }

        public void AddTransition(string url)
        {
            All.Add(new Transition(currentName, url));
        } 

        public Transitions Named(string name)
        {
            currentName = name;
            return this;
        }
    }
}