using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FileManagerScripts
{
	public class PictureResizer : MonoBehaviour
	{

		public Texture2D DefaultImage;

		// Use this for initialization
		void Start()
		{
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void InitImage(string imgPath)
		{
			var texture = new Texture2D(10, 10, TextureFormat.RGB565, false);
			var www = new WWW(imgPath);
			www.LoadImageIntoTexture(texture);

			if (gameObject.GetComponent<Renderer>().material == null)
			{
				Material mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/Models/Materials/metal.mat", typeof(Material));
				gameObject.GetComponent<Renderer>().material = mat;
			}

			gameObject.GetComponent<Renderer>().material.mainTexture = texture;
			gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 1);
		}
	}
}
