//AuthorName : 梁振东;
//CreateDate : 9/21/2019 6:43:13 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
namespace PureMVC.Patterns.Command
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        public virtual void Execute(INotification notification)
        {

        }
    }
}
