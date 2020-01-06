//AuthorName : 梁振东;
//CreateDate : 9/21/2019 6:45:35 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
namespace PureMVC.Patterns.Observer
{
	public class Notifier : INotifier
	{
        public virtual void SendNotification(string notificationName, object body = null, string type = null)
		{
			Facade.SendNotification(notificationName, body, type);
		}
		protected IFacade Facade
		{
			get
			{
				return Patterns.Facade.Facade.GetInstance(() => {return new Facade.Facade();});
			}
		}
	}
}