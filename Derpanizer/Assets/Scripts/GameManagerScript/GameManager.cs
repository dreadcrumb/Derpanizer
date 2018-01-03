using System.IO;
using FileManagerScripts;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    private Camera _camera;


    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
        InitGame();
    }

    public void InitGame()
    {
        //InputField inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
        //if (inputField != null)
        //{
        string text = "D:/Leander/FH_Hagenberg_IM/MA_3D/Derpanizer/Test";
            gameObject.AddComponent<FileManager>();
            gameObject.GetComponent<FileManager>().Init(/*inputField.*/text);
            var directories = gameObject.GetComponent<FileManager>().ReadFirstLayer();
            gameObject.AddComponent<CopyScript>();
            gameObject.AddComponent<MoveScript>();
            gameObject.GetComponent<MoveScript>().Init(directories);

            var menuItems = GameObject.FindGameObjectsWithTag("menu");
            foreach (var item in menuItems)
            {
                item.SetActive(false);
            }
            // make camera movement possible
            _camera.GetComponent<FirstPersonController>().StartGame();
        //}
        //else
        //{
        // TODO: error handling
        //}


    }
}