﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractiveGameObjectModel : DisplayableEntityModel
{
    public override System.Type GetViewType()
    {
        return typeof(InteractiveGameObjectView);
    }
    public override System.Type GetControllerType()
    {
        return typeof(InteractiveGameObjectController);
    }

}