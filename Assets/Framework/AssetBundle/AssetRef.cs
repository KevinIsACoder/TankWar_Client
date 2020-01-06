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
		private AssetBundle assetBundle;
		private UnityEngine.Object asset; //资源
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
		private UnityEngine.Object handler;
		public UnityEngine.Object Handler
		{
			get
			{
				return handler;
			}
		}
		private System.Type type;
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
			referenceCount = 0;
			status = Status.UnLoaded;
			handlers = new Dictionary<UnityEngine.Object, Action<UnityEngine.Object>>();
			fsm = new FSM<AssetRef>(this);
		}
		//加载成功后回调方法
		void CallBack()
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
			if(referenceCount <= 0)  //当前资源无引用, 释放assetbundle
			{
				status = Status.UnLoading;
			}
		}
		public void UnLoadDeps()
		{
			for(int i = 0; i < dependence.Length; ++i)
			{
				//dependence[i].Release()
			}
		}
	}
	public class AssetInfo
	{
		public string name {get; set;} //资源名字
		public System.Type type{get; set;} //类型，texture, object...
		public UnityEngine.Object asset{get; set;}
		public AssetInfo(string fileName, System.Type type, UnityEngine.Object obj)
		{
			this.name = fileName;
			this.type = type;
			this.asset = obj;
		}
	}
	//资源的各个状态
	public enum Status
	{
		LoadingDepedence,
		Loading,
		Loaded,
		UnLoading,
		UnLoaded
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
	sealed class LoadingDeps : FSMState<AssetRef>  //加载依赖
	{
		static readonly LoadingDeps instance = new LoadingDeps();
		public static LoadingDeps Instance {get {return instance; }}
		public override void Enter(AssetRef entity)
		{
			HashSet<string> waitQueue = new HashSet<string>();
			for(int i = 0; i < entity.Dependence.Length; ++i)
			{
					
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
			
		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class LoadedState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef entity)
		{

		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class UnLoadingState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef entity)
		{

		}
		public override void Exit(AssetRef entity)
		{

		}
	}
	public class UnLoadedState : FSMState<AssetRef>
	{
		public override void Enter(AssetRef Enity)
		{

		}
		public override void Exit(AssetRef Enity)
		{

		}
	}
}
