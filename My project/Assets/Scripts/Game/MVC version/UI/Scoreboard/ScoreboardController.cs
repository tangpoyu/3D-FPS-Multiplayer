using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField]
    private ScoreboardModel scoreboardModel;

    internal void initialize()
    {
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            object kill;
            if (p.CustomProperties.ContainsKey("kill"))
            {
                p.CustomProperties.TryGetValue("kill", out kill);
            }
            else
            {
                kill = 0;
            }
            scoreboardModel.LeaderBoard.Add(p, (int) kill);
            // scoreboardModel.ScoreboardItems[p] = Instantiate(scoreboardModel.ScoreboardItemPrefab, scoreboardModel.Container);
        }
        SortLeaderBoard();
    }

    public void AddPlayerToScoreboard(Player player) 
    {
        //object kill;
        //if (player.CustomProperties.ContainsKey("kill"))
        //{
        //    player.CustomProperties.TryGetValue("kill", out kill);
        //}
        //else
        //{
        //    kill = 0;
        //}
        scoreboardModel.LeaderBoard.Add(player, 0);
        SortLeaderBoard();

        // scoreboardModel.ScoreboardItems.Add(player, Instantiate(scoreboardModel.ScoreboardItemPrefab, scoreboardModel.Container));
    }

    public void RemovePlayerFromScoreboard(Player otherPlayer)
    {
        Destroy(scoreboardModel.ScoreboardItems[otherPlayer].gameObject);
        scoreboardModel.ScoreboardItems.Remove(otherPlayer);
        scoreboardModel.LeaderBoard.Remove(otherPlayer);
    }

    public void SortLeaderBoard()
    {
        var LeaderBoard = scoreboardModel.LeaderBoard.ToList();
        LeaderBoard.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

        scoreboardModel.LeaderBoard.Clear();

        foreach(KeyValuePair<Player,int> keyValuePair in LeaderBoard)
        {
            scoreboardModel.LeaderBoard.Add(keyValuePair.Key, keyValuePair.Value);
        }

        foreach (Transform trom in scoreboardModel.Container)
        {
            // FIXPPB
            Destroy(trom.gameObject);
        }

        foreach (Player p in scoreboardModel.LeaderBoard.Keys)
        {
            scoreboardModel.ScoreboardItems[p] = Instantiate(scoreboardModel.ScoreboardItemPrefab, scoreboardModel.Container);
        }
    }

    public void UpdateLeaderBoard(Player player, object kill)
    {
        scoreboardModel.LeaderBoard[player] = (int) kill;
        SortLeaderBoard();
    }

    internal void Open()
    {
        scoreboardModel.CanvasGroup.alpha = 1;
    }

    internal void Close()
    {
        scoreboardModel.CanvasGroup.alpha = 0;
    }
}
