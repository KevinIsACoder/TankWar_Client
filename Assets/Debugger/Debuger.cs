using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine;
// Author:梁振东
// Date:10/12/2019 11:43:28 AM
// DESC:debug输出日志到本地
namespace LZDUtils
{
    public class Debuger
    {
		public static bool EnableLog; //是否启用日志写入
		public static bool EnableTime; //是否记录时间
		public static string fileLogPath = Application.persistentDataPath + "/" + Appconst.gameName + "/";
		public static string fileName = Appconst.gameName;
		public const string log_prefix = ">>>>>>>>";
		public static string GetLogContext(string tag, string context)
		{
		    context = tag + ":" + "\n" + log_prefix + context;
			if(EnableTime)
				context += DateTime.Now.ToString("f", DateTimeFormatInfo.InvariantInfo);
			return context;
		}
		public static void Log(string tag, string message)
		{
			Debug.Log(GetLogContext(tag, message));
            if(!EnableLog) return;
			LogToFile(tag, message);
		}
		public static void Log()
		{

		}
		public static void LogWarning(string tag, string message)
		{
			Debug.LogWarning(GetLogContext(tag, message));
			if(!EnableLog) return;
			LogToFile(tag, message);
		}
		public static void LogError(string tag, string message)
		{
			Debug.LogError(GetLogContext(tag, message));
			if(!EnableLog) return;
			LogToFile(tag, message);
		}
		public static void LogToFile(string tag, string context)
		{
			string filePath = fileLogPath + fileName + ".log";
			StreamWriter sw;
			if(!Directory.Exists(fileLogPath))
			{
				Directory.CreateDirectory(fileLogPath);
			}
            try
			{
				sw = File.AppendText(filePath);
				string message = GetLogContext(tag, context);
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
				sw.WriteLine(message);
				sw.Close();
			}
			catch(Exception ex)
			{
				context = GetLogContext(tag, ex.StackTrace);
				Debug.LogError(context);
			}
		}
    }
}
