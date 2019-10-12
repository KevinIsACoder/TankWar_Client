using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 10/8/2019 5:39:28 PM
//DESC : 对象池，用于减少内存的开销
[DisallowMultipleComponent]
public class GameObjectPool<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    public Queue<T> objectPool;
    private T obj; //待实例化物体
    private int maxNum = 5;
    public int MaxNum
    {
        get
        {
            return maxNum;
        }
        set
        {
            maxNum = value;
        }
    }
    private GameObject parent;
    public GameObject Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }
    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
    public void CreateGameObjectPool(int maxNum, T obj)
    {
        this.maxNum = maxNum;
        objectPool = new Queue<T>();
        this.obj = obj;
    }
    public void EnqueObject(T obj)
    {
        if(objectPool.Count > maxNum) //队列已达上限
        {
            return;
        }
        objectPool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
    public T GetObject(Vector3 pos)
    {
        T obj = null;
        if(objectPool.Count > 0)
        {
           obj = objectPool.Dequeue();
           obj.gameObject.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(obj) as T;
            obj.transform.SetParent(transform, false);
            obj.transform.localPosition = pos;
        }
        return obj;
    }
}
