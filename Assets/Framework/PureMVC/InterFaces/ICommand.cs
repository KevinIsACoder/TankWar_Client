//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:36:27 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVC.Interfaces
{
	public interface ICommand : INotifier
	{
		void Execute(INotification notification);
	}
}
