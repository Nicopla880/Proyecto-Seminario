using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public AudioSource ClickSound;

	public void Batalla(){

		ClickSound.Play ();
		SceneManager.LoadScene (2);
	}

	public void Panaderia(){

		ClickSound.Play ();
		SceneManager.LoadScene (1);
	}

	public void Establo(){

		ClickSound.Play ();
	}

}
