using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenHandler : MonoBehaviour
{
	public void ImageTrack()
	{
		SceneManager.LoadScene("ImageTrack");
	}
	public void FloorTrack()
	{
		SceneManager.LoadScene("FloorTrack");
	}
}
