﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexTexcoordUIView : ComponentView
{
    public new VertexTexcoordUIController controller = null;
    public VertexModel targetGO = null;

    public override void BindEntityController(DisplayableEntityController controller)
    {
        base.BindEntityController(controller);
        this.controller = base.controller as VertexTexcoordUIController;

        for (int i = 0; i != inputFields.Count; ++i)
        {
            int channel = i;
            this.inputFields[i].onEndEdit.AddListener((val) => this.controller.SetTexcoord(channel, val));
        }

    }

    public override void InitComponent()
    {
        base.InitComponent();
        targetGO = model.targetGameObject as VertexModel;

        targetGO.OnTexcoordUpdated += UpdateVertexTexcoord;
    }


    private void UpdateVertexTexcoord(Vector2 texcoord)
    {
        for (int i = 0; i != inputFields.Count; ++i)
        {
            inputFields[i].text = texcoord[i].ToString("f2");
        }
    }

    ~VertexTexcoordUIView()
    {
        targetGO.OnTexcoordUpdated -= UpdateVertexTexcoord;
    }

}
