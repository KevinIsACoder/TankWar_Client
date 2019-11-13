using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// 资源类
public class AssetInfo
{
    //资源所在的assetbundle
	private BundleInfo bundleInfo;
    public BundleInfo BundleInfo
    {
        get{return bundleInfo;}
    }

    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    private string assetPath;
    public string AssetPath
    {
        get
        {
            return assetPath;
        }
        set
        {
            assetPath = value;
        }
    }
    private string guid;
    public string GUID
    {
        get
        {
            return guid;
        }
        set
        {
            guid = value;
        }
    }
    public Dictionary<string, AssetInfo> dependences = new Dictionary<string, AssetInfo>();
    public void AddDependency(AssetInfo assetDepedence)
    {
        if(assetDepedence == null)
        {
            Debug.LogWarning(string.Format("Invalid Depedence: {0} Dependent {1}", assetPath, assetDepedence.assetPath));
        }
        if(!dependences.ContainsKey(assetDepedence.GUID))
        {
            if(bundleInfo != assetDepedence.BundleInfo)
            {
                dependences.Add(assetDepedence.GUID, assetDepedence);
            }
        }
    }
}
