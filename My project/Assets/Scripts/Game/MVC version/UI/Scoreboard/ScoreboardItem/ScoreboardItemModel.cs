using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardItemModel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text usernameText, killText, deathText;
    private Player player;

    public TMP_Text UsernameText { get => usernameText; set => usernameText = value; }
    public TMP_Text KillText { get => killText; set => killText = value; }
    public TMP_Text DeathText { get => deathText; set => deathText = value; }
    public Player Player { get => player; set => player = value; }
}
