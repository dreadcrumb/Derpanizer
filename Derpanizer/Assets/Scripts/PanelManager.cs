using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PanelManager : MonoBehaviour {

	public Animator InitiallyOpen;

	private int _mOpenParameterId;
	private Animator _mOpen;
	private GameObject _mPreviouslySelected;

	const string KOpenTransitionName = "Open";
	const string KClosedStateName = "Closed";

	public void OnEnable()
	{
		_mOpenParameterId = Animator.StringToHash (KOpenTransitionName);

		if (InitiallyOpen == null)
			return;

		OpenPanel(InitiallyOpen);
	}

	public void OpenPanel (Animator anim)
	{
		if (_mOpen == anim)
			return;

		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

		CloseCurrent();

		_mPreviouslySelected = newPreviouslySelected;

		_mOpen = anim;
		_mOpen.SetBool(_mOpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (_mOpen == null)
			return;

		_mOpen.SetBool(_mOpenParameterId, false);
		SetSelected(_mPreviouslySelected);
		StartCoroutine(DisablePanelDeleyed(_mOpen));
		_mOpen = null;
	}

	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(KClosedStateName);

			wantToClose = !anim.GetBool(_mOpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
}
