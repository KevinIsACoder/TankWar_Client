using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
namespace lzdUnityEditor
{
    public class LZDUnityEditor
    {
        private const string root_name = "LZDUnityEditor";
        [MenuItem(root_name + "/CreateBundleObject",false,100)]
        private static void createBundle()
        {
            MyScriptObject.CreateScriptObject<BundleObject>("Bundle");
        }
        [MenuItem(root_name + "/BuildIOSBundle", false, 101)]
        private static void BuildIOSBundle()
        {
            
        }
        [MenuItem(root_name + "/BuildAndroidBundle", false, 102)]
        private static void BuildAndroidBundle()
        {

        }
    }
}