using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;

public class CodeWindowManager : MonobehaviorSingleton<CodeWindowManager>
{
    private List<CodeWindowController> windowControllers = null;
    public int currentWindowIndex = 0;
    [System.NonSerialized]
    public bool duringAni = false;

    protected override void Start()
    {
        base.Start();
        windowControllers = new List<CodeWindowController>(transform.GetComponentsInChildren<CodeWindowController>());

        float windowWidth = GetComponent<RectTransform>().rect.width;
        for (int i = 0; i != windowControllers.Count; ++i)
        {
            windowControllers[i].manager = this;
            windowControllers[i].windowIndex = i;
            windowControllers[i].gameObject.SetActive(i == 0);
            windowControllers[i].windowWidth = windowWidth;

            Transform windowTrans = windowControllers[i].transform;
            Vector3 localPos = windowTrans.localPosition;
            windowTrans.localPosition = localPos.SetX(i * windowWidth);
        }
    }

    public void OnClickWindowTab(int index)
    {
        if (currentWindowIndex == index || duringAni)
            return;

        currentWindowIndex = index;
        windowControllers.ForEach(window => window.Select(index));
    }
}
