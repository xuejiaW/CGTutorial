using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentModel : DisplayableEntityModel
{
    public override System.Type GetViewType()
    {
        return ComponentUIDict.id2ViewDict.TryGetValue(base.assetID, out var view) ? view : typeof(EntityView);
    }
    public override System.Type GetControllerType()
    {
        return ComponentUIDict.id2ControllerDict.TryGetValue(base.assetID, out var view) ? view : typeof(DisplayableEntityController);
    }

    private DisplayableEntityModel _targetGameObject;
    public DisplayableEntityModel targetGameObject
    {
        get { return _targetGameObject; }
        set { _targetGameObject = value; OnTargetGameObjectSet(_targetGameObject); }
    }

    protected virtual void OnTargetGameObjectSet(DisplayableEntityModel targetGO)
    {

    }
}
