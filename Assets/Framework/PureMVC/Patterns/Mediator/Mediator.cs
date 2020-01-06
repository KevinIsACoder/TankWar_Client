//AuthorName : 梁振东;
//CreateDate : 9/22/2019 5:01:00 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
namespace PureMVC.Patterns.Mediator
{
	public class Mediator : IMediator
	{
		public virtual object ViewComponent
		{
			get;
			set;
		}
		public virtual string MediatorName
		{
			get;
			set; 
		}
		public virtual IList<string> ListNotificationInterests()
		{
			return new List<string>(0);
		}
        public void HandleNotification(INotification notification)
		{

		}
		public virtual void OnRegister()
		{

		}
		public virtual void OnRemove()
		{

		}
	}
}