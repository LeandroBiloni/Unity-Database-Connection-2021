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
			Debug.Log("count: " + www.downloadHandler.text.Length);
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

		string query = "SELECT Username FROM `accounts` WHERE Username = ('" + username + "') AND Password = ('" + password + "');";
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

	public void SendFriendRequest(string userA, string userB, Action<string> successCallback, Action<string> failureCallback)
	{
		if (userA == userB)
		{
			Debug.Log("Can't send a request to yourself");
			return;
		}

		WWWForm form = CreateForm();
		form.AddField("userA", userA);
		form.AddField("userB", userB);
		StartCoroutine(DoQuery("sendfriend", form, successCallback, failureCallback));
	}

	public void DeleteFriendRequest(string userA, string userB, Action<string> successCallback, Action<string> failureCallback)
	{
		WWWForm form = CreateForm();
		form.AddField("userA", userA);
		form.AddField("userB", userB);

		StartCoroutine(DoQuery("deletefriend", form, successCallback, failureCallback));
	}

	//Nico
	public void CheckRequests(string user, Action<string> successCallback, Action<string> failureCallback)
	{
		WWWForm form = CreateForm();

		string query = "SELECT user1 FROM `friendlist` WHERE user2 = '" + user + "' AND FriendStatus = 0";
		form.AddField("query", query);

		StartCoroutine(DoQuery("checkrequest", form, successCallback, failureCallback));
	}

	//Nico
	public void AcceptRequests(string userA, string userB, Action<string> successCallback, Action<string> failureCallback)
	{
		WWWForm form = CreateForm();
		string query = "UPDATE `friendlist` SET `FriendStatus` = 1 WHERE user1 = '" + userB + "' AND user2 = '" + userA + "' AND FriendStatus = 0";
		form.AddField("query", query);

		StartCoroutine(DoQuery("acceptrequest", form, successCallback, failureCallback));
	}

	public void CheckFriendList(string users, Action<string> successCallback, Action<string> failureCallback)
	{
		WWWForm form = CreateForm();
		form.AddField("users", users);
		StartCoroutine(DoQuery("checkfriendlist", form, successCallback, failureCallback));
	}
}
