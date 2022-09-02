using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppModel : MonoBehaviour
{
    private PlayerModel playerModel;

    public PlayerModel PlayerModel { get => playerModel; set => playerModel = value; }
}
