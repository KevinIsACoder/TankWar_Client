//AuthorName : 梁振东;
//CreateDate : 9/21/2019 11:43:13 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
namespace PureMVC.Core
{
    public class Controller : IController
    {
        protected readonly Dictionary<string, Type> commandMap; //命令集合， 根据收到的指令执行对应的命令
        protected IView view;
        protected static IController _instance;
        private static readonly object m_syncRoot = new object();
        public Controller()
        {
            commandMap = new Dictionary<string, Type>();
            view = View.GetInstance(() => { return new View(); });
            _instance = this;
            InitialController();
        }

        public virtual void InitialController()
        {

        }
        public static IController GetInstance(Func<IController> controllerFunc)
        {
            if (_instance == null)
            {
                _instance = controllerFunc();
            }
            return _instance;
        }
        public virtual void RegisterCommand(string notificationName, Type command)
        {
            if (commandMap.ContainsKey(notificationName)) //在View种注册监听消息
            {
                view.RegisterObserver(notificationName, new Observer(ExecuteCommand, this)); //刚开始搞不懂controller为什么要放一个View, 原来View是负责通知mediator和controller的角色
            }
            commandMap[notificationName] = command;
        }
        public virtual void RemoveCommand(string notificationName) //执行完命令后移除命令应该
        {
            if (commandMap.ContainsKey(notificationName))
                commandMap.Remove(notificationName);
        }
        public virtual bool HasCommand(string notificationName)
        {
            return commandMap.ContainsKey(notificationName);
        }
        // public virtual void ExecuteCommand(INotification notification)
        // {
        // 	if(commandMap.ContainsKey(notification.Name))
        // 	{
        // 	    ICommand command = commandMap[notification.Name]();
        // 		command.Execute(notification);
        // 	}
        //}
        //另一种写法通过反射调用
        public virtual void ExecuteCommand(INotification note)
        {
            Type commandType = null;

            lock (m_syncRoot)
            {
                if (!commandMap.ContainsKey(note.Name)) return;
                commandType = commandMap[note.Name];
            }

            object commandInstance = Activator.CreateInstance(commandType);

            if (commandInstance is ICommand)
            {
                ((ICommand)commandInstance).Execute(note);
            }
        }
    }
}
