using System;
using System.IO;
using Assets.Scripts.Classes;
using UnityEngine;

namespace Assets.Scripts.ObjectManager
{
    public class FileManager
    {

        private FileReader _reader;

        public FileManager(string path)
        {
            _reader = new FileReader(path);
        }

        public void Init()
        {
            if (_reader != null)
            {
                _reader.ReadFirstLayer();
            }

        }

        // not used for now
        //public void InitFurniture()
        //{
        //    foreach (var d in directories)
        //    {
        //        if (d.Name.Equals("desk"))
        //        {
        //            //UnityEngine.Object.Instantiate(Resources.Load("desk"));
        //            //UnityEngine.Object.Instantiate(GameObject.FindGameObjectWithTag("desk"));

        //        }
        //    }
        //}
    }
}
