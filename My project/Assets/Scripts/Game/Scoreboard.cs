using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    [SerializeField] CanvasGroup canvasGroup;
    private Dictionary<Player, ScoreboardItem> scoreboardItems;

    private void Awake()
    {
        print("1");
    }

    private void Start()
    {
        scoreboardItems = new Dictionary<Player, ScoreboardItem>();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddPlayerToScoreboard(player);
        }
    }

    public void AddPlayerToScoreboard(Player player)
    {
        scoreboardItems.Add(player, Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>().Initialize(player));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToScoreboard(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemovePlayerFromScoreboard(otherPlayer);
    }

    private void RemovePlayerFromScoreboard(Player otherPlayer)
    {
        Destroy(scoreboardItems[otherPlayer].gameObject);
        scoreboardItems.Remove(otherPlayer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasGroup.alpha = 1;
        }else if (Input.GetKeyUp(KeyCode.Tab))
        {
            canvasGroup.alpha = 0;
        }
    }
}
