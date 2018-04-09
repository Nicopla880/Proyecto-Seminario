using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManagement : MonoBehaviour {


	[Header("Scripts")]
	public ButtonManagement ButtonScript;

	[Header("Players")]
	public GameObject EnemySprite;
	public GameObject Player;

	[Header("Health bars")]
	public Image ShieldBar;
	public Image EnemyBar;

	[Header("Texts")]
	public Text Status;
	public Text FighStatus;
	public Text GoldText;

	[Header("JSON DATA")]
	public Weapons GMWeapon;
	public Shields GMShield;
	public Enemys GMEnemy;

	[Header("Others")]
	public Sprite[] SpriteSheet;
	public bool FightStatus;  // TRUE DEFENCE // FALSE ATTACK
	public bool EspecialAttack;

	private int Rand;
	private float BuffAttack;

	private bool Trigger;
	private bool Restart;
	private int aux = 0;

	public int Gold;

	void Start(){

		SpriteSheet = Resources.LoadAll<Sprite> (@"Sprites/Atk_sprite_sheet");
		ButtonScript.ButtonNum = -1;
		FighStatus.text = "Defend";
		FightStatus = true;
		Trigger = true;
		EspecialAttack = false;
		GetRandom();

		BuffAttack = (GMWeapon.damage/2) + GMWeapon.damage;


	}

	void Update () {

		MoveSprites (Trigger);
		OnTrigger (Trigger);

	}


		
	private void MoveSprites(bool Active){

		if (Active && EnemySprite.transform.localPosition.x > -0.5f) {
			
			EnemySprite.transform.localPosition += new Vector3 (-GMEnemy.speed * Time.deltaTime, 0f, 0f);

		} else {

			Trigger = false;

			if (FightStatus == false) {
				Player.transform.localScale = new Vector3 (-1, 1, 1);
				Player.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [1];
			} 

			if (FightStatus == true) {
				Player.transform.localScale = new Vector3 (-1, 1, 1);
				Player.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load("Sprites/no_defense",typeof(Sprite));
			} 


		}

	}

	private void GetRandom(){

		Rand = Random.Range (0, 3);
		int Aux = Rand;
		ChangeSprite ();

		if (Random.Range (0, 99) > 1 && FightStatus == true) {

			EspecialAttack = true;
			while (Rand == Aux) {
				Rand = Random.Range (0, 3);
			}
		}

		StartCoroutine (EspecialTime());
	


	}

	IEnumerator EspecialTime()
	{
		yield return new WaitForSeconds (0.5f);
		ChangeSprite ();
	}


	private void ChangeSprite(){



		switch (Rand) {

		case 0:
			if (FightStatus == false) {
				EnemySprite.transform.localScale = new Vector3(-1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [3];
			} else {
				EnemySprite.transform.localScale = new Vector3(1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [5];
			}

			break;

		case 1:
			if (FightStatus == false) {
				EnemySprite.transform.localScale = new Vector3(-1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [4];
			}
			else {
				EnemySprite.transform.localScale = new Vector3(1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [6];
			}


			break;

		case 2:
			if (FightStatus == false) {
				EnemySprite.transform.localScale = new Vector3(-1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [0];
			}
			else {
				EnemySprite.transform.localScale = new Vector3(1,1,1);
				EnemySprite.GetComponent<SpriteRenderer> ().sprite = SpriteSheet [2];
			}
			break;



		}
	}

	private void OnTrigger(bool Active){

		if (Active == false && Restart == false) {

			CancelInvoke ("GetRandom");


			if (FightStatus == true) {

				if (GMShield.defence <= 0) {
					ShieldBar.fillAmount = 0;
					StartCoroutine (Lose ());
				} else {
			
					if (ButtonScript.ButtonNum == Rand) {

						Status.text = "You defended the attack";
						StartCoroutine (ReloadTimer ());

					} else {

						Status.text = "You have been hit";
						GMShield.defence -= GMEnemy.damage;
						ShieldBar.fillAmount -= GMEnemy.damage / 100;

						if (GMShield.defence <= 0) {
							ShieldBar.fillAmount = 0;
							StartCoroutine (Lose ());


						} else {
							StartCoroutine (ReloadTimer ());
						}
					}
				}
			} else {

				if (EnemyBar.fillAmount <= 0) {
					EnemyBar.fillAmount = 0;
					StartCoroutine (Win ());
				} else {

					if (ButtonScript.ButtonNum == Rand || ButtonScript.ButtonNum == -1) {

						Status.text = "You failed the attack";
						StartCoroutine (ReloadTimer ());

					} else {

						if (ButtonScript.BA == true) {

							Status.text = "You hit the enemy";
							GMEnemy.defence -= BuffAttack;
							EnemyBar.fillAmount -= GMWeapon.damage / 100;
							ButtonScript.BA = false;

							if (GMEnemy.defence <= 0) {
								EnemyBar.fillAmount = 0;
								StartCoroutine (Win ());
							} else {

								StartCoroutine (ReloadTimer ());
							}
						} else {

							Status.text = "You hit the enemy";
							GMEnemy.defence -= GMWeapon.damage;
							EnemyBar.fillAmount -= GMWeapon.damage / 100;

	
							if (GMEnemy.defence <= 0) {
								EnemyBar.fillAmount = 0;
								StartCoroutine (Win ());
							} else {
							
								StartCoroutine (ReloadTimer ());
							}
						}
					}
				}


			}

			if (FightStatus == true) {
				aux = 0;
			} else 
			{
				aux = 1;
			}

			if (ButtonScript.BB == true) {
				FightStatus = false;

				if (FightStatus == false) {
					FightStatus = false;
					FighStatus.text = "Attack";
				} 

				EspecialAttack = false;
				Restart = true;
				ButtonScript.ButtonNum = -1;

				aux++;



				if(aux == 2)
				{
				ButtonScript.BB = false;
				aux = 0;
				}
			} 
			else 
			{
				if (FightStatus == true) {
					FightStatus = false;
					FighStatus.text = "Attack";
				} else {
					FightStatus = true;
					FighStatus.text = "Defend";
				}

				EspecialAttack = false;
				Restart = true;
				ButtonScript.ButtonNum = -1;
			}
			
		}
	}



	IEnumerator ReloadTimer(){

		if (Trigger == false) {
			EnemySprite .transform.localPosition = new Vector3 (3f, EnemySprite.transform.localPosition.y, 0f);

		}
		yield return new WaitForSeconds (0.1f);

		Trigger = true;
		Restart = false;
		GetRandom();

	}


	IEnumerator Lose(){

		Status.text = "You Lose";
		yield return new WaitForSeconds (1);
		Status.text = "GAME RESTARTING";
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);
	}


	IEnumerator Win(){

		Status.text = "You Win";
		WinMatch ();
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);
		yield return new WaitForSeconds (1);
		Status.text = "GAME RESTARTING";
	}

	void WinMatch()
	{	
		Load_Data.gd.Gold += 10;
		SaveManager.SaveGame (Load_Data.gd);
	}

}
