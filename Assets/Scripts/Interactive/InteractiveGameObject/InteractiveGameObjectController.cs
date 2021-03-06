﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectController : DisplayableEntityController
{
    public new InteractiveGameObjectModel model;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = model as InteractiveGameObjectModel;
    }

    public override void Init()
    {
        base.Init();
        InteractiveGameObjectCollection.Instance.AddInteractiveGo(model);

        model.componentsAssetID?.ForEach(component => ComponentManager.Instance.CreateComponent(component, model));
        model.codeSnippetsAssetID?.ForEach(snippetID => CodeSnippetManager.Instance.CreateCodeSnippet(snippetID, model));
    }
}
