//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:35:43 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVC.Interfaces
{
	public interface IMediator
	{
		string MediatorName
		{
			get;
		}
		object ViewComponent
		{
			get;
			set;
		}
		IList<string> ListNotificationInterests();
		void HandleNotification(INotification notification);
		void OnRegister();
		void OnRemove();
	}
}
