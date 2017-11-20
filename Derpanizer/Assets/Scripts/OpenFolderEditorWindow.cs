using UnityEditor;

namespace Assets.Scripts
{
    public class OpenFolderEditorWindow : EditorWindow
    {
        //[MenuItem("Load file directory")]
        public static string Apply()
        {
            string path = EditorUtility.OpenFolderPanel("Load file directory", "", "");
            return path;
            //string[] files = Directory.GetFiles(path);

            //foreach (string file in files)
            //    if (file.EndsWith(".png"))
            //        File.Copy(file, EditorApplication.currentScene);
        }
    }
}
