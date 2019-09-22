//AuthorName : 梁振东;
//CreateDate : 9/21/2019 6:45:24 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PureMVC.Interfaces;
namespace PureMVC.Patterns.Observer
{
    public class Observer : IObserver
    {
        public Action<INotification> NotifyMethod
        {
            get;
            set;
        }
        public object NotifyContext 
		{
			set;
            get;
		}
		public Observer(Action<INotification> NotifyMethod, object NotifyContext)
		{
			this.NotifyMethod = NotifyMethod;
			this.NotifyContext = NotifyContext;
		}
		public virtual void NotifyObserver(INotification notification)
		{
			if(NotifyMethod != null) NotifyMethod(notification);
		}
		public virtual bool CompareNotifyContext(object obj)
		{
			return NotifyContext.Equals(obj);
		}
    }
}
