using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionManager : Singleton<ReflectionManager>
{
    public Type GetAssetType(string assetID, string prefix, string suffix)
    {
        string core = assetID.Replace(prefix, "");
        string[] parts = core.Split('_');
        string result = "";
        for (int i = 0; i != parts.Length; ++i)
        {
            result += parts[i].Substring(0, 1).ToUpper() + parts[i].Substring(1);
        }

        return Type.GetType(result + suffix);
    }
}
