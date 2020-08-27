using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColorSnippetView : CodeSnippetView
{
    public new ClearColorSnippetController controller = null;
    public ClearColorModel targetModel = null;

    public IUpdateComponent<Color> componentUpdater = null;

    public override void InitCodeSnippet()
    {
        base.InitCodeSnippet();

        this.controller = base.controller as ClearColorSnippetController;
        targetModel = model.targetGameObject as ClearColorModel;

        for (int i = 0; i != snippetInputsList.Count; ++i)
        {
            int channel = i;
            snippetInputsList[i].onEndEdit.AddListener((val) => this.controller.UpdateClearColor(channel, val));
            this.controller.UpdateClearColor(channel, snippetInputsList[i].text); // using the code to init
        }

        componentUpdater = new ColorComponentUpdater();
        componentUpdater.SetTargetInputFields(snippetInputsList);
        targetModel.OnClearColorChanged += (color => componentUpdater.UpdateComponent(color));
    }
}
