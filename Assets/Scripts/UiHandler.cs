using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
	public void Menu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void MarkerBased()
	{
		SceneManager.LoadScene("MarkerBasedAR");
	}
	public void Markerless()
	{
		SceneManager.LoadScene("MarkerLessAR");
	}

}
