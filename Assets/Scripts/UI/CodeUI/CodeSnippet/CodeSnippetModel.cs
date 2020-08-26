using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CodeSnippetModel : EntityModel
{
    public DisplayableEntityModel targetGameObject { get; set; }
    public int dataCount = 0;
    public string tag = null;
}
