using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSnippetManager : MonoBehaviour
{
    public GameObject codeSentence_prefab = null;

    private float maxSentenceWidth = -1;
    private float totalSentenceHeight = -1;
    private new Transform transform = null;
    private RectTransform rectTransform = null;

    private void Awake()
    {
        this.transform = base.transform;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        CreateCodeSnippet(CoursesAdaptor.Instance.currentCourse.mainCPPPath);
    }

    private void CreateCodeSnippet(string sourcePath)
    {
        TextAsset source = Resources.Load<TextAsset>(sourcePath);
        List<string> sourceContent = source.text.Split('\n').ToList();

        sourceContent.ForEach(sentence =>
        {
            CodeSentenceController sentenceController = GameObject.Instantiate(codeSentence_prefab).GetComponent<CodeSentenceController>();
            Vector2 sentenSize = sentenceController.LoadSentence(sentence);
            maxSentenceWidth = Mathf.Max(maxSentenceWidth, sentenSize[0]);
            sentenceController.rectTransform.sizeDelta = new Vector2(sentenSize[0], sentenSize[1]);


            sentenceController.transform.SetParent(transform, false);
            totalSentenceHeight += sentenSize[1];
        });

        rectTransform.sizeDelta = new Vector2(Mathf.Max(rectTransform.sizeDelta.x, maxSentenceWidth), totalSentenceHeight);
    }
}
