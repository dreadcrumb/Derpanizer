using Assets.Scripts.FileManagerScripts;
using Assets.Scripts.SaveManager;
using UnityEngine;

namespace Assets.Scripts.GameManagerScript
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Manager;
		private FileManager _mFileManager;
		private SaveAndLoadScript _mSaveScript;
		private SaveClass _loadFile;

		private string _mPath;

		private void Awake()
		{
			if (Manager == null)
			{
				DontDestroyOnLoad(gameObject);
				Manager = this;
			}
			else if (Manager != this)
			{
				CopyManager(Manager);
				Destroy(Manager);
				InitFileManager();
			}
		}

		private void CopyManager(GameManager mngr)
		{
			_mFileManager = mngr._mFileManager;
			_mSaveScript = mngr._mSaveScript;
			_mPath = mngr._mPath;
			_loadFile = mngr._loadFile;
		}

		private void Start()
		{
			_mSaveScript = gameObject.AddComponent<SaveAndLoadScript>();
		}

		private void Update()
		{
			if (Input.GetKeyDown("o") && gameObject.GetComponent<FileManager>() != null)
			{
				gameObject.GetComponent<FileManager>().UpdateFileLocations();
				gameObject.GetComponent<SaveAndLoadScript>().Save();
			}
			else if (Input.GetKeyDown("r"))
			{
				var sgs = new StartGameScript();
				SetPath(sgs.InitDebug());
				sgs.LoadNewScene();
			}
		}

		public FileManager GetFileManager()
		{
			return gameObject.GetComponent<FileManager>();
		}

		public void SetPath(string path)
		{
			_mPath = path;
		}

		public string GetPath()
		{
			return _mPath;
		}

		public void LoadGame()
		{
			var sgs = new StartGameScript();
			_loadFile = _mSaveScript.Load();
			sgs.LoadNewScene();
		}

		public void StartGame()
		{
			var sgs = new StartGameScript();
			SetPath(sgs.InitRootPath());
			sgs.LoadNewScene();
		}

		private void InitFileManager()
		{
			_mFileManager = gameObject.AddComponent<FileManager>();

			if (_loadFile == null)
			{
				_mFileManager.InitFromScratch(_mPath);
			}
			else
			{
				_mFileManager.InitFromLoadFile(_loadFile);
			}

		}

	}
}
