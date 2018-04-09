using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CookManager : MonoBehaviour {


	[Header("Panel")]

	public GameObject Panel;

	[Header("Buttons")]

	public GameObject ButtonCook;
	public GameObject ButtonClose;
	public GameObject ButtonLanzas;
	public GameObject ButtonEscudos;

	[Header("UI Sprite")]
	public Image DoorOpen;
	public Image DoorClosed;


	[Header("Timer Text")]
	public Text TimerText;

	private Sprite[] SpriteSheet;
	private bool BCook;
	private DateTime ActualTime;

	void Start(){

		SpriteSheet = Resources.LoadAll<Sprite> (@"Sprites/compra_panaderia_menu");
		BCook = false;
	}

	void Update(){

		CountDown (BCook);
		TimerText.text = (System.DateTime.Now - ActualTime).ToString();
	}



	public void CookButton (bool Active) {

		if (Active) {

			Panel.SetActive (true);
			ButtonLanzas.SetActive (true);
			ButtonEscudos.SetActive (true);
			ButtonClose.SetActive (true);

			
		} else {

			Panel.SetActive (false);
			ButtonLanzas.SetActive (false);
			ButtonEscudos.SetActive (false);
			ButtonClose.SetActive (false);

		}

		
	}
		

	public void LanzasButton(){

		ButtonLanzas.GetComponent<Image> ().sprite = SpriteSheet[2];
		ButtonEscudos.GetComponent<Image> ().sprite = SpriteSheet[0];

		for (int i = 0; i < this.GetComponent<Cook_Load_Data>().ClonL.Length; i++)
		{
			this.GetComponent<Cook_Load_Data> ().ClonL [i].SetActive (true);
		}

		for (int i = 0; i < this.GetComponent<Cook_Load_Data>().ClonE.Length; i++)
		{
			this.GetComponent<Cook_Load_Data> ().ClonE [i].SetActive (false);
		}
	}


	public void EscudosButton(){

		ButtonEscudos.GetComponent<Image> ().sprite = SpriteSheet[1];
		ButtonLanzas.GetComponent<Image> ().sprite = SpriteSheet[3];

		for (int i = 0; i < this.GetComponent<Cook_Load_Data>().ClonE.Length; i++)
		{
			this.GetComponent<Cook_Load_Data> ().ClonE [i].SetActive (true);
		}

		for (int i = 0; i < this.GetComponent<Cook_Load_Data>().ClonL.Length; i++)
		{
			this.GetComponent<Cook_Load_Data> ().ClonL [i].SetActive (false);
		}

	}


	public void Cooking(){


		CookButton (false);
		ButtonCook.SetActive (false);
		DoorOpen.enabled = false;
		DoorClosed.enabled = true;
		TimerText.enabled = true;

		ActualTime = System.DateTime.Now;
		BCook = true;



	}



	private void CountDown(bool Active){
	
		if (Active) {

			if (ActualTime.Minute + 1 == System.DateTime.Now.Minute) {
				TimerText.enabled = false;
				ButtonCook.SetActive (true);
				DoorOpen.enabled = true;
				DoorClosed.enabled = false;
			}

		}

	}



}
