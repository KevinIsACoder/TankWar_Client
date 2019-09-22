//AuthorName : 梁振东;
//CreateDate : 9/21/2019 11:43:01 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
namespace PureMVC.Core
{
    public class View : IView
	{
		protected static IView _instance;
		protected Dictionary<string, IMediator> mediatorMap; //中介者Map
		protected Dictionary<string, List<IObserver>> observerMap; //消息和观察者的关系是一对多的关系
		public View()
		{
            mediatorMap = new Dictionary<string, IMediator>();
			observerMap = new Dictionary<string, List<IObserver>>();
			_instance = this;
			InitialView();
		}
		public static IView GetInstance(Func<IView> viewFunc)
		{
			if(_instance == null)
			{
				_instance = viewFunc();
			}
			return _instance;
		}
		public virtual void InitialView()
		{

		}
		public virtual void RegisterObserver(string notification, IObserver observer)
		{
			if(observerMap.ContainsKey(notification))
			{
				observerMap[notification].Add(observer);

			}
			else
			{
				observerMap.Add(notification, new List<IObserver>(){observer});
			}
		}
		public virtual void RemoveObserver(string notification, object notifyContext)
		{
			if(observerMap.ContainsKey(notification))
			{
				List<IObserver> observer = observerMap[notification];
                for(int count = observer.Count - 1, i = count; i > 0; i--)
				{
					if(observer[i].CompareNotifyContext(notifyContext))
					{
						observer.RemoveAt(i);
						break;
					}
				}
				//如果消息队列里的obsever 空了， 直接就移除消息
				if(observer.Count <= 0) 
					observerMap.Remove(notification);
			}
		}
		//
		public virtual void NotifyObservers(INotification notification)
		{
			if(observerMap.ContainsKey(notification.Name))
			{
				List<IObserver> ref_observers = observerMap[notification.Name];
				List<IObserver> observers = new List<IObserver>(ref_observers); //从原数组中复制一份出来，因为原数组随时会变
				foreach(var observer in observers)
				{
					observer.NotifyObserver(notification);
				}
			}
		}
		public virtual void RegisterMediator(IMediator mediator)
		{
            mediatorMap[mediator.MediatorName] = mediator;
			if(!mediatorMap.ContainsKey(mediator.MediatorName))
			{
				IList<string> interests = mediator.ListNotificationInterests();
				IObserver observer = new Observer(mediator.HandleNotification, mediator);
				foreach(var interest in interests)
				{
					RegisterObserver(interest, observer);
				}
			}
			mediator.OnRegister();
		}
		public virtual IMediator RetrieveMediator(string mediatorName)
		{
            return mediatorMap[mediatorName];
		}
		public virtual IMediator RemoveMediator(string mediatorName)
		{
			IMediator mediator = null;
            if(mediatorMap.ContainsKey(mediatorName))
			{
				mediator = mediatorMap[mediatorName];
				mediatorMap.Remove(mediatorName);
			}
			return mediator;
		}
		public virtual bool HasMediator(string mediatorName)
		{
			return mediatorMap.ContainsKey(mediatorName);
		}
	}
}
