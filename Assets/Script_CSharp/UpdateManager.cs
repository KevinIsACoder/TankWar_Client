using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 9/23/2019 6:01:02 PM
//DESC : ****
// 热更新模块， 检测资源更新， 下载资源
public class UpdateManager : MonoBehaviour
{
    private const string fileText = "file.txt";
    // Start is called before the first frame update
    void Start()
    {
        //LuaManager.Instance.Init();
        int key = (14 << 10) | 8;
        Debug.Log(key);  
    }

    // Update is called once per frame
    void Update()
    {

    }
}
