using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _gameRunning = false;
    private Camera _camera;


    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
    }

    public void InitGame()
    {
        InputField inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
        if (inputField != null)
        {
            gameObject.AddComponent<FileManager>();
            gameObject.GetComponent<FileManager>().Init(inputField.text);
            gameObject.GetComponent<FileManager>().ReadFirstLayer();

            var menuItems = GameObject.FindGameObjectsWithTag("menu");
            foreach (var item in menuItems)
            {
                item.SetActive(false);
            }

        }
        //else
        //{
        // TODO: error handling
        //}

        // make camera movement possible
        _camera.GetComponent<CameraScript>().SetGameRunning(true);
    }
}