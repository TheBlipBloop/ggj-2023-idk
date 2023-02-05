using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonUtil : MonoBehaviour
{
	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
	}

	public void ActivateGameobject(GameObject obj)
	{
		obj.SetActive(true);
		transform.parent.gameObject.SetActive(false);
	}
}
