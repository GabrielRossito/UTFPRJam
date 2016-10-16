using UnityEngine;
using System.Collections;

//namespace da UI da Uinty
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonAbout;
    public Button buttonQuit;

    public void LoadPlayScene()
    {
		Debug.Log("Load Play");
		Application.LoadLevel("sceneIntro");
    }

    public void LoadAboutScene()
    {
		Debug.Log("Load About");
		Application.LoadLevel("sceneAbout");
    }

    public void QuitGame()
    {
		Debug.Log("Quit Game");
		Application.Quit();
    }
}