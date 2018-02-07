using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace FileManagerScripts
{
    public class FileManager : MonoBehaviour
    {
        private FileReader _reader;
        private Vector3 _defaultLocation;
        private List<DirectoryInfo> _directories;
        private string _rootPath;

        public void Init(string path)
        {
            _rootPath = path;
            _reader = new FileReader(path);
            SetDefaultLocation();
            _directories = _reader.GetAllDirectories();
            InitContainers();
        }

        private void SetDefaultLocation()
        {
            var table = GameObject.Find(Const.Const.DEFAULT_TABLE);
            _defaultLocation = table.transform.position;
            _defaultLocation.y += table.GetComponent<Renderer>().bounds.size.y + 0.01f;
        }

        private void InitContainers()
        {
            GameObject[] containers = GameObject.FindGameObjectsWithTag(Const.Const.CONTAINER);
            foreach (var container in containers)
            {
                DirectoryInfo dir = DirectoryExists(container.name);
                if (dir != null)
                {
                    container.GetComponent<MoveScript>().Init(dir);
                }
                else
                {
                    //find right place in tree and create folder
                    string parentFolder;

                    if ((parentFolder = container.transform.parent.name).Equals(_rootPath))
                    {
                        parentFolder = container.transform.name;
                    }
                    DirectoryInfo parentDir = DirectoryExists(parentFolder);
                    if (parentDir == null)
                    {
                        DirectoryInfo rootdir = _reader.GetRootDirectory();
                        rootdir.CreateSubdirectory(rootdir.FullName + Const.Const.BACKSLASH + parentFolder);
                    }
                    dir = parentDir.CreateSubdirectory(parentDir.FullName + Const.Const.BACKSLASH + container.name);
                    container.GetComponent<MoveScript>().Init(dir);
                }
                var filesinDirectory = dir.GetFiles();
                if (filesinDirectory.Length > 0)
                {
                    InstantiateFiles(filesinDirectory, container);
                }
            }
        }

        private DirectoryInfo DirectoryExists(string containerName)
        {
            foreach (var directory in _directories)
            {
                if (directory.Name.Equals(containerName))
                {
                    return directory;
                }
            }
            return null;
        }

        private void InstantiateFiles(IEnumerable<FileInfo> infos, GameObject container)
        {
            Vector3 location;
            if (container != null)
            {
                location = container.transform.position;
            }
            else
            {
                location = _defaultLocation;
            }
            float xAxis = 0;
            float zAxis = 0;
            foreach (var file in infos)
            {
                if (xAxis < 5)
                {
                    xAxis += 1;
                }
                else
                {
                    zAxis += 1;
                    xAxis = 0;
                }
                InstantiateFile(file, location, xAxis, zAxis);
                Thread.Sleep(100);
            }
        }

        private void InstantiateFile(FileInfo fileInfo, Vector3 location, float xAxis, float zAxis)
        {
            //location.x += xAxis;
            //location.z += zAxis;
            GameObject obj;
            switch (fileInfo.Extension)
            {
                case ".txt":
                case ".doc":
                case "pdf":
                case "docx":
                case "rtf":
                    obj = Instantiate(
                        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Textdocument.prefab", typeof(GameObject)), location, new Quaternion());
                    break;
                default:
                    obj = Instantiate(
                        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/File.prefab", typeof(GameObject)), location, new Quaternion());
                    break;

            }
            obj.AddComponent<FileObject>();
            obj.GetComponent<FileObject>().Init(fileInfo);
          
            // Set HoverText
            GameObject hover = Instantiate(
                (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HoverT.prefab", typeof(GameObject)),
                obj.transform.position, new Quaternion());
            hover.GetComponent<HoverText>().SetTarget(obj.transform);
            hover.GetComponent<HoverText>().GetComponent<GUIText>().text = fileInfo.Name.Split(Const.Const.BACKSLASH.ToCharArray()).Last();
        }

        private GameObject GetContainer(string dirName)
        {
            GameObject[] containers = GameObject.FindGameObjectsWithTag(Const.Const.CONTAINER);
            foreach (var container in containers)
            {
                if (container.name.Equals(dirName))
                {
                    return container;
                }
            }
            return null;
        }
    }
}