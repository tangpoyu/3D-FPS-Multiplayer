using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ScoreboardItemView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private ScoreboardItemController scoreboardItemController;

    private void Start()
    {
        scoreboardItemController.Initialize();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer != scoreboardItemController.ScoreboardItemModel.Player) return;

        if (changedProps.TryGetValue("kill", out object kill))
        { 
            GetComponentInParent<ScoreboardController>().UpdateLeaderBoard(scoreboardItemController.ScoreboardItemModel.Player, kill);
            scoreboardItemController.UpdateStated("kill", (int) kill);
        }

        if (changedProps.TryGetValue("death", out object death))
        {
            scoreboardItemController.UpdateStated("death", (int) death);
        }
    }
}
