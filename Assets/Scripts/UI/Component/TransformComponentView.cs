using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformComponentView : ComponentView
{
    protected DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitComponent()
    {
        base.InitComponent();
        // Should only register the indicator's transform update, thus unregister of model.targetGameobject
        viewUpdater?.UnRegisterEvent(); // 
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    protected override void onModelActiveUpdated(bool active)
    {
        if (active)
            viewUpdater.SetTargetModel(targetGameObject).RegisterEvent();
        else
            viewUpdater.SetTargetModel(targetGameObject).UnRegisterEvent();
    }

    protected virtual void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        Debug.Log("on indicator updated");
        // unregister event in old target and register event in new target
        viewUpdater.SetTargetModel(oldIndicator.model).UnRegisterEvent();
        viewUpdater.SetTargetModel(newIndicator.model).RegisterEvent();
    }

    ~TransformComponentView()
    {
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}
