using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameScript
{
	private const string DEFAULT_LOC = "E:/Leander/Master/Derpanizer/Derpanizer/Test";
	private const string INPUT_FIELD_NAME = "FilePathInput";
	private const string MAIN_SCENE = "MainScene";

	public string InitRootPath()
	{
		var inputField = GameObject.Find(INPUT_FIELD_NAME).GetComponent<InputField>();
		string text;
		if (inputField != null && !inputField.text.Equals(""))
		{
			text = inputField.text;
		}
		else
		{
			text = DEFAULT_LOC;
		}

		return text;
	}

	public void LoadNewScene()
	{
		SceneManager.LoadSceneAsync(MAIN_SCENE);
	}
}
