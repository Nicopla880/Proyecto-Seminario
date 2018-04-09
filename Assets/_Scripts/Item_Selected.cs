using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Selected : MonoBehaviour {

	public Weapons Weapon;
	public Shields Shield;

	public CookManager Script;

	void Start(){

		Script = GameObject.Find ("CookingManagement").GetComponent<CookManager>();
	}

	public void BuyWeapon(){

		Cook_Load_Data.gd.Jugador [0] = (int)Weapon.damage;
		SaveManager.SaveGame (Cook_Load_Data.gd);
		Script.Cooking ();

	}
		
	public void BuyShield(){

		Cook_Load_Data.gd.Jugador [1] = (int)Shield.defence;
		SaveManager.SaveGame (Cook_Load_Data.gd);
		Script.Cooking ();


	}


}
