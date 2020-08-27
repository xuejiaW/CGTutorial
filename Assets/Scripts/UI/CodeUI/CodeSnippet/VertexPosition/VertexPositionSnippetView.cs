using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionSnippetView : CodeSnippetView
{
    public new VertexPositionSnippetController controller = null;
    public VertexModel targetModel = null;

    private DisplayableEntityModel targetGameObject
    {
        get { return InteractiveIndicatorCollection.Instance.currentIndicator?.model; }
    }

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();

        controller = base.controller as VertexPositionSnippetController;

        for (int i = 0; i != snippetInputsList.Count; ++i)
        {
            int channel = i;
            snippetInputsList[i].onEndEdit.AddListener((val) => controller.SetTargetGOPosition(channel, val));
            controller.SetModelPosition(channel, snippetInputsList[i].text); // using code to init
        }

        targetGameObject.OnPositionUpdated += UpdateVertexPosUI;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    public override void Switch(bool on)
    {
        base.Switch(on);
        onModelActiveUpdated(on);
    }

    private void onModelActiveUpdated(bool active)
    {

        if (active)
        {
            targetGameObject.OnPositionUpdated += UpdateVertexPosUI;
            UpdateVertexPosUI(targetGameObject.localPosition);
        }
        else
        {
            targetGameObject.OnPositionUpdated -= UpdateVertexPosUI;
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
        for (int i = 0; i != snippetInputsList.Count; ++i)
            snippetInputsList[i].text = (pos[i]).ToString("f2");
    }

    ~VertexPositionSnippetView()
    {
        targetGameObject.OnPositionUpdated += UpdateVertexPosUI;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }
}
