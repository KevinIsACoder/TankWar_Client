//AuthorName : #author#;
//CreateDate : #dateTime#;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
namespace PureMVC.Patterns.Observer
{
	public class Notification : INotification
	{
		public Notification(string name, object body = null, string type = null)
		{
			Name = name;
			Body = body;
			Type = type;
		}
		public virtual string Name
		{
			get;
			set;
		}
		public virtual object Body
		{
			get;
			set;
		}
		public virtual string Type
		{
			get;
			set;
		}
	}
}
