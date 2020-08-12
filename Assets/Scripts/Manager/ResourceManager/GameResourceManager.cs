using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameResourceManager : Singleton<GameResourceManager>
{
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
