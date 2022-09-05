
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;

public class Playfab : MonoBehaviour
{
    private string username;
   [SerializeField] TMP_InputField usernameInputfield;
   [SerializeField] TMP_Text hint;


    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "17D60";
        }
    }

    public void SetUsername(string name)
    {
        usernameInputfield.interactable = false;
        username = name;
        PlayerPrefs.SetString("USERNAME", name);
        Login();
    }

    public void Login()
    {
        if (!IsValidUsername())
        {
            usernameInputfield.interactable = true;
            hint.text = "username Length must larger than 2 characters and  less than 25 characters";
            return;
        }
        LoginWithCustomId();
    }

    private bool IsValidUsername()
    {
        if (username.Length >= 3 && username.Length <= 24) return true;
        else return false;
    }
    private void LoginWithCustomId()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = username,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
    }
    private void OnLoginCustomIdSuccess(LoginResult obj)
    {
        Debug.Log("Logged into Playfab");
        UpdateDisplayName(username);
    }

    public void UpdateDisplayName(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult obj)
    {
        Debug.Log("Updated the displayname of the playfab");
        Laucher.instance.conntect();
    }

   
    private void OnFailure(PlayFabError obj)
    {
        usernameInputfield.interactable = true;
        hint.text = "Login Error: " + obj.GenerateErrorReport();
    }


}
