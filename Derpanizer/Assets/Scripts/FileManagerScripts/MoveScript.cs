using System.IO;
using FileManagerScripts;
using UnityEditor;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public GameObject Parent;

    private DirectoryInfo _infos;

    public void Init(DirectoryInfo info)
    {
        _infos = info;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("container"))
        //{
        //    //var fileInfo = other.gameObject.GetComponent<FileObject>();
        //    //if (fileInfo != null)
        //    //{
        //    //    var folder = GetFolderInDirectories(gameObject.name);
        //    //    if (!folder.Equals("null"))
        //    //    {
        //    //        FileUtil.MoveFileOrDirectory(fileInfo.Info.DirectoryName, folder);
        //    //    }
        //    //    else
        //    //    {
        //    //        var root = gameObject.GetComponentInParent<FileReader>().GetRootDirectory();
        //    //        AssetDatabase.CreateFolder(root.ToString(), fileInfo.name);
        //    //    }
        //    //}
        //    Debug.Log("file -> shelf");
        //}
        if (other.gameObject.CompareTag("file"))
        {
            var fileInfo = other.gameObject.GetComponent<FileObject>();
            if (fileInfo != null)
            {
                var folder = GetDirectory();
                if (folder == null)
                {
                    var root = gameObject.GetComponentInParent<FileReader>().GetRootDirectory();
                    AssetDatabase.CreateFolder(root.Name, fileInfo.name);
                }
                FileUtil.MoveFileOrDirectory(fileInfo.Info.Name, folder.Name);

                Debug.Log("shelf -> file");
            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (!collision.gameObject.CompareTag("file"))
    //    {
    //        var fileInfo = collision.gameObject.GetComponent<FileObject>();
    //        if (fileInfo != null)
    //        {
    //            var folder = GetFolderInDirectories(gameObject.name);
    //            if (folder != null)
    //            {
    //                FileUtil.MoveFileOrDirectory(fileInfo.Info.Name, folder.Name);
    //            }
    //            else
    //            {
    //                var root = gameObject.GetComponentInParent<FileReader>().GetRootDirectory();
    //                AssetDatabase.CreateFolder(root.ToString(), fileInfo.name);
    //            }
    //        }
    //    }
    //    else if (!collision.gameObject.CompareTag("container"))
    //    {
    //        var fileInfo = collision.gameObject.GetComponent<FileObject>();
    //        if (fileInfo != null)
    //        {
    //            var folder = GetFolderInDirectories(gameObject.name);
    //            if (!folder.Equals("null"))
    //            {
    //                FileUtil.MoveFileOrDirectory(fileInfo.Info.DirectoryName, folder);
    //            }
    //            else
    //            {
    //                var root = gameObject.GetComponentInParent<FileReader>().GetRootDirectory();
    //                AssetDatabase.CreateFolder(root.ToString(), fileInfo.name);
    //            }
    //        }
    //    }
    //}

    //private DirectoryInfo GetFolderInDirectories(string dName)
    //{
    //    if (_infos == null)
    //    {
    //        _infos = GetComponentInParent<FileManager>().GetRootDirectory();
    //    }
    //    var directories = _infos.GetDirectories();
    //    foreach (var directory in directories)
    //    {
    //        if (directory.Name.Equals(dName))
    //        {
    //            return directory;
    //        }
    //    }
    //    return null;
    //}

    private DirectoryInfo GetDirectory()
    {
        return _infos;
    }
}
