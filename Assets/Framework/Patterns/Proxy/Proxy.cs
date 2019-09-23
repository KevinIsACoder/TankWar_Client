using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
namespace PureMVC.Patterns.Proxy
{
    public class Proxy : Notifier, IProxy, INotifier
    {
        public static string Name = "Proxy";
        protected string m_proxyName;
        protected object m_data;
        public Proxy(string name, object data)
        {
            this.m_proxyName =  name;
            this.m_data = data;
        }
        public string ProxyName 
        {
            get
            {
                return m_proxyName;
            }
        }
        public object Data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }
        public virtual void OnRegister()
        {

        }
        public virtual void OnRemove()
        {

        }
    }
}
