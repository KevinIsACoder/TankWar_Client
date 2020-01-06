//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:33:07 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
namespace PureMVC.Interfaces
{
	public interface IProxy : INotifier
	{
		string ProxyName
		{
			get;
		}
		object Data
		{
			get;
			set;
		}
		void OnRegister();
		void OnRemove();
	}
}
