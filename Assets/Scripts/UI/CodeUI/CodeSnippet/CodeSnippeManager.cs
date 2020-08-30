using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CodeSnippetManager : Singleton<CodeSnippetManager>
{
    private List<CodeSnippetView> snippetViews = null;

    public override void Init()
    {
        base.Init();
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;
    }

    protected override void InitProcess()
    {
        base.InitProcess();
        snippetViews = new List<CodeSnippetView>();
    }

    public void CreateCodeSnippet(string snippetID, InteractiveGameObjectModel model)
    {
        Type modelType = ReflectionManager.Instance.GetAssetType(snippetID, "code_snippet_", "CodeSnippetModel") ?? typeof(CodeSnippetModel);
        Type viewType = ReflectionManager.Instance.GetAssetType(snippetID, "code_snippet_", "CodeSnippetView") ?? typeof(CodeSnippetView);
        Type controllerType = ReflectionManager.Instance.GetAssetType(snippetID, "code_snippet_", "CodeSnippetController") ?? typeof(CodeSnippetController);


        CodeSnippetModel codeModel = (Activator.CreateInstance(modelType) as CodeSnippetModel);
        codeModel.assetID = snippetID;
        codeModel.targetGameObject = model;

        CodeSnippetController codeController = Activator.CreateInstance(controllerType) as CodeSnippetController;
        codeController.model = codeModel;

        GameResourceManager.Instance.LoadConfigData(codeModel);

        CodeSnippetView codeView = Activator.CreateInstance(viewType) as CodeSnippetView;
        codeView.model = codeModel;
        codeView.controller = codeController;
        codeView.inputFields = CodeBlockManager.Instance.PopEditablePart(codeModel.tag, codeModel.dataCount);

        Debug.Log("Pop count is " + codeModel.dataCount);

        codeController.InitCodeSnippet();
        codeView.InitCodeSnippet();

        snippetViews.Add(codeView);
    }

    private void OnSelectedGoUpdated(DisplayableEntityModel oldGbj, DisplayableEntityModel newGbj)
    {
        snippetViews.ForEach(view => view.Switch(newGbj != null && newGbj == view.model.targetGameObject));
    }
}
