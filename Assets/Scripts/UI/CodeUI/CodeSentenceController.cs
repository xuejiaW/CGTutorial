using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeSentenceController : MonoBehaviour
{
    // using `` may cause problem
    // private string testCode = "abc <`0`>,<`1`>,<`2`> def,<`456`>";
    // private string testCode = "    glVertexAttribPointer( <`0`> , 3, GL_FLOAT, GL_FALSE, 3 * sizeof(GL_FLOAT), (GLvoid *)0);";

    public GameObject nonEditablePart_prefab = null;
    public GameObject editablePart_prefab = null;

    [System.NonSerialized]
    public new Transform transform = null;

    [System.NonSerialized]
    public RectTransform rectTransform = null;

    [System.NonSerialized]
    public Vector2 size = Vector2.zero;

    [System.NonSerialized]
    public List<InputField> sentenceEditablePart = null;

    private void Awake()
    {
        this.transform = base.transform;
        rectTransform = GetComponent<RectTransform>();
    }

    public void LoadSentence(string sentence)
    {
        List<KeyValuePair<string, bool>> parserContent = GetSplittedCodeSnippet(sentence);
        sentenceEditablePart = new List<InputField>();

        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        float totalWidth = 0;
        float maxHeight = 0;
        parserContent.ForEach(str =>
        {
            Vector2 size = GetStringWidth(str.Key, str.Value ? 10 : 0);
            float width = size[0];
            float height = size[1];
            totalWidth += width;
            maxHeight = Mathf.Max(maxHeight, height);
            RectTransform codeSentence = str.Value ? LoadEditablePart(str.Key, width) : LoadNonEditablePart(str.Key, width);

            if (str.Value)
                sentenceEditablePart.Add(codeSentence.GetComponent<InputField>());

            codeSentence.SetParent(transform, false);
        });

        size = new Vector2(totalWidth, maxHeight);
    }

    private List<KeyValuePair<string, bool>> GetSplittedCodeSnippet(string text)
    {
        List<KeyValuePair<string, bool>> splittedCodeSentence = new List<KeyValuePair<string, bool>>();

        int searchIndex = 0;
        while (searchIndex < text.Length)
        {
            int prevIndex = text.IndexOf("<`", searchIndex);
            if (prevIndex == -1) break;
            int nextIndex = text.IndexOf("`>", prevIndex);

            splittedCodeSentence.Add(new KeyValuePair<string, bool>(text.Substring(searchIndex, prevIndex - searchIndex), false));
            splittedCodeSentence.Add(new KeyValuePair<string, bool>(text.Substring(prevIndex + 2, nextIndex - prevIndex - 2), true));
            searchIndex = nextIndex + 2;
        }
        splittedCodeSentence.Add(new KeyValuePair<string, bool>(text.Substring(searchIndex, text.Length - searchIndex), false));

        return splittedCodeSentence;
    }


    private RectTransform LoadEditablePart(string textValue, float width)
    {
        InputField editableInput = GameObject.Instantiate(editablePart_prefab).GetComponent<InputField>();
        Vector2 sizeData = new Vector2(width, 100); // because the text's alignment is middle, so the height doesn't matter
        RectTransform rectTrans = editableInput.GetComponent<RectTransform>();
        rectTrans.sizeDelta = sizeData;
        editableInput.textComponent.rectTransform.sizeDelta = sizeData;
        editableInput.placeholder.rectTransform.sizeDelta = sizeData;
        editableInput.text = textValue;
        return rectTrans;
    }

    private RectTransform LoadNonEditablePart(string textValue, float width)
    {
        Text nonEditableText = GameObject.Instantiate(nonEditablePart_prefab).GetComponent<Text>();
        nonEditableText.rectTransform.sizeDelta = new Vector2(width, 0);
        nonEditableText.text = textValue;
        return nonEditableText.rectTransform;

    }

    private Vector2 GetStringWidth(string text, int wordsPadding = 0, int heightPadding = 15)
    {
        Text nonEditableText = nonEditablePart_prefab.GetComponent<Text>();
        return GetStringWidth(text, nonEditableText.font, nonEditableText.fontSize, nonEditableText.fontStyle, wordsPadding, heightPadding);
    }

    private Vector2 GetStringWidth(string text, Font font, int fontSize, FontStyle style, int wordsPadding, int heightPadding)
    {
        font.RequestCharactersInTexture(text, fontSize, style);
        float totalWidth = 0;
        float maxHeight = 0;
        for (int i = 0; i != text.Length; ++i)
        {
            font.GetCharacterInfo(text[i], out CharacterInfo info, fontSize);
            totalWidth += info.advance;
            maxHeight = Mathf.Max(maxHeight, info.glyphHeight);
        }
        totalWidth += wordsPadding;
        return new Vector2(totalWidth + wordsPadding, maxHeight + heightPadding);
    }
}


// Version: splitted all part
// private List<KeyValuePair<string, bool>> GetSplittedCodeSentence(string text)
// {
//     List<KeyValuePair<string, bool>> splittedCodeSentence = new List<KeyValuePair<string, bool>>();

//     // save the indent
//     int firstCharIndex = text.IndexNotOf(' ');
//     splittedCodeSentence.Add(new KeyValuePair<string, bool>(text.Substring(0, firstCharIndex), false));
//     text = text.Substring(firstCharIndex);

//     string[] splitted = text.Split(' ');

//     for (int i = 0; i != splitted.Length; ++i)
//     {
//         if (splitted[i].IndexOf("<`") == -1)
//         {
//             splittedCodeSentence.Add(new KeyValuePair<string, bool>(splitted[i], false));
//         }
//         else
//         {
//             int prevfix = splitted[i].IndexOf("<`"), postfix = splitted[i].IndexOf("`>");

//             if (prevfix != 0)
//                 splittedCodeSentence.Add(new KeyValuePair<string, bool>(splitted[i].Substring(0, prevfix), false));
//             splittedCodeSentence.Add(new KeyValuePair<string, bool>(splitted[i].Substring(prevfix + 2, postfix - prevfix - 2), true));

//             if ((postfix + 2) != splitted[i].Length)
//                 splittedCodeSentence.Add(new KeyValuePair<string, bool>(splitted[i].Substring(postfix + 2, splitted[i].Length - postfix - 2), true));
//         }
//     }

//     return splittedCodeSentence;
// }
