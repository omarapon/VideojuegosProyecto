using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager3 : MonoBehaviour
{
    public int vidas = 3;
    public int monedas = 0;
    public int escudos = 0;
    public Text monedasText;
    public Text vidasText;
    public Text escudosText;
    void Start()
    {
        LoadGame();
        Escribir();
    }

    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/data.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        Data data = new Data();
        data.Vidas = vidas;
        data.Monedas = monedas;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/data.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        Data data = (Data)bf.Deserialize(file);
        file.Close();

        UnityEngine.Debug.Log(data.Monedas);
        monedas = data.Monedas;
        vidas = data.Vidas;
        //PrintPuntosInScreen();
        UnityEngine.Debug.Log("Carga1");

    }
    public void Escribir(){
        monedasText.text = "Monedas: " + monedas;
        vidasText.text = "Vidas: " + vidas;
        escudosText.text = "Escudos: " + escudos;
    }
    public void CambioVidas(int a){
        vidas = vidas + a;
        Escribir();
    }
    public void CambioMonedas(int a){
        monedas = monedas + a;
        Escribir();
    }
    public void CambioEscudos(int a){
        escudos = escudos + a;
        Escribir();
    }
}
