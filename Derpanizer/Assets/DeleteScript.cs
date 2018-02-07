using FileManagerScripts;
using UnityEditor;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.Const.FILE))
        {
            var fileInfo = other.GetComponent<FileObject>().Info;
            Destroy(other.gameObject);
            FileUtil.DeleteFileOrDirectory(fileInfo.ToString());
        }
    }
}
