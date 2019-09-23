//AuthorName : 梁振东;
//CreateDate : 9/21/2019 11:22:51 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
namespace PureMVC.Core
{
	public class Model:IModel
	{
		protected Dictionary<string, IProxy> proxyMap = new Dictionary<string, IProxy>();
        protected static IModel _instance;
        private static readonly object sync_object = new object();
		public Model()
		{
           proxyMap = new Dictionary<string, IProxy>();
		   _instance = this;
		   InitialModel();
		}
		public static IModel GetInstance(System.Func<IModel> modelFunc)
		{
			if(_instance == null)
			{
				_instance = modelFunc();
			}
			return _instance;
		}
		public virtual void InitialModel()
		{

		}
		//注册代理
		public virtual void RegisterProxy(IProxy proxy)
		{
            proxyMap[proxy.ProxyName] = proxy;
			proxy.OnRegister();
		} 
		//移除代理
		public virtual IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;
			if(proxyMap.TryGetValue(proxyName, out proxy))
			{
                proxyMap.Remove(proxyName);
			}
			return proxy;
		}
		//获取代理
		public virtual IProxy RetrieveProxy(string proxyName)
		{
			IProxy proxy = null;
			if(proxyMap.TryGetValue(proxyName, out proxy))
			{
				return proxy;
			}
			return proxy;
		}
		public virtual bool HasProxy(string proxyName)
		{
			return proxyMap.ContainsKey(proxyName);
		}
	} 
}
