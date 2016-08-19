using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

	void Start () {
		Cursor.visible = false;
	}
	
	public void StartSinglePlayer () {
		SceneManager.LoadScene("SinglePlayer");
	}

	public void StartMultiPlayer () {
		SceneManager.LoadScene("SinglePlayer");
	}

	public void ExitGame () {
		Application.Quit();
	}
}
