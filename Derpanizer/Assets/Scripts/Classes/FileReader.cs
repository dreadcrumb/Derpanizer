using System.Collections.Generic;
using System.IO;
using Assets.Scripts.ObjectManager;

namespace Assets.Scripts.Classes
{
    public class FileReader
    {

        public string Path;
        public DirectoryInfo DirectoryInfo;

        public FileReader(string path)
        {
            Path = path;
            ReadFirstLayer();
        }

        public void ReadFirstLayer()
        {
            if (string.IsNullOrEmpty(Path))
            {
                // TODO : Error handling
                return;
            }
            DirectoryInfo = new DirectoryInfo(Path);
            var directories = DirectoryInfo.GetDirectories();
            var fileInfo = DirectoryInfo.GetFiles();
            foreach (var file in fileInfo)
            {
                switch (file.Extension)
                {
                    case "txt":
                        break;
                }
            }

        }


    }
}
