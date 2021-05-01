using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Account : MonoBehaviour
{
    public GameObject registerScreen, loggedScreen;

    public TMP_Text welcomeText;

    public TMP_InputField scoreField;

    public GameObject friendlistWindow;

    public TMP_InputField friendToAdd;

    public TMP_Text friendList;

    public TMP_Text receivedRequestText;

    public TMP_InputField friendToAccept;

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

    public void OpenFriendlistBTN()
    {
        friendlistWindow.SetActive(true);
    }

    public void CloseFriendlistBTN()
    {
        friendlistWindow.SetActive(false);
    }

    public void SendFriendRequestBTN()
    {
        _db.SendFriendRequest(_username, friendToAdd.text, SendFriendRequestSucceed, SendFriendRequestFailed);
    }

    void SendFriendRequestSucceed(string res)
    {
        Debug.Log("Friend request sent");
    }

    void SendFriendRequestFailed(string res)
    {
        Debug.Log("Friend request failed.");
        Debug.Log(res);
    }

    public void DeleteFriendRequestBTN()
    {
        _db.DeleteFriendRequest(_username, friendToAdd.text, DeleteFriendRequestSucceed, DeleteFriendRequestFailed);
    }

    void DeleteFriendRequestSucceed(string res)
    {
        Debug.Log("Friend deleted");
    }

    void DeleteFriendRequestFailed(string res)
    {
        Debug.Log("Friend delete failed.");
        Debug.Log(res);
    }

    public void CheckRequestsBTN()
    {
        _db.CheckRequests(_username, CheckRequestsSucceed, CheckRequestsFailed);
    }
    
    void CheckRequestsSucceed(string res)
    {
        Debug.Log("Requests Checked");
    }

    void CheckRequestsFailed(string res)
    {
        Debug.Log("Failed to check requests");
    }

    public void AcceptRequestsBTN()
    {
        _db.AcceptRequests(_username, friendToAccept.text, AcceptRequestsSucceed, AcceptRequestsFailed);
    }

    void AcceptRequestsSucceed(string res)
    {
        Debug.Log("Friend accepted");
    }

    void AcceptRequestsFailed(string res)
    {
        Debug.Log("Failed to accept request");
    }
}
