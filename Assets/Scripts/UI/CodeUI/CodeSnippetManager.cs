using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeSnippetManager : MonobehaviorSingleton<CodeSnippetManager>
{
    public GameObject codeSentence_prefab = null;

    private float maxSentenceWidth = -1;
    private float totalSentenceHeight = -1;
    public RectTransform[] snippetHolderTrans = null;
    public List<InputField> snippetEditablePart = null;

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

        sourceContent.ForEach(sentence =>
        {
            CodeSentenceController sentenceController = GameObject.Instantiate(codeSentence_prefab).GetComponent<CodeSentenceController>();
            sentenceController.LoadSentence(sentence);
            snippetEditablePart.AddRange(sentenceController.sentenceEditablePart);
            Vector2 sentenSize = sentenceController.size;
            maxSentenceWidth = Mathf.Max(maxSentenceWidth, sentenSize[0]);
            sentenceController.rectTransform.sizeDelta = new Vector2(sentenSize[0], sentenSize[1]);

            sentenceController.transform.SetParent(contentParent, false);
            totalSentenceHeight += sentenSize[1];
        });

        contentParent.sizeDelta = new Vector2(Mathf.Max(contentParent.sizeDelta.x, maxSentenceWidth), totalSentenceHeight);
    }

    // Alloc editable part to adaptor, warning the order of setting adaptor is matter
    public void BindSnippetAdaptor(CodeEditablePartAdaptor adaptor)
    {
        int neededCount = adaptor.dataCount;
        adaptor.BindSnippetEditableFields(snippetEditablePart.GetRange(0, neededCount));
        snippetEditablePart.RemoveRange(0, neededCount);
    }
}
