using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ExampleSerialization : MonoBehaviour
{
    public string path = "Assets/Serializacion/Data/TheInfo";

    public string username;
    public string password;

    public UserInfo myInfo;

    private void Update()
    {
        //Binary
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //Serialize Binary
            SerializeBinary();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            //Deserialize Binary
            DeserializeBinary();
        }
        //Json
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            //Serialize Json
            SerializeJson();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            //Deserialize Json
            DeserializeJson();
        }
    }

    void SerializeBinary()
    {
        //Creamos el formateador (el que va a traspasar la info original a binary y hacia el archivo)
        BinaryFormatter bf = new BinaryFormatter();

        //Creamos el archivos, vacio
        FileStream file = File.Create(path + ".bin");

        //Traspasamos la info
        bf.Serialize(file, myInfo.username);
        bf.Serialize(file, myInfo.password);

        //Cerramos el archivo
        file.Close();
    }

    void DeserializeBinary()
    {
        if (!File.Exists(path + ".bin")) return;

        //Creamos el formateador (el que va a traspasar la info original a binary y hacia el archivo)
        BinaryFormatter bf = new BinaryFormatter();

        //Levantamos el archivo
        FileStream file = File.Open(path + ".bin", FileMode.Open);

        //Deserializamos la info del archivo (de binary a object)
        username = (string)bf.Deserialize(file);
        password = (string)bf.Deserialize(file);

        //Cerramos el archivo
        file.Close();
    }

    void SerializeJson()
    {
        //Creamos el archivo en su respectivo lugar (path)
        StreamWriter file = File.CreateText(path + ".json");

        //Pasamos la info a string
        string js = JsonUtility.ToJson(myInfo, true);

        //Guardamos la info en el archivo
        file.Write(js);

        //Cerramos el archivo
        file.Close();
    }

    void DeserializeJson()
    {
        if (!File.Exists(path + ".json")) return;

        //Obtenemos la info del archivo
        string js = File.ReadAllText(path + ".json");

        //Deserializamos esa info
        myInfo = JsonUtility.FromJson<UserInfo>(js);
    }
}