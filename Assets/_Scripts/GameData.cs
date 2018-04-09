using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveManager {

	public static string fileName = "gameData6.db";



	public static void SaveGame(GameData gd) {

		string path = Application.persistentDataPath + "/" + SaveManager.fileName;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream (path, FileMode.Create);
		bf.Serialize (stream, gd);
		stream.Close ();
	}

	public static GameData LoadGame() {


		string path = Application.persistentDataPath + "/" + SaveManager.fileName;

		if (File.Exists(path)) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			GameData gd = (GameData)bf.Deserialize (stream);
			stream.Close ();
			return gd;

		} else {
           
			GameData gd = new GameData ();
			gd.Jugador = new Dictionary<int, int> ();
			gd.Opciones = new Dictionary<int, int> ();
			gd.Gold = 100;
			return gd;
		}
			
	}

}

[Serializable]
public class GameData {

	public Dictionary<int,int> Jugador;
	public Dictionary<int,int> Opciones;
	public int Gold;
}
