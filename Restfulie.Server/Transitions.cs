using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.DynamicProxy;

namespace Restfulie.Server
{
    public class Transitions
    {
        private readonly ProxyGenerator generator;
        private string currentName;
        public IList<Transition> All { get; private set; }

        public Transitions()
        {
            generator = new ProxyGenerator();
            All = new List<Transition>();
        }

        public T Uses<T>() where T : Controller
        {
            return generator.CreateClassProxy<T>(new TransitionInterceptor(this));
        }

        public void AddTransition()
        {
            All.Add(new Transition(currentName));
        } 

        public Transitions Named(string name)
        {
            currentName = name;
            return this;
        }
    }
}