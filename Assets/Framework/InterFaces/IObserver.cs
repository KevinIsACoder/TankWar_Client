//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:34:07 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using System;
namespace PureMVC.Interfaces
{
	public interface IObserver
	{
		Action<INotification> NotifyMethod
		{
			get;
			set;
		}
		object NotifyContext
		{
			set;
		}
		void NotifyObserver(INotification notification); //通过消息通知观察者
        bool CompareNotifyContext(object obj);
	}
}
