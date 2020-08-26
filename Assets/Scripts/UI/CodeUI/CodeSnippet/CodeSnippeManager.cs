using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeSnippetManager : Singleton<CodeSnippetManager>
{
    public void CreateCodeSnippet(string snippetID, InteractiveGameObjectModel model)
    {
        string core = snippetID.Replace("code_snippet_", "");
        string[] parts = core.Split('_');
        string result = "";
        for (int i = 0; i != parts.Length; ++i)
            result += parts[i].Substring(0, 1).ToUpper() + parts[i].Substring(1);

        Type modelType = Type.GetType(result + "SnippetModel") ?? typeof(CodeSnippetModel);
        Type viewType = Type.GetType(result + "SnippetView") ?? typeof(CodeSnippetView);
        Type controllerType = Type.GetType(result + "SnippetController") ?? typeof(CodeSnippetController);

        CodeSnippetModel codeModel = (Activator.CreateInstance(modelType) as CodeSnippetModel);
        codeModel.assetID = snippetID;
        codeModel.targetGameObject = model;

        CodeSnippetController codeController = Activator.CreateInstance(controllerType) as CodeSnippetController;
        codeController.model = codeModel;

        GameResourceManager.Instance.LoadConfigData(codeModel);

        CodeSnippetView codeView = Activator.CreateInstance(viewType) as CodeSnippetView;
        codeView.model = codeModel;
        codeView.controller = codeController;
        codeView.snippetInputsList = CodeBlockManager.Instance.PopEditablePart(codeModel.tag, codeModel.dataCount);


        codeController.InitCodeSnippet();
        codeView.InitCodeSnippet();
    }
}
