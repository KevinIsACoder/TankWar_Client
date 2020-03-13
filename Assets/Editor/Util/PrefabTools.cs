using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
//Author : 梁振东
//CreateDate : 20-3-10 下05时29分53秒
//DESC : ****
public class PrefabTools : EditorWindow{

	[MenuItem("PrefabTools/Fix Prefab")]
	static void FixPrefabByFile()
	{
		if(Selection.objects.Length <= 0)
		{
			Debug.LogError("No Selected Object");
			return;
		}
		string path = AssetDatabase.GetAssetPath(Selection.objects[0]);
		if(string.IsNullOrEmpty(path))
		{
			Debug.LogError(string.Format("Asset {0} Path Not Find!", path));
			return;
		}
		string text = File.ReadAllText(path);
		Debug.Log(text);
		if(text.IndexOf("serializedVersion: 6") < 0)
		{
			Debug.LogError("not find string");
		}
		text = text.Replace("serializedVersion: 6", "serializedVersion: 4");
		Debug.Log(text);
		File.WriteAllText(path, text);
		AssetDatabase.Refresh();
	}
}