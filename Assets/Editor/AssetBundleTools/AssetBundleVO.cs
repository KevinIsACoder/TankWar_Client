using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
// Author:#AUTHORNAME#
// Date:#DATE#
// DESC:#Desc#
public class AssetBundleVO
{
	public class Asset
	{
       public string assetName;
	   public string assetType;
       public string[] depedences; //依赖
	}
	public class Bundle
	{
		public string abName;  //assetBundle name
		public int size; //ab size
		public string md5Value;
		public bool isLeaf;
		public Dictionary<string, Asset> content;
		public int referenceNum; //引用计数，当为0时，应该释放AssetBundle
	}
}
