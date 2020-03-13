using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LZDFrameWork
{
	public class AssetRef
	{
		private int referenceCount; // 资源引用计数， 当引用计数为0时，释放资源
		private AssetRef[] dependence; //依赖资源
		public AssetRef[] Dependence
		{
			get
			{
				return dependence;
			}
		}
		private AssetBundle bundle;
		public AssetBundle Bundle
		{
			get
			{
				return bundle;
			}
			set
			{
				bundle = value;
			}
		}
		public UnityEngine.Object asset; //资源
		private FSM<AssetRef> fsm; //资源加载状态机,根据不同状态加载资源
		private Dictionary<UnityEngine.Object, Action<UnityEngine.Object>> handlers; //资源加载成功后, 调用回调，参数是加载成功后的资源
		public Dictionary<UnityEngine.Object, Action<UnityEngine.Object>> Handlers
		{
			get
			{
				return handlers;
			}
		}
		private string fileName;
		public string FileName
		{
			get
			{
				return fileName;
			}
		}
		private string url;
		public string LoadUrl
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}
		private UnityEngine.Object handler;
		public UnityEngine.Object Handler
		{
			get
			{
				return handler;
			}
		}
		private System.Type type;
		public System.Type Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
		private Status status;
		public Status Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
				switch(status)
				{
					case Status.LoadingDepedence:
						fsm.ChangeStatus(new LoadingDeps());
					break;
					case Status.Loading:
						fsm.ChangeStatus(new LoadingState());
					break;
					case Status.Loaded:
						fsm.ChangeStatus(new LoadedState());
					break;
					case Status.UnLoading:
						fsm.ChangeStatus(new UnLoadingState());
					break;
					case Status.UnLoaded:
						fsm.ChangeStatus(new UnLoadedState());
					break;
					default:
					break;
				}
			}
		}
		public AssetRef(string fileName, System.Type type)
		{
			this.fileName = fileName;
			this.type = type;
			this.url = utility.DataPath + fileName + Appconst.ExactName;
			referenceCount = 0;
			status = Status.UnLoaded;
			handlers = new Dictionary<UnityEngine.Object, Action<UnityEngine.Object>>();
			fsm = new FSM<AssetRef>(this);
		}
		//加载成功后回调方法
		public void CallBack()
		{
			foreach(var kv in handlers)
			{
				handlers[kv.Key].Invoke(asset);
			}
			handlers.Clear();
		}
		public void Retrieve(UnityEngine.Object obj, Action<UnityEngine.Object> handler)
		{
			if(obj == null)  //obj是宿主，加载资源后的作用对象
				return;
			++referenceCount;
			if(!handlers.ContainsKey(obj))
				handlers.Add(obj, handler);
			else
				handlers[obj] = handler;
			if(asset != null)
			{
				CallBack();
			}
			else
			{
				if(status != Status.LoadingDepedence)
				{
					status = Status.LoadingDepedence;
				}
			}
		}
		public void Release(UnityEngine.Object handler = null)
		{
			referenceCount--;
			if(handlers.ContainsKey(handler)) //移除绑定到object的监听
			{
				handlers.Remove(handler);
			}
			if(referenceCount <= 0)  //当前资源无引用, 释放assetbundle
			{
				status = Status.UnLoading;
			}
		}
		public void UnLoadDeps()
		{
			if(dependence == null) return;
			for(int i = 0; i < dependence.Length; ++i)
			{
				AssetManagerInterval.Instance.UnLoadAsset(dependence[i]);
			}
			dependence = null;
		}
	}
	//资源的各个状态
	public enum Status
	{
		LoadingDepedence,
		Loading,
		Loaded,
		UnLoading,
		UnLoaded,
		Error
	}
	public abstract class FSMState<T>
	{
		public abstract void Enter(T Enity);
		public abstract void Exit(T Enity);
	}
	public class FSM<T>
	{
		public FSMState<T> currentStatus;
        public T owner;
		public FSM(T owner)
		{
			this.owner = owner;
		}
		public void ChangeStatus(FSMState<T> newStatus) 
		{
			if(currentStatus != null)
				currentStatus.Exit(owner);
			currentStatus = newStatus;
			currentStatus.Enter(owner);
		}
	}
	public class LoadingDeps : FSMState<AssetRef>  //加载依赖
	{
		static readonly LoadingDeps instance = new LoadingDeps();
		public static LoadingDeps Instance {get {return instance; }}
		public override void Enter(AssetRef entity)
		{
			HashSet<string> waitQueue = new HashSet<string>();
			AssetRef[] deps;  //依赖资源
			deps = AssetManagerInterval.Instance.LoadDependences(entity, (obj) => 
			{
				if(obj == null)
				{
					entity.Status = Status.Error;
					return;
				}
				if(waitQueue.Count == 0)
				{
					entity.Status = Status.Loading;			
				}
				else
				{
				    if(waitQueue.Contains(obj.name))
					{
						waitQueue.Remove(obj.name);
					}
				}

			});
			if(deps == null)    //如果没有依赖，进行下一个状态，加载资源
			{
				entity.Status = Status.Loading;
				return;
			}
			else
			{
				for(int i = 0; i < deps.Length; ++i)
				{
					if(deps[i] != null && (deps[i].Status == Status.LoadingDepedence || deps[i].Status == Status.Loading))
					{
						waitQueue.Add(deps[i].FileName);
					}
				}
			}
		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class LoadingState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef entity)
		{
			if(entity.asset == null)
			{
				AssetManagerInterval.Instance.Load(entity, (obj) =>
				{
					entity.asset = obj;
					entity.Status = Status.Loaded;
				});
			}
			else
			{
				entity.Status = Status.Loaded;
			}
			     
		}
		public override void Exit(AssetRef entity)
		{
			AssetManagerInterval.Instance.CancleLoading(entity);
		}
	}
	public class LoadedState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef entity)
		{
			entity.CallBack();
		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class UnLoadingState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef entity)
		{
			AssetManagerInterval.Instance.UnLoadAsset(entity);
		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class UnLoadedState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef Enity)
		{
			Enity.Release();
		}
		public override void Exit(AssetRef Enity)
		{

		}
	}
	public class ErrorState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef Enity)
		{

		}
		public override void Exit(AssetRef Enity)
		{

		}
	}
}
