using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Object = UnityEngine.Object;
namespace LZDFrameWork
{
    public class AssetManagerInterval
    {
		private const string manifestName = "AssetBundleManifest";
        private static AssetManagerInterval instance;
        public 	static AssetManagerInterval Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetManagerInterval();
                }
                return instance;
            }
        }
		private AssetBundleManifest assetBundleManifest;
		public AssetBundleManifest AssetBundleManifest
		{
			get
			{
				return assetBundleManifest;
			}
		}
		private Dictionary<string, AssetRef> assetCache = new Dictionary<string, AssetRef>();
        AssetManagerInterval() { }

		public AssetRef LoadAsset(UnityEngine.Object handler, string assetName, Type type, Action<UnityEngine.Object> callback)
		{
			if(string.IsNullOrEmpty(assetName)) return null;
			if(!assetCache.ContainsKey(assetName))
			{
				AssetRef assetRef = new AssetRef(assetName, type);
				assetCache.Add(assetName, assetRef);
			}
			Action<UnityEngine.Object> action = delegate(UnityEngine.Object obj)
			{
				if(obj != null)
					callback.Invoke(obj);
			};
			assetCache[assetName].Retrieve(handler, action);
			return assetCache[assetName];
		}
		public AssetRef[] LoadDependences(AssetRef assetRef, Action<UnityEngine.Object> callback = null)
		{
			string file = Path.Combine(utility.DataPath, Appconst.streammingAssets + Appconst.ExactName);  //load streamingAsset.assetbundle
			AssetBundle bundle;
			AssetRef[] deps = null;
			if(File.Exists(file))
				bundle = AssetBundle.LoadFromFile(file);   //加载StreamingAsset, 获取依赖信息
			else
			{
				Debug.LogError("StreamingAsset.assetbundle not exit!");
				return null;
			}
			if(bundle != null)
				assetBundleManifest = bundle.LoadAsset<AssetBundleManifest>(manifestName);
			string[] dependences = assetBundleManifest.GetAllDependencies(assetRef.FileName);
		    if(dependences != null)
			{
				deps = new AssetRef[dependences.Length];
				for(int i = 0; i < deps.Length; ++i)
				{
					deps[i] = LoadAsset(assetRef.Handler, dependences[i], typeof(UnityEngine.Object), callback);
				}
			}
			return deps;
		}
		public void Load(AssetRef assetRef, Action<UnityEngine.Object> callback)
		{
			if(!assetCache.ContainsKey(assetRef.FileName))
			{
				LoadManager.Instance.Load(assetRef, (obj) =>
				{
					if(obj)
					{
						callback(obj);
						assetRef.asset = obj;
						assetCache.Add(assetRef.FileName, assetRef);
					}
				});
			}
			else
			{
				callback(assetCache[assetRef.FileName].asset);
			}
		}
		public void CancleLoading(AssetRef assetRef)
		{

		}
		public void UnLoadDependence(AssetRef[] deps)  //卸载所有依赖
		{

		}
		public void UnLoadAsset(AssetRef asset)
		{
			
		}
	    public void Clear()
		{
			Resources.UnloadUnusedAssets();
			assetCache.Clear();
		}
    }
}
