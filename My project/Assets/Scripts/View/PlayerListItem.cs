using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviour
{
    private TMP_Text text;
    private Player player;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetUp(Player player)
    {
        this.player = player;
        text.text = player.NickName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
