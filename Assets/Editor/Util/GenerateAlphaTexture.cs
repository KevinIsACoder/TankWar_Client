using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
//Author : 梁振东
//CreateDate : 03/23/2020 10:43:27
//DESC : 生成alpha图片工具
public class GenerateAlphaTexture : EditorWindow
{
	[MenuItem("TextureImportTool/GenerateAlphaTexture")]
	public static void GenerateAlpha()
	{
		var textures = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);
		for(int i = 0, count = textures.Length; i < count; ++i)
		{
			var tex = textures[i];
			var path = AssetDatabase.GetAssetPath(tex); //获取资源路径
			
			//set texture readable
			var texImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			texImporter.isReadable = true;
			if(!texImporter.DoesSourceTextureHaveAlpha())
			{
				Debug.LogError("----This Texture Doesn't Have A Alpha-----" + path);
				return;
			}
            AssetDatabase.ImportAsset(path);
			var newTexture = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
			Color32[] color = newTexture.GetPixels32();
			for(int j = 0; j < color.Length; ++j)
			{
				var cl = color[i];
				color[i] = new Color32(cl.a, cl.a, cl.a, cl.a); 
			}
			newTexture.SetPixels32(color);
			byte[] bytes = newTexture.EncodeToPNG();
			path = path.Split('.')[0] + "_a.png";
			File.WriteAllBytes(path, bytes);

			texImporter.isReadable = false;
			AssetDatabase.ImportAsset(path);
			AssetDatabase.Refresh();
		}
	}
}
