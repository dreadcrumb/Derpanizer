using System.IO;
using UnityEngine;

namespace FileManagerScripts
{
    public class FileObject : MonoBehaviour
    {
        public FileInfo Info
        { get; set; }

        public void Init(FileInfo fileInfo)
        {
            Info = fileInfo;
            //gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
        }

        //void OnCollisionStay(Collision collision)
        //{
        //    // TODO add cases for copy, delete, etc
        //    if (!collision.gameObject.CompareTag("container"))
        //    {
        //        return;
        //    }
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        return;
        //    }
        //    var directory = collision.gameObject.GetComponent<DirectoryInfo>();
        //    if (directory != null)
        //    {
        //        Info.MoveTo(directory.Name);
        //    }
        //}
    }
}