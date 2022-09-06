using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardModel : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    [SerializeField] CanvasGroup canvasGroup;
    
    private Dictionary<Player, GameObject> scoreboardItems;
    private Dictionary<Player, int> leaderBoard;

    public Transform Container { get => container; set => container = value; }
    public GameObject ScoreboardItemPrefab { get => scoreboardItemPrefab; set => scoreboardItemPrefab = value; }
    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }
    public Dictionary<Player, GameObject> ScoreboardItems { get => scoreboardItems; set => scoreboardItems = value; }
    public Dictionary<Player, int> LeaderBoard { get => leaderBoard; set => leaderBoard = value; }

    // Start is called before the first frame update
    void Awake()
    {
        scoreboardItems = new Dictionary<Player, GameObject>();
        LeaderBoard = new Dictionary<Player, int>();
    }
}
