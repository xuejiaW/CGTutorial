using System.Collections.Generic;
using System;
using UnityEngine;

public partial class KeyboardInputManager : Singleton<KeyboardInputManager>, IMainUpdateObserver, IInputManager
{
    private Dictionary<KeyCode, Action<KeyCode>> keyDownHandlesDict = null;
    private Dictionary<KeyCode, Action<float>> keyHandlesDict = null;
    private Dictionary<KeyCode, Action<KeyCode>> keyUpHandlesDict = null;

    public override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        keyDownHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();
        keyHandlesDict = new Dictionary<KeyCode, Action<float>>();
        keyUpHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();

        trackedKeyDownList = new Dictionary<KeyCode, int>();
        trackedKeyList = new Dictionary<KeyCode, int>();
        trackedKeyUpList = new Dictionary<KeyCode, int>();
    }

    public void RegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyDownHandlesDict, trackedKeyDownList, handle, key);
    }

    public void RegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyHandlesDict, trackedKeyList, handle, key);
    }

    public void RegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyUpHandlesDict, trackedKeyUpList, handle, key);
    }

    public void UnRegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyDownHandlesDict, trackedKeyDownList, handle, key);
    }

    public void UnRegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyHandlesDict, trackedKeyList, handle, key);
    }

    public void UnRegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyUpHandlesDict, trackedKeyUpList, handle, key);
    }
}