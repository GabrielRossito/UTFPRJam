using UnityEngine;
using System.Collections;

//namespace da UI da Uinty
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerController : MonoBehaviour
{
	public Button buttonCura;
	public Button buttonEscudo;
	public Button buttonForca;
	public Button buttonCarreta;

	public void Cura()
	{
		Debug.Log("Curar");
	}

	public void Escudo()
	{
		Debug.Log("Dar Resistência");
	}

	public void Forca()
	{
		Debug.Log("Dar Força");
	}

	public void Carreta()
	{
		Debug.Log("Invocar a Carreta Furacão");
	}
}