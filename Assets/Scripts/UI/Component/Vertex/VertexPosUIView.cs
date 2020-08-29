using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexPosUIView : ComponentView
{
    private new VertexPosUIModel model = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public IUpdateComponent<Vector3> componentUpdater = null;

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as VertexPosUIModel;
    }

    public override void InitComponent()
    {
        base.InitComponent();
        componentUpdater = new LocalPositionComponentUpdater();
        componentUpdater.SetTargetInputFields(inputFields);

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
        componentUpdater.UpdateComponent(pos);
    }

    ~VertexPosUIView()
    {
        targetGameObject.OnPositionUpdated -= UpdateVertexPosUI;
        model.OnActiveUpdated -= onModelActiveUpdated;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged -= OnIndicatorChanged;
    }
}
