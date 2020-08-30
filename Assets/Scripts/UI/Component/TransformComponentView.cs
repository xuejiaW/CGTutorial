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
        // unregister event in old target and register event in new target
        viewUpdater.UnRegisterEvent();
        viewUpdater.SetTargetModel(targetGameObject).RegisterEvent();
    }
}
