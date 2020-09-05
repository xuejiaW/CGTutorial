using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;

public partial class GameResourceManager : Singleton<GameResourceManager>
{
#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);
#endif

    public override void Init()
    {
        base.Init();
        InitConfigEntityData();
    }
    public GameObject Instantiate(string prefabPath)
    {
        GameObject prefab = Resources.Load(prefabPath) as GameObject;
        GameObject result = prefab == null ? null : Object.Instantiate(prefab);
        if (result == null)
            Debug.Log("Create null GO" + prefabPath);
        return result;
    }
}
