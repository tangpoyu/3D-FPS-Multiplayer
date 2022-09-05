using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddFriend : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    public static Action<string> OnAddFriend = delegate { };

    public void AddFriendName()
    {
        if (string.IsNullOrEmpty(inputField.text)) return;
        OnAddFriend?.Invoke(inputField.text);
    }
}
