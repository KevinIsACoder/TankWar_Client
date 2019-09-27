using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 9/27/2019 1:27:27 PM
//DESC : ****
public class CollisionMessage : _Message<CollisionMessage>
{
    public class CollisionEvent : _MessageEvent<Collision>{}
    public CollisionEvent OnCollisionStayEvent = new CollisionEvent();
    public CollisionEvent OnCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent OnCollisionExitEvent = new CollisionEvent();

    void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent.InVoke(collision);
    }
    void OnCollisionEnter(Collision collision) 
    {
        OnCollisionEnterEvent.InVoke(collision);
    }
    void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent.InVoke(collision);
    }
}
