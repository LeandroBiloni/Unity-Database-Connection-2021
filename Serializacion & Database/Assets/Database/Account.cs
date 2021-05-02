using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Account : MonoBehaviour
{
    public GameObject registerScreen, loggedScreen, friendWindow;

    public TMP_Text welcomeText, checkRequestText, friendListText;


    public TMP_InputField scoreField;

    public TMP_InputField friendToAddField, friendToAcceptField;

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

		CheckFriendListBT();

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
        friendWindow.SetActive(true);
    }

    public void CloseFriendlistBTN()
    {
        friendWindow.SetActive(false);
    }

    public void SendFriendRequestBTN()
    {
        _db.SendFriendRequest(_username, friendToAddField.text, SendFriendRequestSucceed, SendFriendRequestFailed);
    }

	void SendFriendRequestSucceed(string res)
	{
		Debug.Log("Friend request sent");
	}

	void SendFriendRequestFailed(string res)
	{
		Debug.Log("Friend request failed");
		Debug.Log(res);
	}

	public void CheckRequestBTN()
	{
		_db.CheckRequests(_username, CheckRequestSucceed, CheckRequestFailed);
	}


	void CheckRequestSucceed(string res)
	{
		Debug.Log("Friend Requests: " + res);

		string[] rows = res.Split('\n');

		
		checkRequestText.text = "";

		for (int i = 1; i < rows.Length; i++)
		{
			checkRequestText.text += rows[i] + "\n";
		}
	}

	void CheckRequestFailed(string res)
	{
		Debug.Log("No requests");
		checkRequestText.text = "No requests";
	}

	public void CheckFriendListBT()
	{
		_db.CheckFriendList(_username, CheckFriendListSuccess, CheckFriendListFailed);
	}

	void CheckFriendListSuccess(string res)
	{
		Debug.Log("Friend Requests: " + res);

		string[] rows = res.Split('\n');


		friendListText.text = "";

		for (int i = 1; i < rows.Length; i++)
		{
			friendListText.text += rows[i] + "\n";
		}
	}

	void CheckFriendListFailed(string res)
	{
		Debug.Log(res);
		friendListText.text = res;
	}

	public void AcceptFriendRequestBTN()
	{
		_db.AcceptRequests(_username, friendToAcceptField.text, AcceptFriendRequestSucces, AcceptFriendRequestFailed);
	}

	private void AcceptFriendRequestSucces(string res)
	{
		Debug.Log("Friend Accepted");
		CheckFriendListBT();
		CheckRequestBTN();
	}

	private void AcceptFriendRequestFailed(string res)
	{
		Debug.Log(res);
	}

	public void DeleteFriendRequestBTN()
	{
		_db.DeleteFriendRequest(_username, friendToAddField.text, DeleteFriendRequestSucces, DeleteFriendRequestFailed);
	}

	private void DeleteFriendRequestSucces(string res)
	{
		Debug.Log("Friend Deleted");
		CheckRequestBTN();
		CheckFriendListBT();
	}

	private void DeleteFriendRequestFailed(string res)
	{
		Debug.Log("No tenes amigos porque los mataste");
	}
}
