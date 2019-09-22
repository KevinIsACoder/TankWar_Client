//AuthorName : 梁振东;
//CreateDate : 9/21/2019 6:55:40 PM;
using System.Collections;
using System.Collections.Generic;
using System;
using PureMVC.Interfaces;
using PureMVC.Core;
using PureMVC.Patterns.Observer;
namespace PureMVC.Patterns.Facade
{
	public class Facade : IFacade
	{
		protected IModel model;
		protected IController controller;
		protected IView view;
		protected static IFacade _instance;
		public static IFacade GetInstance(Func<IFacade> facade)
		{
			if(_instance == null)
			{
				_instance = facade();
			}
			return _instance;
		}
		public Facade()
		{
			InitialFacade();
		}
		public virtual void InitialFacade()  //初始化MVC三个模块
		{
			InitialModel();
			InitialView();
			InitialController();
		}
		public virtual void InitialModel()
		{
            model = Model.GetInstance(() => {return new Model();});
		}
		public virtual void InitialView()
		{
			view = View.GetInstance(() => {return new View();});
		}
		public virtual void InitialController()
		{
            controller = Controller.GetInstance(() => {return new Controller();});
		}
		public virtual void RegisterProxy(IProxy proxy)
		{
			model.RegisterProxy(proxy);
		}
		public virtual IProxy RetrieveProxy(string proxyName)
		{
			return model.RetrieveProxy(proxyName);
		}
		public virtual IProxy RemoveProxy(string proxyName)
		{
			return model.RemoveProxy(proxyName);
		}
		public virtual bool HasProxy(string proxyName)
		{
			return model.HasProxy(proxyName);
		}
		public virtual void RegisterCommand(string notificationName, Func<ICommand> command)
		{
            controller.RegisterCommand(notificationName, command);
		}
		public virtual void RemoveCommand(string notificationName)
		{
			controller.RemoveCommand(notificationName);
		}
		public virtual bool HasCommand(string notificationName)
		{
			return controller.HasCommand(notificationName);
		}

		//for mediator
        public virtual void RegisterMediator(IMediator mediator)
		{
			view.RegisterMediator(mediator);
		}
		public IMediator RetrieveMediator(string mediatorName)
		{
			return view.RetrieveMediator(mediatorName);
		}
		public IMediator RemoveMediator(string mediatorName)
		{
            return view.RemoveMediator(mediatorName);
		}
        public virtual bool HasMediator(string mediatorName)
		{
			return view.HasMediator(mediatorName);
		}
		public virtual void NotifyObservers(INotification notification)
		{
            view.NotifyObservers(notification);
		}
		public virtual void SendNotification(string notificationName, object body = null, string Type = null)
		{
			NotifyObservers(new Notification(notificationName, body, Type));
		}
	}
}