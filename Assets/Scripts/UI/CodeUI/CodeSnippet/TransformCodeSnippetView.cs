using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCodeSnippetView : CodeSnippetView
{
    protected DisplayableEntityModel targetGO
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitCodeSnippet()
    {
        viewUpdater?.SetTargetView(this);
        viewUpdater?.SetTargetModel(targetGO);
        viewUpdater?.RegisterEvent();

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i;
            inputFields[i].onEndEdit.AddListener((val) => this.controller.UpdateModelProperty(channel, val));

            // using the code to init
            controller.modelUpdater.SetTargetModel(model.targetGameObject);
            this.controller.UpdateModelProperty(channel, inputFields[i].text);

            controller.modelUpdater.SetTargetModel(targetGO);
        }

        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    public override void Switch(bool on)
    {
        base.Switch(on);

        if (on)
            viewUpdater.SetTargetModel(targetGO).RegisterEvent();
        else
            viewUpdater.SetTargetModel(targetGO).UnRegisterEvent();
    }

    protected virtual void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // unregister event in old target and register event in new target
        viewUpdater.SetTargetModel(oldIndicator.model).UnRegisterEvent();
        viewUpdater.SetTargetModel(newIndicator.model).RegisterEvent();
    }
}
