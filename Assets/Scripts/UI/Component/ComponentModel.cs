using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentModel : DisplayableEntityModel
{
    private static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) },
                    {"component_clear_color",typeof(ClearColorUIView)} };
    private static Dictionary<string, System.Type> id2ControllerDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIController) },
                    { "component_clear_color", typeof(ClearColorUIController) },
                    };

    public override System.Type GetViewType()
    {
        return id2ViewDict.TryGetValue(base.assetID, out var view) ? view : typeof(EntityView);
    }
    public override System.Type GetControllerType()
    {
        return id2ControllerDict.TryGetValue(base.assetID, out var view) ? view : typeof(DisplayableEntityController);
    }

    public DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }
}
