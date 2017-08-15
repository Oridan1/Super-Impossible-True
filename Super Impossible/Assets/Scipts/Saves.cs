using UnityEngine;
using System.Collections;
using System;// libreria para guardado  de datos 
using System.Runtime.Serialization.Formatters.Binary; //  lib para guardado de datos
using System.IO;


public class Saves : MonoBehaviour
{

    //public PlayerController game;

    public static Saves control;

   // public string level;

    // Use this for initialization
    void Awake()
    {
        if (control == null)
        {
            //DontDestroyOnLoad(gameObject);
            //control = this;
        }
        else
        {
            if (control != this)
            {
               // Destroy(gameObject);
            }
        }
    }

    public void Start()
    {
        //Saves.control.Load();
    }

    public void Update()
    {
        
    }

    public void Save(PlayerController game)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData(); // crea un nuevo player data 
        //Array.Copy(game.bestScore, data.score, game.bestScore.Length);// al crear uno  nuevo  lo  sobreescribe con los datos deseados 
        //Array.Copy(game.bestPosition, data.posicion, game.bestPosition.Length);        
        data.score0 = game.BestScore()[0];
        data.score1 = game.BestScore()[1];
        data.posicion0 = game.BestPosition()[0];
        data.posicion1 = game.BestPosition()[1];        
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveMonedas(PlayerController game)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo2.dat");
        PlayerMonedas data = new PlayerMonedas();
        data.setMonedas1 = game.SetMonedas()[1];
        data.maxMonedas1 = game.MaxMonedas()[1];
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load(PlayerController game)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            // Array.Copy(data.score, game.bestScore, data.score.Length);
            // Array.Copy(data.posicion, game.bestPosition, data.posicion.Length);   
            game.BestScore()[0] = data.score0;
            game.BestScore()[1] = data.score1;
            game.BestPosition()[0] = data.posicion0;
            game.BestPosition()[1] = data.posicion1;            
        }
    }

    public void LoadMonedas(PlayerController game)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo2.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo2.dat", FileMode.Open);
            PlayerMonedas data = (PlayerMonedas)bf.Deserialize(file);
            file.Close();
            game.SetMonedas()[1] = data.setMonedas1;
            game.MaxMonedas()[1] = data.maxMonedas1;
        }
            
    }

}
[Serializable]
class PlayerData
{
    public float score0;
    public float posicion0;
    public float score1;
    public float posicion1;   
}
[Serializable]
class PlayerMonedas
{
    public int setMonedas1;
    public int maxMonedas1;
}