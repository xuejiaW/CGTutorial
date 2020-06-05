using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    private int targetLayerMask = 0;
    
    public void SetTargetLayer(int layer)
    {
        targetLayerMask |= 1 << layer;
    }


    public void Update()
    {

    }
}
