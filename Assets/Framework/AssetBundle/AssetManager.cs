using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AssetManager : Singleton<AssetManager> 
{
	public void LoadScene(string sceneName, Action<UnityEngine.Object> callback, UnityEngine.Object handler)
	{

	}
	public void LoadTexure(string name, Action<Texture> callback)
	{
		
	}
}
