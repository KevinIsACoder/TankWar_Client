using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author:#AUTHORNAME#
// Date:#DATE#
// DESC:#Desc#
public class AssetBundleSettings{
	public static string OTAVersion = "v1.0";
	public string otaPath = "Assets/OTAPath/";
	public string bundleExtension = ".assetBundle";
	public static string bundleAndroidPath = "../Bundles/Android/" + OTAVersion + "/";
	public static string bundleIosPath = ",,/Bundles/IOS/" + OTAVersion + "/";
	//versionpreload 文件
	public static string versionPreloadName = "versionpreload.json";
    //otaversionpreload
	public static string otaversionPreload = "otaversionpreload.json";
}
