using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using UnityEngineInternal;
/*
*AUTHOR: #AUTHOR#
*CREATETIME: #CREATETIME#
*DESCRIPTION: 
*/
public class BundleBuilder{

    private static string SPLIGHTLINE = "|";
    public static void BuildBundle(BundleObject bundleobj)
    {
        if (bundleobj.forceRebuild && Directory.Exists(utility.DataPath)) Directory.Delete(utility.DataPath, true); //判断要不要重新生成StreamingAssets
        Directory.CreateDirectory(utility.DataPath);
        AssetDatabase.Refresh();
        CopyFiles(bundleobj);
        ConvertLuaFileToText();
        AssetDatabase.Refresh();
        BuildPipeline.BuildAssetBundles(utility.DataPath, GetBundleList(bundleobj).ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, bundleobj.target);
        if (bundleobj.filetxtName != null) CreateFileList(bundleobj.outPath,bundleobj.filetxtName);
        AssetDatabase.Refresh();
        Debug.Log("Build Complete");
    }
    private static void ConvertLuaFileToText()
    {
        string sourcePath = Appconst.LuaDir;
        string destPath = Appconst.LuaTxtDir;
        if(!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
        string[] files = Directory.GetFiles(sourcePath, "*.lua", SearchOption.AllDirectories);
        foreach(string file in files)
        {
            if(file.EndsWith(".meta")) continue;
            string pathName = Path.GetDirectoryName(file) + "/";
            string newPath = pathName.Replace(sourcePath, destPath);
            Debug.LogError(newPath);
            if(!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);
            string filePath = newPath + "/" + newPath.Replace(destPath, "").Replace("/", "_") + Path.GetFileName(file) + ".txt"; 
            try
            {
                File.Copy(file, filePath, true);
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
    private static void CopyFiles(BundleObject bundleobj)
    {
        foreach(BundleObject.copyInfo info in bundleobj.copyinfo)
        {
            string sourthPath = info.sourthPath;
            string destPath = info.destPath;
            if (sourthPath == string.Empty || destPath == string.Empty) continue;
            if (sourthPath == null) return;
            string[] files = Directory.GetFiles(sourthPath,info.filePattern,info.searchOption);
            foreach(string filePath in files)
            {
                if (filePath.EndsWith(".meta")) continue;
                Directory.CreateDirectory(destPath);
                string newpath = destPath + filePath.Replace(sourthPath, "");
                Directory.CreateDirectory(Path.GetDirectoryName(newpath));
                File.Copy(sourthPath, newpath, true);
            }
        }
    }
    private static List<AssetBundleBuild> GetBundleList(BundleObject bundleobj)
    {
        List<AssetBundleBuild> buildList = new List<AssetBundleBuild>();
        foreach(BundleObject.bundleInfo info in bundleobj.bundleinfo)
        {
            if (string.IsNullOrEmpty(info.bundleName)) { continue; Debug.LogWarning("bundleName is Empty"); }
            if (info.filePattern == "") info.filePattern = "*";
            string[] files = Directory.GetFiles("Assets/" + info.searchPath,info.filePattern,info.searchOption);
            for(int i = 0;i < files.Length;++i)
            {
                files[i] = files[i].Replace("\\", "/");
            }
            AssetBundleBuild abb = new AssetBundleBuild();
            abb.assetBundleName = info.bundleName;
            abb.assetNames = files;
            buildList.Add(abb);
        }
        return buildList;
    }
    private static void CreateFileList(string fileListpath,string filename)
    {
        string filetxtPath = Path.Combine(fileListpath, filename);
        if (File.Exists(filetxtPath)) File.Delete(filetxtPath); //如果file.txt存在了已经，就删除存在的，重新生成
        List<string> fileList = new List<string>();
        TraverseFile(fileListpath, fileList);
        FileStream fs = new FileStream(fileListpath, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);
        foreach (string file in fileList)
        {
            if (file.EndsWith(".meta") || file.EndsWith(".manifest")) continue;
            string md5Value = utility.Md5File(file);
            string relativePath = file.Replace(fileListpath, string.Empty);
            string size = (new FileInfo(file).Length >> 10).ToString();
            sw.WriteLine(relativePath + SPLIGHTLINE + md5Value + SPLIGHTLINE + size);
        }
        sw.Close();
        fs.Close();
    }

    private static void TraverseFile(string filePath,List<string> fileList) //遍历文件
    {
        if (!Directory.Exists(filePath)) return;
        string[] files = Directory.GetFiles(filePath);
        string[] paths = Directory.GetDirectories(filePath);
        foreach(string file in files)
        {
            fileList.Add(file.Replace("\\","/"));
        }
        foreach(string path in paths)
        {
            TraverseFile(path, fileList);
        }
    }
}