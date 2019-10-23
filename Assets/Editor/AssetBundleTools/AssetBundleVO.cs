using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author:#AUTHORNAME#
// Date:#DATE#
// DESC:#Desc#
public class AssetBundleVO
{
	public class AssetInfo
	{
       public string assetName;
       public string[] depedences; //依赖
	   public int referenceNum; //引用计数，当为0时，应该释放AssetBundle
	}
	public class BundleInfo
	{
		public string abName;  //assetBundle name
		public int size; //ab size
		public string md5Value;
	}
}
