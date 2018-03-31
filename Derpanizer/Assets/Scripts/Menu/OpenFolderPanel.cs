using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class OpenFolderPanel : MonoBehaviour
	{
		public void SelectDirectory()
		{
			var inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
			if (inputField != null)
			{
				inputField.text = OpenFolderEditorWindow.Apply();
				gameObject.GetComponent<SelectOnInput>().selectedObject = GameObject.Find("StartButton");
			}
		}
	}
}