using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChecker : MonoBehaviour
{
	public static bool isFirstTime = false;
	public GameObject startPanel;
	public GameObject MenuPanel;

	private void Start()
	{
		if (isFirstTime)
		{
			startPanel.SetActive(false);
			MenuPanel.SetActive(true);
		}
	}

	public void FirstPlay()
	{
		isFirstTime = true;
	}
}
