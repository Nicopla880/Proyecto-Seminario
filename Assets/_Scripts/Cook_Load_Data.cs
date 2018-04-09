using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook_Load_Data : MonoBehaviour {

	public static string URL_File = "Data/data";
	public static GameData gd;

	public Transform PanelItems;
	public GameObject PrefabLanza;
	public GameObject PrefabEscudo;

	public GameObject[] ClonL;
	public GameObject[] ClonE;

	private Sprite[] SpriteSheet;
	private Sprite[] SpriteSheet2;

	private int lan;
	private int esc;

	private void Awake()
	{

		gd = SaveManager.LoadGame(); 
	} 



	private void Start()
	{

		SpriteSheet = Resources.LoadAll<Sprite>(@"Sprites/PAMELA");
		SpriteSheet2 = Resources.LoadAll<Sprite> (@"Sprites/Grissines");
		TextAsset txt = Resources.Load<TextAsset>(URL_File);

		ParseJson(txt.text);

	}

	void ParseJson(string dataJson)
	{
		LoadJson jsLoad = JsonUtility.FromJson<LoadJson> (dataJson);

		foreach (Weapons Weap in jsLoad.weapons)
		{

			GameObject clon = Instantiate (PrefabLanza) as GameObject;

		/*	int valor = 0;
			gd.Jugador.TryGetValue (0, out valor);
			Weap.damage = valor;*/

			clon.transform.SetParent (PanelItems);
			clon.transform.localScale = Vector3.one;
			clon.transform.Find ("Name").GetComponent<Text>().text = Weap.title;
			clon.transform.Find ("Price").GetComponent<Text>().text = Weap.price.ToString();
			clon.transform.Find ("Damage").GetComponent<Text>().text = Weap.damage.ToString();
			clon.transform.Find ("ItemImage1").GetComponent<Image>().sprite = SpriteSheet2[esc];
			clon.transform.Find ("ItemImage2").GetComponent<Image>().sprite = SpriteSheet2[esc];
			clon.GetComponent<Item_Selected> ().Weapon = Weap;
			ClonL[lan] = clon;
			lan++;

		}

		foreach (Shields Shie in jsLoad.shields)
		{
			GameObject clon = Instantiate (PrefabEscudo) as GameObject;

		/*	int valor = 0;
			gd.Jugador.TryGetValue (Shie.id, out valor);
			Shie.id = valor;*/

			clon.SetActive (false);
			clon.transform.SetParent (PanelItems);
			clon.transform.localScale = Vector3.one;
			clon.transform.Find ("Name").GetComponent<Text>().text = Shie.title;
			clon.transform.Find ("Price").GetComponent<Text>().text = Shie.price.ToString();
			clon.transform.Find ("Defence").GetComponent<Text>().text = Shie.defence.ToString();
			clon.transform.Find ("ItemImage1").GetComponent<Image>().sprite = SpriteSheet[esc];
			clon.transform.Find ("ItemImage2").GetComponent<Image>().sprite = SpriteSheet[esc + 5];
			clon.GetComponent<Item_Selected> ().Shield = Shie;

			ClonE [esc] = clon;
			esc++;
		}


	}

}
