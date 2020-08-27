using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIView : ComponentView
{
    private new VertexPosUIModel model = null;
    public new VertexPosUIController controller = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as VertexPosUIModel;
    }


    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as VertexPosUIController;

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i; // to fix the c# closure problem
            this.inputFields[i].onEndEdit.AddListener((val) => this.controller.SetPosition(channel, val));
        }
    }

    public override void InitComponent()
    {
        base.InitComponent();

        targetGameObject.OnPositionUpdated += UpdateVertexPosUI;
        model.OnActiveUpdated += onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    private void onModelActiveUpdated(bool active)
    {

        if (active)
        {
            targetGameObject.OnLocalPositionUpdated += UpdateVertexPosUI;
            UpdateVertexPosUI(targetGameObject.localPosition);
        }
        else
        {
            targetGameObject.OnLocalPositionUpdated -= UpdateVertexPosUI;

            // Refresh the UI
            UpdateVertexPosUI(model.targetGameObject.localPosition);
        }
    }

    private void OnIndicatorChanged(InteractiveIndicatorController oldIndicator, InteractiveIndicatorController newIndicator)
    {
        // modify the callback when indicator changed
        if (oldIndicator != null)
            oldIndicator.model.OnLocalPositionUpdated -= UpdateVertexPosUI;
        if (newIndicator != null)
            newIndicator.model.OnLocalPositionUpdated += UpdateVertexPosUI;
    }

    private void UpdateVertexPosUI(Vector3 pos)
    {
        for (int i = 0; i != 3; ++i)
            inputFields[i].text = (pos[i]).ToString("f2");
    }

    ~VertexPosUIView()
    {
        targetGameObject.OnPositionUpdated -= UpdateVertexPosUI;
        model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}
