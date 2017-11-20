using Assets.Scripts.Classes;
using Assets.Scripts.ObjectManager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private FileManager _manager;

        // Use this for initialization
        void Start()
        {

        }

        public void InitGame()
        {
            InputField inputField = GameObject.Find("FilePathInput").GetComponent<InputField>();
            if (inputField != null)
            {
                _manager = new FileManager(inputField.text);
                _manager.Init();

                //SceneManager.LoadScene("MainScene");
                var menuItems = GameObject.FindGameObjectsWithTag("menu");
                foreach (var item in menuItems)
                {
                    item.SetActive(false);
                }

            }
            //else
            //{
            // TODO: error message
            //}


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
