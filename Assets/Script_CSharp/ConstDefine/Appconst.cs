using System;
using UnityEngine;
public class Appconst{

    public static bool DebugMode = true;
    public static bool bundleMode = false;
    public static string gameName = "LZDGame"; 

    public static string streammingAssets = "StreamingAssets";

    public static string url = "";
    public static string LuaDir = "Assets/Script_Lua/";
    public static string LuaTxtDir = OTAPath + "Script_LuaBytes/";
    public static string ExactName = ".assetbundle";

    public const string OTAPath = "Assets/OTAPath/";

    //server address
    public const string serverAdress = "127.0.0.1";
    public const string port = "8080";
}