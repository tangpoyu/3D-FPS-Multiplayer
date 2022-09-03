using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text usernameText, killText, deathText;
    private Player player;

    public ScoreboardItem Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;
        UpdateStated("kill");
        UpdateStated("death");
        return this;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer != player) return;
        
        if (changedProps.ContainsKey("kill"))
        {
            UpdateStated("kill");
        }
        
        if (changedProps.ContainsKey("death"))
        {
            UpdateStated("death");
        }
    }

    private void UpdateStated(string property)
    {
        switch (property)
        {
            case "kill":
                if (player.CustomProperties.TryGetValue("kill", out object kill))
                {
                    killText.text = kill.ToString();
                }
                else
                {
                    killText.text = "0";
                }
                break;

            case "death":
                if (player.CustomProperties.TryGetValue("death", out object death))
                {
                    deathText.text = death.ToString();
                }
                else
                {
                    deathText.text = "0";
                }
                break;
        }
    }
}
