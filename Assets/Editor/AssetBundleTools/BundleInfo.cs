using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BundleInfo{
	public enum BundleType : byte
	{

	}
	public BundleType type;
	public string path;
	public string name;
	public string md5;
	public long length;
	public bool isCompressed;
	public Dictionary<string, AssetInfo> assetMap = new Dictionary<string, AssetInfo>(); 
}
