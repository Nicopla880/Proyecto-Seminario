using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using WWWKit;
using UnityEngine.SceneManagement;

[Serializable] 
public class LoadJson
{
	public List<Weapons> weapons; 
	public List<Shields> shields; 
	public List<Enemys> enemys;

}

[Serializable]
public class Weapons 
{
    public int id; 
    public int price;
    public string title;
	public float damage;
}

[Serializable]
public class Shields 
{
	public int id; 
	public int price;
	public string title;
	public float defence;
}

[Serializable]
public class Enemys 
{
	public int id; 
	public int price;
	public float damage;
	public float defence;
	public float speed;
}
	


public class Load_Data : MonoBehaviour {

	public static string URL_File = "Data/data";

    public static GameData gd;

	public Text DMGText;
	public Text ArmorText;


    private void Awake()
    {
        gd = SaveManager.LoadGame(); 


		this.GetComponent<GameplayManagement> ().GoldText.text = "Gold: " + gd.Gold;
    } 



    private void Start()
    {

		TextAsset txt = Resources.Load<TextAsset>(URL_File);

        ParseJson(txt.text);

    }
		

    void ParseJson(string dataJson)
    {
		LoadJson jsLoad = JsonUtility.FromJson<LoadJson> (dataJson);

		foreach (Weapons Weap in jsLoad.weapons)
		{

			int value = 0;
			if(gd.Jugador.TryGetValue (0, out value))
			{


				DMGText.text = value.ToString();
				this.GetComponent<GameplayManagement> ().GMWeapon.damage = value;
			}

			else
				
			{

				Weap.damage = 5;
				DMGText.text = Weap.damage.ToString();
				this.GetComponent<GameplayManagement> ().GMWeapon = Weap;
			}

		

		}

		foreach (Shields Shie in jsLoad.shields)
		{
			int value = 0;
			if(gd.Jugador.TryGetValue (1, out value))
			{


				ArmorText.text = value.ToString();
				this.GetComponent<GameplayManagement> ().GMShield.defence = value;

			}

			else

			{
				Shie.defence = 30;
				ArmorText.text = Shie.defence.ToString();
				this.GetComponent<GameplayManagement> ().GMShield = Shie;
			}

		}

		foreach (Enemys Ene in jsLoad.enemys)
		{
			if (SceneManager.GetActiveScene ().buildIndex == Ene.id) {
				this.GetComponent<GameplayManagement> ().GMEnemy = Ene;
			}
				
		}
    }

}


