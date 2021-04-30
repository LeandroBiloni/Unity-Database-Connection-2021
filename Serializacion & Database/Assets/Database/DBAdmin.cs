using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class DBAdmin : MonoBehaviour
{
    public string host, database, dbUser, dbPw;

    IEnumerator DoQuery(string phpScript, WWWForm form, Action<string> successCallback = null, Action<string> failureCallback = null)
    {
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/" + phpScript + ".php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text[0] == '0')
            {
                Debug.Log("Ok");
                successCallback?.Invoke(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Fail");
                failureCallback?.Invoke(www.downloadHandler.text);
            }
        }
    }

    WWWForm CreateForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("host", host);
        form.AddField("database", database);
        form.AddField("dbuser", dbUser);
        form.AddField("dbpw", dbPw);

        return form;
    }

    public void Register(string username, string password)
    {
        WWWForm form = CreateForm();
        form.AddField("user", username);
        form.AddField("pass", password);

        StartCoroutine(DoQuery("register", form));
    }

    public void Login(string username, string password, Action<string> successCallback, Action<string> failureCallback)
    {
        WWWForm form = CreateForm();

        string query = "SELECT usern FROM `accounts` WHERE usern = ('" + username + "') AND passw = ('" + password + "');";
        form.AddField("query", query);

        StartCoroutine(DoQuery("login", form, successCallback, failureCallback));

    }

    public void SetScore(string username, string score)
    {
        WWWForm form = CreateForm();
        form.AddField("user", username);
        form.AddField("score", score);

        StartCoroutine(DoQuery("setscore", form));
    }

    public void GetScore(string username, Action<string> successCallback, Action<string> failureCallback)
    {
        WWWForm form = CreateForm();

        string query = "SELECT score FROM `highscores` WHERE user = ('" + username + "');";
        form.AddField("query", query);

        StartCoroutine(DoQuery("getscore", form, successCallback, failureCallback));
    }

    public void DeleteScore(string username, Action<string> successCallback)
    {
        WWWForm form = CreateForm();

        form.AddField("user", username);

        StartCoroutine(DoQuery("deletescore", form, successCallback));
    }

}
