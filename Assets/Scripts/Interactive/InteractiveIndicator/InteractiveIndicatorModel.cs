using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractiveIndicatorModel : DisplayableEntityModel
{
    public override System.Type GetViewType()
    {
        return typeof(InteractiveIndicatorView);
    }
    public override System.Type GetControllerType()
    {
        return typeof(InteractiveIndicatorController);
    }
}
