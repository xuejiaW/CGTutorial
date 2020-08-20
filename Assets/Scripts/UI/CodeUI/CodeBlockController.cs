using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockController : MonoBehaviour
{
    private CodeBlockController parentBlock = null;
    private List<CodeSentenceController> codeSentences = null;
    private List<CodeBlockController> codeBlocks = null;
    private RectTransform container = null;

    private bool folding = true;
    private float sentenceMaxWidth = 0.0f;
    private float sentenceHeight = 0.0f;
    private float basicWidth = 0.0f;
    private float basicHeight = 0.0f;
    public float height { get { return folding ? basicHeight : sentenceHeight + basicHeight; } }
    public float width { get { return folding ? basicWidth : Mathf.Max(sentenceMaxWidth, basicWidth); } }

    private RectTransform rectTransform = null;
    private Text titleText = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        container = transform.Find("Container").GetComponent<RectTransform>();
        titleText = transform.Find("Button/Text").GetComponent<Text>();

        codeSentences = new List<CodeSentenceController>();
        codeBlocks = new List<CodeBlockController>();

        basicWidth = rectTransform.sizeDelta.x;
        basicHeight = rectTransform.sizeDelta.y;
    }

    public void AddSentence(CodeSentenceController sentence)
    {
        codeSentences.Add(sentence);

        sentence.transform.SetParent(container, false);
        sentence.gameObject.SetActive(false);

        sentenceMaxWidth = Mathf.Max(sentenceMaxWidth, sentence.width);
        sentenceHeight += sentence.height;
        Debug.Log("sentence Height is " + sentenceHeight);
    }

    public void AddBlock(CodeBlockController block)
    {
        codeBlocks.Add(block);
        block.transform.SetParent(container, false);
        block.gameObject.SetActive(false);
        block.parentBlock = this;
    }


    public void LoadTitle(string sentence)
    {
        int index = sentence.IndexOf("[`");
        string title = sentence.Substring(index + 2);
        titleText.text = title;
        Debug.Log("title is " + title);
    }

    // Called by button
    public void SwitchFoldState()
    {
        if (folding)
            UnFoldBlock();
        else
            FoldBlock();
    }

    public void FoldBlock()
    {
        folding = true;
        codeBlocks.ForEach(block => block.gameObject.SetActive(false));
        codeSentences.ForEach(sentence => sentence.gameObject.SetActive(false));
        UpdateContainer();
    }

    public void UnFoldBlock()
    {
        folding = false;
        codeBlocks.ForEach(block => block.gameObject.SetActive(true));
        codeSentences.ForEach(sentence => sentence.gameObject.SetActive(true));
        UpdateContainer();
    }

    private void UpdateContainer()
    {
        float unfoldHeight = this.height;
        float unfoldWidth = this.width;
        codeBlocks.ForEach(block =>
        {
            unfoldHeight += block.height;
            unfoldWidth = Mathf.Max(unfoldWidth, block.width);
        });

        rectTransform.sizeDelta = new Vector2(unfoldWidth, unfoldHeight);
        container.sizeDelta = new Vector2(unfoldWidth, folding ? 0 : unfoldHeight - basicHeight); // Minus current height

        if (parentBlock != null)
            parentBlock.UpdateContainer();
        else
            rectTransform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(unfoldWidth, unfoldHeight);
    }

}
