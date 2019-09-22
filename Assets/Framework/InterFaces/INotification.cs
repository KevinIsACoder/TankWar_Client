//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:34:58 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
namespace PureMVC.Interfaces
{
	public interface INotification
	{
		string Name
		{
			get;
		}
		object Body
		{
			get;
			set;
		}
		string Type
		{
			get;
			set;
		}
	}
}
