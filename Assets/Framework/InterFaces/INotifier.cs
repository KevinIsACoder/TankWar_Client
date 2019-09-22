//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:34:34 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace PureMVC.Interfaces
{
	public interface INotifier
	{
		void SendNotification(string notificationName, object body = null, string type = null);
	}
}
