using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardItemController : MonoBehaviour
{
    [SerializeField]
    private ScoreboardItemModel scoreboardItemModel;

    public ScoreboardItemModel ScoreboardItemModel { get => scoreboardItemModel; set => scoreboardItemModel = value; }

    public void Initialize()
    {
        foreach(KeyValuePair<Player,GameObject> keyValuePairs in GetComponentInParent<ScoreboardModel>().ScoreboardItems)
        {
            if(gameObject == keyValuePairs.Value)
            {
                ScoreboardItemModel.UsernameText.text = keyValuePairs.Key.NickName;
                ScoreboardItemModel.Player = keyValuePairs.Key;
                if (ScoreboardItemModel.Player.CustomProperties.TryGetValue("kill", out object kill))
                {
                    UpdateStated("kill", (int) kill);
                }
                else
                {
                    UpdateStated("kill", 0);
                }
                if(ScoreboardItemModel.Player.CustomProperties.TryGetValue("death", out object death))
                {
                    UpdateStated("death", (int) death);
                }
                else
                {
                    UpdateStated("death",0);
                }
               
            }
        }
    }

    public void UpdateStated(string property, int amount)
    {
        switch (property)
        {
            case "kill":
                ScoreboardItemModel.KillText.text = amount.ToString();
                break;

            case "death":
                ScoreboardItemModel.DeathText.text = amount.ToString();
                break;
        }
    }

   
}
