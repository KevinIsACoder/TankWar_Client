using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
//AUTHOR : 梁振东
//DATE : 9/27/2019 12:01:35 PM
//DESC : ****
[DisallowMultipleComponent]
public class UpdateMessage : _Message<UpdateMessage>
{
    public UpdateEvent updateEvent = new UpdateEvent();
    public UpdateEvent lateUpdateEvent = new UpdateEvent();
    public UpdateEvent fixedUpdateEvent = new UpdateEvent();
    void Update()
    {
        updateEvent.InVoke();
    }
    void LateUpdate()
    {
        lateUpdateEvent.InVoke();
    }
    void FixedUpdate()
    {
        fixedUpdateEvent.InVoke();
    }
}
public class UpdateEvent : _MessageEvent
{
}
