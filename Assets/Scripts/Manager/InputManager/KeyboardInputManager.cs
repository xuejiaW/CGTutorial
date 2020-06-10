using System.Collections.Generic;
using System;
using UnityEngine;

public partial class KeyboardInputManager : Singleton<KeyboardInputManager>, IMainUpdateObserver, IInputManager
{
    private Dictionary<KeyCode, Action<KeyCode>> keyDownHandlesDict = null;
    private Dictionary<KeyCode, Action<float>> keyHandlesDict = null;
    private Dictionary<KeyCode, Action<KeyCode>> keyUpHandlesDict = null;

    private List<KeyCode> trackedKeyList = null;

    public override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        keyDownHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();
        keyHandlesDict = new Dictionary<KeyCode, Action<float>>();
        keyUpHandlesDict = new Dictionary<KeyCode, Action<KeyCode>>();
        trackedKeyList = new List<KeyCode>();
    }

    public void RegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyDownHandlesDict, trackedKeyList, handle, key);
    }

    public void RegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyHandlesDict, trackedKeyList, handle, key);
    }

    public void RegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.RegisterMessageHandle(keyUpHandlesDict, trackedKeyList, handle, key);
    }

    public void UnRegisterKeyDownMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyDownHandlesDict, trackedKeyList, handle, key);
    }

    public void UnRegisterKeyMessageHandle(Action<float> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyHandlesDict, trackedKeyList, handle, key);
    }

    public void UnRegisterKeyUpMessageHandle(Action<KeyCode> handle, params KeyCode[] key)
    {
        this.UnRegisterMessageHandle(keyUpHandlesDict, trackedKeyList, handle, key);
    }
}