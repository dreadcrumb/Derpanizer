using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FileManagerScripts
{
    public class CopyScript : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Const.Const.FILE))
            {
                // copy file in file system
                var fileToCopy = other.gameObject.GetComponent<FileObject>();
                var extension = fileToCopy.Info.Extension;
                var filename = fileToCopy.Info.ToString().TrimEnd(extension.ToCharArray());
                var pathParts = filename.Split(Const.Const.BACKSLASH.ToCharArray());

                var numberAtEnd = "0";
                if (pathParts.Last().Contains(Const.Const.COPY))
                {
                    numberAtEnd = GetNumbers(pathParts.Last());
                }


                if (!numberAtEnd.Equals("0"))
                {
                    var copyname = filename + Const.Const.COPY + (int.Parse(numberAtEnd) + 1) +
                                      extension;
                    FileUtil.CopyFileOrDirectory(fileToCopy.Info.ToString(), copyname);
                }
                else
                {
                    var copyname = filename + Const.Const.COPY + 1 + extension;
                    FileUtil.CopyFileOrDirectory(fileToCopy.Info.ToString(), copyname);
                }

                // Copy gameobject
                Instantiate(other.gameObject, gameObject.transform);
            }
        }

        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}