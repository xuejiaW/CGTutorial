using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockManager : MonobehaviorSingleton<CodeBlockManager>
{
    public GameObject codeSentence_prefab = null;
    public GameObject codeBlock_prefab = null;

    [System.NonSerialized]
    public List<InputField> snippetEditablePart = null;

    public RectTransform[] snippetHolderTrans = null;

    protected override void Init()
    {
        base.Init();

        snippetEditablePart = new List<InputField>();

        CreateCodeSnippet(CoursesAdaptor.Instance.currentCourse.mainCPPPath, snippetHolderTrans[0]);
        CreateCodeSnippet(CoursesAdaptor.Instance.currentCourse.vertexShaderPath, snippetHolderTrans[1]);
        CreateCodeSnippet(CoursesAdaptor.Instance.currentCourse.fragmentShaderPath, snippetHolderTrans[2]);
    }

    // Generate all code snippet and get all editable part
    private void CreateCodeSnippet(string sourcePath, RectTransform contentParent)
    {
        if (string.IsNullOrEmpty(sourcePath))
            return;

        TextAsset source = Resources.Load<TextAsset>(sourcePath);
        List<string> sourceContent = source.text.Split('\n').ToList();

        Stack<CodeBlockController> blockStack = new Stack<CodeBlockController>();
        CodeBlockController mainBlock = GameObject.Instantiate(codeBlock_prefab).GetComponent_AutoAdd<CodeBlockController>();
        mainBlock.transform.SetParent(contentParent, false);
        blockStack.Push(mainBlock);

        sourceContent.ForEach(sentence =>
        {
            if (sentence.Contains("[`"))
            {
                CodeBlockController codeBlock = GameObject.Instantiate(codeBlock_prefab).GetComponent_AutoAdd<CodeBlockController>();
                codeBlock.LoadTitle(sentence);
                blockStack.Peek().AddBlock(codeBlock);
                blockStack.Push(codeBlock);
            }
            else if (sentence.Contains("`]"))
            {
                CodeBlockController codeBlock = blockStack.Pop();
            }
            else
            {
                CodeSentenceController sentenceController = GameObject.Instantiate(codeSentence_prefab).GetComponent<CodeSentenceController>();
                sentenceController.LoadSentence(sentence);
                snippetEditablePart.AddRange(sentenceController.sentenceEditablePart);
                blockStack.Peek().AddSentence(sentenceController);
            }
        });
        mainBlock.UnFoldBlock();
        // contentParent.sizeDelta = new Vector2(Mathf.Max(contentParent.sizeDelta.x, maxSentenceWidth), totalSentenceHeight);
    }



    // Alloc editable part to adaptor, warning the order of setting adaptor is matter
    public void BindSnippetAdaptor(CodeSnippetInputAdaptor adaptor)
    {
        int neededCount = adaptor.dataCount;
        adaptor.BindSnippetEditableFields(snippetEditablePart.GetRange(0, neededCount));
        snippetEditablePart.RemoveRange(0, neededCount);
    }
}
