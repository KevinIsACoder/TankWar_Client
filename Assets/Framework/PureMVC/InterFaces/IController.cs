//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:36:13 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PureMVC.Interfaces
{
	public interface IController
    {
		void RegisterCommand(string notificationName, Type command);
		void RemoveCommand(string notificationName);
		void ExecuteCommand(INotification notification);
		bool HasCommand(string notificationName);
	}
}
