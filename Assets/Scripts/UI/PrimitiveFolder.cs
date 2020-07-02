using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveFolder : MonoBehaviour
{
    public void OnClickCreatePrimitive(string assetID)
    {
        InteractiveGameObjectView view = GameResourceManager.Instance.
                                        CreateEntityView<InteractiveGameObjectModel>(assetID) as InteractiveGameObjectView;
    }
}
