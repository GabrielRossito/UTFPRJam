using UnityEngine;
using System.Collections;

//namespace da UI da Uinty
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControllerBack : MonoBehaviour
{
	public Button buttonBack;

	public void LoadMenuScene()
	{
		Debug.Log("Load Menu");
		Application.LoadLevel("sceneMenu");
	}
}