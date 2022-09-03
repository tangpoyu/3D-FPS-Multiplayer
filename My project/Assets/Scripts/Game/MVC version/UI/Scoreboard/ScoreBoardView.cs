using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ScoreBoardView : Photon.Pun.MonoBehaviourPunCallbacks
{
    [SerializeField]
    private ScoreboardController scoreboardController;

    private void Start()
    {
        scoreboardController.initialize();
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    scoreboardController.AddPlayerToScoreboard(player);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) scoreboardController.Open();
        if (Input.GetKeyUp(KeyCode.Tab)) scoreboardController.Close();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        scoreboardController.AddPlayerToScoreboard(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        scoreboardController.RemovePlayerFromScoreboard(otherPlayer);
    }
}
