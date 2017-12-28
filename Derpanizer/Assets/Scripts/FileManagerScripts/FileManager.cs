using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class FileManager : MonoBehaviour
{

    private FileReader _reader;
    private Vector3 _defaultLocation;

    public void Init(string path)
    {
        _reader = new FileReader(path);
        var table = GameObject.Find("table");
        _defaultLocation = table.transform.position;
        _defaultLocation.y += table.GetComponent<Renderer>().bounds.size.y + 5;


    }

    public DirectoryInfo[] ReadFirstLayer()
    {
        var fileInfo = _reader.GetRootFileInfo();
        InstantiateFiles(fileInfo);
        var directoryInfo = _reader.GetAllDirectories();
        foreach (var info in directoryInfo)
        {
            var files = info.GetFiles();
            InstantiateFiles(files);
        }
        return directoryInfo;
    }

    private void InstantiateFiles(IEnumerable<FileInfo> infos)
    {
        float xAxis = 0;
        float zAxis = 0;
        foreach (var file in infos)
        {
            if (xAxis < 25)
            {
                xAxis += 5;
            }
            else
            {
                zAxis += 5;
                xAxis = 0;
            }
            InstantiateFile(file, xAxis, zAxis);
            Thread.Sleep(100);
        }
    }

    private void InstantiateFile(FileInfo fileInfo, float xAxis, float zAxis)
    {
        var location = _defaultLocation;
        location.x += xAxis;
        location.z += zAxis;
        switch (fileInfo.Extension)
        {
            //case ".txt":
            default:
                var obj = Instantiate(
                    (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/File.prefab", typeof(GameObject)), _defaultLocation, new Quaternion());
                obj.AddComponent<FileObject>();
                obj.GetComponent<FileObject>().Init(fileInfo);
                break;
        }
    }

    public void CopyFile(FileObject fileToCopy)
    {
        // Note: Not sure where to put the copied file, maybe put a printer on the desk?
        Instantiate(fileToCopy, _defaultLocation, new Quaternion());
    }

    //public void MoveFile(FileInfo fileToMove, FileObject fileO, FileObject destination)
    //{
    //    fileO.Info.MoveTo(destination.Info.DirectoryName);
    //}
}