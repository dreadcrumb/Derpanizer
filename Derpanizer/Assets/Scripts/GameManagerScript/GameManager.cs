using System.IO;
using FileManagerScripts;
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
            var directories = gameObject.GetComponent<FileManager>().ReadFirstLayer();
            gameObject.AddComponent<CopyScript>();
            gameObject.AddComponent<MoveScript>();
            gameObject.GetComponent<MoveScript>().Init(directories);

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