using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;
public class TCPConnection : MonoBehaviour {
	
	private static readonly object syncObj = new object();
	private TCPConnection _instance;
	public TCPConnection Instance
	{
		get
		{
			lock(syncObj)
			{
				if(_instance == null)
			    {
					GameObject obj = new GameObject("TCPConnection");
					_instance = obj.AddComponent<TCPConnection>();
					GameObject.DontDestroyOnLoad(obj);
				}
				return _instance;
			}
		}
	}
	private byte[] buffer;
	private const int BUFFER_SIZE = 1024;
	private NetworkStream streamToServer;
	void Awake()
	{
		tcpClient = new TcpClient();
	}
	private TcpClient tcpClient;
	void Connect()
	{
		try
		{
			if(Appconst.DebugMode)
			{

			}
		}catch(Exception ex)
		{
			Debug.LogError("TCPConnection Error-----" + ex);
			tcpClient = null;
		}
	}
	void Close()
	{
		if(tcpClient != null)
		{
			tcpClient.Close();
			tcpClient = null;
		}
		if(streamToServer != null)
		{
			streamToServer.Close();
			streamToServer = null;
		}
	}
}
