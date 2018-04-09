using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour {


	public  Button Top;
	public  Button Mid;
	public  Button Bot;
	public Button BuffA;
	public Button BuffB;

	public GameObject SG;

	public GameplayManagement Management;

	public int ButtonNum;

	public AudioSource ClickSound;

	public bool BA;
	public bool BB;

	void Start(){

		Management.enabled = false;

		BA = false;
		BB = false;

		Top.enabled = false;
		Mid.enabled = false;
		Bot.enabled = false;

		BuffA.interactable = false;
		BuffB.interactable = false;
	}


	public void StartGame (){

		Management.enabled = true;

		Top.enabled = true;
		Mid.enabled = true;
		Bot.enabled = true;

		SG.SetActive (false);

		StartCoroutine (BuffATimer ());
		StartCoroutine (BuffBTimer ());
	}

	public void ButtonClick(int Num){

		ClickSound.Play ();

		switch (Num) {

		case 0:
			ButtonNum = 0;


			if (Management.FightStatus == false) {

				Management.Player.transform.localScale = new Vector3(-1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [5];
			} else {

				Management.Player.transform.localScale = new Vector3(1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [3];
			}
			break;
		
		case 1:
			ButtonNum = 1;

			if (Management.FightStatus == false) {

				Management.Player.transform.localScale = new Vector3(-1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [6];
			} else {

				Management.Player.transform.localScale = new Vector3(1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [4];
			}
			break;

		case 2:
			ButtonNum = 2;

			if (Management.FightStatus == false) {

				Management.Player.transform.localScale = new Vector3(-1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [2];
			} else {

				Management.Player.transform.localScale = new Vector3(1,1,1);
				Management.Player.GetComponent<SpriteRenderer> ().sprite = Management.SpriteSheet [0];
			}
			break;


		}


	}

	public void BuffButton(int Num){

		switch (Num) {

	
		case 1:
			BuffA.interactable = false;
			StartCoroutine (BuffATimer ());
			BB = true;

			break;

		
		case 2:
			BuffB.interactable = false;
			StartCoroutine (BuffBTimer ());
			BA = true;
			break;


		}

	}

	IEnumerator BuffATimer(){

		yield return new WaitForSeconds (5);
		BuffA.interactable = true;
		


	}

	IEnumerator BuffBTimer(){

		yield return new WaitForSeconds (10);
		BuffB.interactable = true;
	}
}
