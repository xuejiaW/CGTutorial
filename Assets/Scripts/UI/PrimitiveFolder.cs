using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveFolder : MonoBehaviour
{
    public void OnClickCreatePrimitive(GameObject prefab)
    {
        GameResourceManager.Instance.InstantiateInteractive(prefab);
    }
}
