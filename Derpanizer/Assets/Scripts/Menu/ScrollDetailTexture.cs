using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class ScrollDetailTexture : MonoBehaviour
{
	public bool UniqueMaterial = false;
	public Vector2 ScrollPerSecond = Vector2.zero;

	Matrix4x4 _mMatrix;
	Material _mCopy;
	Material _mOriginal;
	Image _mSprite;
	Material _mMat;

	void OnEnable ()
	{
		_mSprite = GetComponent<Image>();
		_mOriginal = _mSprite.material;

		if (UniqueMaterial && _mSprite.material != null)
		{
			_mCopy = new Material(_mOriginal);
			_mCopy.name = "Copy of " + _mOriginal.name;
			_mCopy.hideFlags = HideFlags.DontSave;
			_mSprite.material = _mCopy;
		}
	}

	void OnDisable ()
	{
		if (_mCopy != null)
		{
			_mSprite.material = _mOriginal;
			if (Application.isEditor)
				UnityEngine.Object.DestroyImmediate(_mCopy);
			else
				UnityEngine.Object.Destroy(_mCopy);
			_mCopy = null;
		}
		_mOriginal = null;
	}

	void Update ()
	{
		var mat = (_mCopy != null) ? _mCopy : _mOriginal;

		if (mat != null)
		{
			var tex = mat.GetTexture("_DetailTex");

			if (tex != null)
			{
				mat.SetTextureOffset("_DetailTex", ScrollPerSecond * Time.time);

				// TODO: It would be better to add support for MaterialBlocks on UIRenderer,
				// because currently only one Update() function's matrix can be active at a time.
				// With material block properties, the batching would be correctly broken up instead,
				// and would work with multiple widgets using this detail shader.
			}
		}
	}
}
