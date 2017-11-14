using UnityEngine;
using UnityEngine.UI;

public class SetupScript : MonoBehaviour
{

    public void InitFileTree()
    {
        InputField inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
        if (inputField != null)
        {
            var fileTree = FileReader.ReadFileTree(inputField.text);
            //var fileTree = FileReader.ReadFileTree("D:/Test");
        }
    }
}
