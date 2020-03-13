using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author : 梁振东
//CreateDate : 01/09/2020 14:36:05
//DESC : 范型单例
public class Singleton<T> where T : class, new(){
	private static readonly object syncObject = new object();
    
	private static T instance;
	public static T Instance
	{
		get
		{
			lock(syncObject)
			{
				if(instance == null)
				{
					instance = new T();
				}
				return instance;
			}
		}
	}
}
