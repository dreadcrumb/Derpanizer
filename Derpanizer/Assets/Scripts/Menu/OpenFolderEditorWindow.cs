using UnityEditor;

namespace Assets.Scripts
{
	public class OpenFolderEditorWindow : EditorWindow
    {
        //[MenuItem("Load file directory")]
        public static string Apply()
        {
            var path = EditorUtility.OpenFolderPanel("Load file directory", "", "");
            return path;
        }
    }
}
