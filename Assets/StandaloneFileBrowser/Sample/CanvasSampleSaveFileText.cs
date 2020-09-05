using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler
{
    private string[] fileNames = new string[] { "Main", "Vertex", "Fragment" };
    private string[] fileExtensions = new string[] { "cpp", "vert", "frag" };

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        int index=CodeWindowManager.Instance.currentWindowIndex;
        string code = CodeBlockManager.Instance.GetWholeCode(index);
        Debug.Log("code is " + code);
        // GameResourceManager.Instance.DownloadString(code,fileNames[index],fileExtensions[index]);
        byte[] bytes = Encoding.UTF8.GetBytes(code);
        DownloadFile(gameObject.name, "OnFileDownload", fileNames[index]+"."+fileExtensions[index], bytes, bytes.Length);
    }
#else

    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        int index = CodeWindowManager.Instance.currentWindowIndex;
        string code = CodeBlockManager.Instance.GetWholeCode(index);
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", fileNames[index], fileExtensions[index]);
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, code);
        }
    }
#endif
}