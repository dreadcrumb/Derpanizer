using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileReader
{

    public static FileTree ReadFileTree(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        if (!directoryInfo.Exists)
        {
            return null;
        }
        return new FileTree();
    }
}
