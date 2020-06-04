using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceManager : Singleton<GameResourceManager>
{
    public GameObject Instantiate(string prefabPath)
    {
        GameObject prefab = Resources.Load(prefabPath) as GameObject;
        GameObject result = prefab == null ? null : Object.Instantiate(prefab);
        if (result == null)
            Debug.Log("Create null GO" + prefabPath);
        return result;
    }

    public GameObject InstantiateInteractive(GameObject prefab)
    {
        GameObject result = Object.Instantiate(prefab);
        InteractiveGameObjectCollection.Instance.AddInteractiveGo(result);
        return result;
    }
}
