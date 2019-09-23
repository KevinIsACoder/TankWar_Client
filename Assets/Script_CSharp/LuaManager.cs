using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using XLua;
using System;
//AUTHOR : 梁振东
//DATE : 9/18/2019 10:34:54 AM
//DESC : ****
public class LuaManager
{
    private LuaManager _instance;
    public LuaManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new LuaManager();
            }
            return _instance;
        }
    }
    private LuaEnv luaEnv = null;
    private AssetBundle luaBundle;
    private Action LuaStart;
    private Action LuaExit;
    public void Init()
    {
        luaEnv = new LuaEnv();
        if(Appconst.DebugMode)
        {
            luaEnv.AddLoader(LoadFromFile);  //加载Lua代码
        }
        else
        {
            luaEnv.AddLoader(LoadFromBundle);
        }
        luaEnv.DoString("require 'main'"); //xlua建议整个程序就一个Dostring("require 'main'"), 然后在main中加载其他模块
        LuaStart = luaEnv.Global.Get<Action>("Start");
        LuaExit = luaEnv.Global.Get<Action>("Exit");
        //执行Lua逻辑
        LuaStart();
    }
    byte[] LoadFromFile(ref string fileName)
    {
        byte[] bytes = null;
        fileName = fileName.Replace(".", "/");  //返还给调试器的路径
        string filePath = utility.DataPath + Appconst.LuaDir + "/" + fileName + ".lua";
        if(!Directory.Exists(filePath))
        {
            Debug.LogError("Lua Directory not Exist, Please Export IOS/Android First");
            return null;
        }
        bytes = System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
        return bytes;
    }
    byte[] LoadFromBundle(ref string fileName)
    {
        byte[] bytes = null;
        string luadDir = utility.DataPath + Appconst.LuaDir + "/" + Appconst.ExactName;
        if(!File.Exists(luadDir))
        {
            Debug.LogError("Lua Bundle Not Exsit----");
            return null;
        }
        luaBundle = AssetBundle.LoadFromFile(luadDir);
        if(luaBundle != null)
        {
            fileName = fileName.Replace(".", "_") + ".lua.txt";
            try
            {
                TextAsset textAsset = luaBundle.LoadAsset<TextAsset>(fileName);
                bytes = textAsset.bytes;
            }
            catch(System.Exception ex)
            {
                Debug.LogError("Read Lua Bundle Erro----" + ex.Message);
                luaEnv.Dispose();
            }
        }
        return bytes;
    }
    public void OnExit()
    {
        luaEnv.Dispose();
    }
}
