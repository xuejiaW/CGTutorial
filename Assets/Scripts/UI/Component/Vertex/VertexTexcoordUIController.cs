using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexTexcoordUIController : ComponentController
{
    private new VertexTexcoordUIModel model = null;
    private CodeSnippetInputAdaptor adaptor = null;
    private VertexModel targetGO = null;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = base.model as VertexTexcoordUIModel;
    }

    public override void Init()
    {
        base.Init();

        adaptor = new CodeSnippetInputAdaptor();
        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int axis = i;
            adaptor.BindValueChangedEvent((val => SetTexcoord(axis, val)));
        }
        CodeSnippetManager.Instance.BindSnippetAdaptor(adaptor);

        for (int i = 0; i != model.inputFields.Length; ++i)
        {
            int index = i, axis = i % 3;
            InputField inputField = model.inputFields[i];

            inputField.onEndEdit.AddListener((val) => SetTexcoord(axis, val));
            inputField.onEndEdit.AddListener(val => adaptor.editableParts[index].text = val);
        }

        // make irrelevant part disinteractable
        model.OnActiveUpdated += (active => adaptor.editableParts.ForEach(inputField => inputField.interactable = active));
    }

    public override void InitComponent()
    {
        base.InitComponent();

        targetGO = model.targetGameObject as VertexModel;
        float.TryParse(adaptor.editableParts[0].text, out float x);
        float.TryParse(adaptor.editableParts[1].text, out float y);
        targetGO.texcoord = new Vector2(x, y);

        model.inputFields[0].text = targetGO.texcoord[0].ToString("f2");
        model.inputFields[1].text = targetGO.texcoord[1].ToString("f2");
    }

    private void SetTexcoord(int axis, string value)
    {
        if (!model.active) return;
        float.TryParse(value, out float val);
        Vector2 texcoord = targetGO.texcoord;
        texcoord[axis] = val;
        targetGO.texcoord = texcoord;
    }
}
