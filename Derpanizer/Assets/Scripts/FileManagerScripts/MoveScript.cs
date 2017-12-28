using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private DirectoryInfo[] _infos;

    public void Init(DirectoryInfo[] info)
    {
        _infos = info;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("file"))
        {
            var fileInfo = collision.gameObject.GetComponent<FileObject>();
            if (fileInfo != null)
            {
                var folder = GetFolderInDirectories(gameObject.name);
                if (!folder.Equals("null"))
                {
                    FileUtil.MoveFileOrDirectory(fileInfo.Info.DirectoryName, folder);
                }
                else
                {
                    var root = gameObject.GetComponentInParent<FileReader>().GetRootDirectory();
                    AssetDatabase.CreateFolder(root.ToString(), fileInfo.name);
                }
            }
        }
    }

    private string GetFolderInDirectories(string dName)
    {
        foreach (var directory in _infos)
        {
            if (directory.Name.Equals(dName))
            {
                return directory.ToString();
            }
        }
        return "null";
    }
}
