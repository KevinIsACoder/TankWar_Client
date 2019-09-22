//AuthorName : 梁振东;
//CreateDate : 9/21/2019 3:35:22 PM;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;
namespace PureMVC.Interfaces
{
    public interface IModel
    {
        void RegisterProxy(IProxy proxy);
        IProxy RetrieveProxy(string ProxyName);
        IProxy RemoveProxy(string ProxyName);
        bool HasProxy(string ProxyName);
    }
}