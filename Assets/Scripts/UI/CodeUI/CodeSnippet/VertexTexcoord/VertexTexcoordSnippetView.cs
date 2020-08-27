using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTexcoordSnippetView : CodeSnippetView
{
    private new VertexTexcoordSnippetController controller = null;
    private VertexModel targetGO = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();
        controller = base.controller as VertexTexcoordSnippetController;
        targetGO = model.targetGameObject as VertexModel;

        for (int i = 0; i != snippetInputsList.Count; ++i)
        {
            int channel = i;
            snippetInputsList[i].onEndEdit.AddListener((val) => controller.SetTargetGOTexcoord(channel, val));
            controller.SetTargetGOTexcoord(channel, snippetInputsList[i].text); // using code to init
        }

        targetGO.OnTexcoordUpdated += UpdateVertexTexcoord;
    }

    public override void Switch(bool on)
    {
        base.Switch(on);
        if (on)
            targetGO.OnTexcoordUpdated += UpdateVertexTexcoord;
        else
            targetGO.OnTexcoordUpdated -= UpdateVertexTexcoord;
    }

    private void UpdateVertexTexcoord(Vector2 texcoord)
    {
        for (int i = 0; i != snippetInputsList.Count; ++i)
        {
            snippetInputsList[i].text = texcoord[i].ToString("f2");
        }
    }

    ~VertexTexcoordSnippetView()
    {
        targetGO.OnTexcoordUpdated -= UpdateVertexTexcoord;
    }
}
