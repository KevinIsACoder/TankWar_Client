using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
//AUTHOR : 梁振东
//DATE : 9/26/2019 7:53:32 PM
//DESC : ****
public abstract class _Message<T> : MonoBehaviour 
where T : _Message<T>
{
    protected virtual bool Bind(GameObject obj)
    {
        if(obj == null) return false;
        T message = obj.GetComponent<T>();
        if(message == null)
        {
            message = obj.AddComponent<T>();
        }
        return message != null;
    }
    protected virtual void Dismiss(GameObject obj)
    {
        if(obj == null) return;
        T message = obj.GetComponent<T>();
        if(message != null)
        {
            Destroy(message);
        }
    }
}
public abstract class _MessageEvent
{
    public delegate void MessageEvent();
    public event MessageEvent OnMessageEvent;

    public virtual void AddEvent(MessageEvent eventParam)
    {
        OnMessageEvent += eventParam;
    }
    public virtual void RemoveEvent(MessageEvent eventParam)
    {
        OnMessageEvent -= eventParam;
    }
    public virtual void ClearEvent()
    {
        OnMessageEvent = null;
    }
    public virtual void InVoke()
    {
        if(OnMessageEvent != null)
            OnMessageEvent.Invoke();
    }
}

public delegate void MessageEvent<T>(T args);
public abstract class _MessageEvent<T>
{
    public event MessageEvent<T> OnMessageEvent;
    public virtual void AddEvent(MessageEvent<T> eventParam)
    {
        OnMessageEvent += eventParam;
    }
    public virtual void RemoveEvent(MessageEvent<T> eventParam)
    {
        OnMessageEvent -= eventParam;
    }
    public virtual void ClearEvent()
    {
        OnMessageEvent = null;
    }
    public virtual void InVoke(T args)
    {
        if(OnMessageEvent != null)
            OnMessageEvent.Invoke(args);
    }
}

