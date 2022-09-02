using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppView : MonoBehaviour
{
    private PlayerView playerView;

    public PlayerView PlayerView { get => playerView; set => playerView = value; }
}
