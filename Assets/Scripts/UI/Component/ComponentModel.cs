using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentModel : DisplayableEntityModel
{
    private static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) } };

    public override System.Type GetViewType()
    {
        return id2ViewDict.TryGetValue(base.assetID, out var view) ? view : typeof(EntityView);
    }
}
