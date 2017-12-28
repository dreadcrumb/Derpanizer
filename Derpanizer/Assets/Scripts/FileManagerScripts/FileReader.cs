using System.IO;
using UnityEngine;

public class FileReader
{

    public string Path;
    public DirectoryInfo DirectoryInfo;
    public Transform File;


    public FileReader(string path)
    {
        Path = path;
    }

    public FileInfo[] GetRootFileInfo()
    {
        if (string.IsNullOrEmpty(Path))
        {
            // TODO : Error handling
            return new FileInfo[0];
        }

        DirectoryInfo = new DirectoryInfo(Path);
        return DirectoryInfo.GetFiles();

    }

    public FileInfo[] GetFileInfo(string path)
    {
        if (string.IsNullOrEmpty(Path))
        {
            // TODO : Error handling
            return new FileInfo[0];
        }

        DirectoryInfo = new DirectoryInfo(Path + path);
        //DirectoryInfo.GetDirectories();
        return DirectoryInfo.GetFiles();

    }

    public DirectoryInfo[] GetAllDirectories()
    {
        return DirectoryInfo.GetDirectories();
    }

    public DirectoryInfo GetRootDirectory()
    {
        return DirectoryInfo;
    }
}