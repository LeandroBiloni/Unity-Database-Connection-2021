using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Account : MonoBehaviour
{
    public GameObject registerScreen, loggedScreen;

    public TMP_Text welcomeText;

    public TMP_InputField scoreField;
    
    string _username;

    DBAdmin _db;

    private void Start()
    {
        _db = FindObjectOfType<DBAdmin>();
    }

    public void AccountLogged(string user)
    {
        _username = user;

        welcomeText.text = "Welcome " + _username + "!";

        NextScreen(true);
    }

    public void AccountLogOutBTN()
    {
        _username = "";

        NextScreen(false);
    }

    void NextScreen(bool isLogged)
    {
        registerScreen.SetActive(!isLogged);
        loggedScreen.SetActive(isLogged);
    }

    public void SetScoreBTN()
    {
        _db.SetScore(_username, scoreField.text);
    }

    public void GetScoreBTN()
    {
        _db.GetScore(_username, GetScoreSucceed, GetScoreFailed);
    }

    void GetScoreSucceed(string res)
    {
        Debug.Log("Bruto: " + res);
        
        string[] rows = res.Split('\n');

        string score = rows[1];
        scoreField.text = score;
    }

    void GetScoreFailed(string res)
    {
        Debug.Log(res);
    }


    public void DeleteScoreBTN()
    {
        _db.DeleteScore(_username, DeleteScoreSucceed);
    }

    void DeleteScoreSucceed(string res)
    {
        Debug.Log("Score deleted");
        scoreField.text = "0";
    }
}
