using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
namespace LZDFrameWork
{
    public class LoadManager : Singleton<LoadManager>
    {
        private const int MAX_DOWNLOAD_NUM = 5;
        public Queue<AssetRef> waitList = new Queue<AssetRef>();
        public List<WWWTask> downLoadingList = new List<WWWTask>();
        public List<WWWTask> retryLoadList = new List<WWWTask>();

        public void Load(AssetRef assetRef, Action<UnityEngine.Object> callback)
        {
            waitList.Enqueue(assetRef);
            WWWTask task = new WWWTask(assetRef, callback);
			downLoadingList.Add(task);
        }
        //在Update里执行资源的下载， 这种做法可能会带来性能上的影响，后面优化
        public void OnUpdate()
        {
            if (waitList.Count > 0)  //还有没下载完的资源
            {
                if (downLoadingList.Count <= MAX_DOWNLOAD_NUM)
                {
                    // for (int i = 0; i < downLoadingList.Count; ++i)
                    // {
                    //     WWWTask task = downLoadingList[i];
                    //     if (task.isComplete)
                    //     {
                    //         downLoadingList.Remove(task);
                    //     }
                    //     downLoadingList[i].OnUpdate();
                    // }

                }
                else
                {
					for(int i = 0, Count = downLoadingList.Count; i < Count; ++i)
					{
						//WWWTask task
					}
                }
            }
        }
    }
    public class WWWTask
    {
        private WWW www;
        private Action<UnityEngine.Object> callBack;
        private AssetRef assetRef;
        public string error;
        public bool isComplete;
        public WWWTask(AssetRef assetRef, Action<UnityEngine.Object> callback)
        {
            www = new WWW(assetRef.LoadUrl);
            this.assetRef = assetRef;
        }
        public void OnUpdate()
        {
            if (www == null) return;
            if (!string.IsNullOrEmpty(www.error))
            {
                error = www.error;
                isComplete = true;
                return;
            }
            if (www.isDone)
            {
                UnityEngine.Object obj = null;
                if (assetRef.Type == typeof(UnityEngine.Object))
                {
                    obj = www.assetBundle.mainAsset;
                }
                else
                {
                    obj = www.assetBundle.LoadAsset(assetRef.FileName, assetRef.Type);
                }
                error = string.Empty;
                isComplete = true;
                callBack(www.assetBundle);
                Dispose();
            }
        }
        public IEnumerator DownLoad(WWW www)
        {
            yield return null;
        }
        public void Dispose()
        {
            www = null;
            www.Dispose();
            this.assetRef = null;
        }
    }
}