using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//AUTHOR : 梁振东
//DATE : 9/18/2019 4:52:55 PM
//DESC : ****
public class AssetBundleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DowLoadAssetBundle(AssetBundleRef bundleRef, Action<BundleState, AssetBundleRef, string> callback)
    {
        while(!Caching.ready)
        {
            yield return null;
        }

    }
    private IEnumerator DownLoadAssetBundle(List<AssetBundleRef> bundleList, Action<BundleState, AssetBundleRef, string> callback)
    {
        while(!Caching.ready)
        {
            yield return null;
        }
    }
    public class AssetBundleRef
    {
        public string name;
        public bool allocMemory;

    }
    //表示assetbundle的状态
    public enum BundleState
    {
        ERROR,
        COMPLETE
    }
}
