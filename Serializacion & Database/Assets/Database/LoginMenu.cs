using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginMenu : MonoBehaviour
{
    public Button loginButton, registerButton;
    public TMP_InputField usernameField, passwordField;

    DBAdmin _db;

    // Start is called before the first frame update
    void Start()
    {
        _db = FindObjectOfType<DBAdmin>();
    }

    public void RegisterBTN()
    {
        _db.Register(usernameField.text, passwordField.text);
    }

    public void LoginBTN()
    {
        _db.Login(usernameField.text, passwordField.text, OnLoginSucceed, OnLoginFail);
    }

    void OnLoginSucceed(string str)
    {
        Debug.Log(str);

        FindObjectOfType<Account>().AccountLogged(usernameField.text);

        usernameField.text = "";
        passwordField.text = "";
    }

    void OnLoginFail(string str)
    {
        Debug.Log(str);

        passwordField.text = "";
    }

    public void VerifyInputs()
    {
        bool canInteract = (usernameField.text.Length >= 4) && (passwordField.text.Length >= 4);

        loginButton.interactable = registerButton.interactable = canInteract;
    }
}
