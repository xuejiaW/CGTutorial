using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionSnippetView : CodeSnippetView
{
    public new VertexPositionSnippetController controller = null;

    private DisplayableEntityModel targetGO
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

        targetGO.OnPositionUpdated += UpdateVertexPosUI;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }

    public override void Switch(bool on)
    {
        base.Switch(on);

        if (on)
        {
            targetGO.OnPositionUpdated += UpdateVertexPosUI;
            UpdateVertexPosUI(targetGO.localPosition);
        }
        else
        {
            targetGO.OnPositionUpdated -= UpdateVertexPosUI;
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
        targetGO.OnPositionUpdated += UpdateVertexPosUI;
        InteractiveIndicatorCollection.Instance.OnIndicatorChanged += OnIndicatorChanged;
    }
}
