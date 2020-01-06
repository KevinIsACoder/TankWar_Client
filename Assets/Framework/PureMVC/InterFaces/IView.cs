//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:23:38 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
namespace PureMVC.Interfaces
{
    public interface IView
    {
		void RegisterObserver(string notificationName, IObserver observer); //注册观察者
		
		void RemoveObserver(string notificationName, object notifycontext);  //移除观察者，observer

		void NotifyObservers(INotification notification); //通过消息通知观察者
		void RegisterMediator(IMediator mediator); //注册中介者
		IMediator RemoveMediator(string mediatorName); //移除中介者
		IMediator RetrieveMediator(string mediatorName);
		bool HasMediator(string mediatorName);
    }
}
