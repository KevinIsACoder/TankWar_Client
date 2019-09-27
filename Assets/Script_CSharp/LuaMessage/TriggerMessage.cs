using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 9/27/2019 12:02:47 PM
//DESC : ****
[DisallowMultipleComponent]
public class TriggerMessage : _Message<TriggerMessage>
{
    public class TriggerEvent : _MessageEvent<Collider>
    {
    }
    public TriggerEvent OnTriggerStayEvent = new TriggerEvent();
    public TriggerEvent OnTriggerEnterEvent = new TriggerEvent();
    public TriggerEvent OnTriggerExitEvent = new TriggerEvent();

    void OnTriggerStay(Collider collider)
    {
        OnTriggerStayEvent.InVoke(collider);
    }
    void OnTriggerEnter(Collider collider)
    {
        OnTriggerEnterEvent.InVoke(collider);
    }
    void OnTriggerExit(Collider collider)
    {
        OnTriggerExitEvent.InVoke(collider);
    }
}