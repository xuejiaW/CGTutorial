using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveFolder : MonoBehaviour
{
    public void OnClickCreatePrimitive(string assetID)
    {
        GameResourceManager.Instance.CreateEntityController<InteractiveGameObjectModel>(assetID);
    }
}
