using System.IO;
using UnityEditor;
using UnityEngine;

namespace FileManagerScripts
{
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
                    var source = fileInfo.Info.ToString();
                    var destination = folder + Const.Const.SLASH + fileInfo.Info.Name;

                    if (!source.Equals(destination))
                    {
                        FileUtil.MoveFileOrDirectory(source, destination);
                        FileInfo info = new FileInfo(folder.ToString() + Const.Const.SLASH + fileInfo.Info.Name);
                        fileInfo.Info = info;
                    }
                }
            }
        }

        private DirectoryInfo GetDirectory()
        {
            return _infos;
        }
    }
}
