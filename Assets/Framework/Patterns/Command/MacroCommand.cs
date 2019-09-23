//AuthorName : 梁振东;
//CreateDate : 9/22/2019 10:12:36 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
namespace PureMVC.Patterns.Command
{
	public class MacroCommand : Notifier, INotifier, ICommand
    {
        private List<Type> subCommands;
        public MacroCommand()
        {
            subCommands = new List<Type>();
            InitialMacroCommand();
        }
        protected virtual void InitialMacroCommand()
        {

        }
        public virtual void Execute(INotification notification)
        {
            foreach(var commandType in subCommands)
            {
                Type command = commandType;
                object commandInstance = Activator.CreateInstance(commandType);
                if(commandInstance is ICommand)
                {
                    ((ICommand)commandInstance).Execute(notification);
                }
            }
            subCommands.Clear();
        }
        public virtual void AddSubCommand(Type commandType)
        {
            subCommands.Add(commandType);
        }
    }
}
