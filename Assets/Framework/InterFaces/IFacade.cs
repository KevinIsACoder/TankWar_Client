//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:35:55 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PureMVC.Interfaces
{
	public interface IFacade : INotifier
	{
		void RegisterProxy(IProxy proxy);
		IProxy RemoveProxy(string proxyName);
		IProxy RetrieveProxy(string proxyName);
		bool HasProxy(string proxyName);

		//for Command
		void RegisterCommand(string notificationName, Func<ICommand> command);
		void RemoveCommand(string notifycationName);
		bool HasCommand(string notificationName);

		//for Mediator
		void RegisterMediator(IMediator mediator);
		IMediator RemoveMediator(string mediatorName);
		IMediator RetrieveMediator(string mediatorName);
		bool HasMediator(string mediatorName);
		void NotifyObservers(INotification notification);
	}
}
