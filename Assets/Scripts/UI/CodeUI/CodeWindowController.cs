using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CodeWindowController : MonoBehaviour
{
    public CodeWindowManager manager = null;
    private float aniDuration = 0.5f;

    [System.NonSerialized]
    public int windowIndex = -1;
    [System.NonSerialized]
    public float windowWidth = 0.0f;

    private new Transform transform = null;
    private new GameObject gameObject = null;

    private void Awake()
    {
        this.transform = base.transform;
        this.gameObject = base.gameObject;
    }

    public void Select(int index)
    {
        if (index == windowIndex)
        {
            gameObject.SetActive(true);
            manager.duringAni = true;
            transform.SetPositionX(transform.localPosition.x < 0 ? -windowWidth : windowWidth);
        }

        float targetX = index == windowIndex ? 0 : (windowIndex < index ? -windowWidth : windowWidth);
        transform.DOLocalMoveX(targetX, aniDuration).OnComplete(() =>
        {
            manager.duringAni = false;
            gameObject.SetActive(windowIndex == manager.currentWindowIndex);
        });
    }
}
