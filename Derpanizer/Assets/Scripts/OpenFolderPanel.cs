using UnityEngine;
using UnityEngine.UI;

public class OpenFolderPanel : MonoBehaviour
{

    public void SelectDirectory()
    {
        InputField inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
        //Text text = InputField.GetComponent<Text>();
        if (inputField != null)
        {
            inputField.text = OpenFolderEditorWindow.Apply();
        }
    }
}