using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerModel : AppElement
{


    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private PlayerController playerController;

    // player move data
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameObject cameraHolder;
    private Vector3 moveAmount;
    private float mouseSensitivity = 3, sprintSpeed = 6, walkSpeed = 3, jumpForce = 300, smoothTime = 0.15f;
    private float verticalLookRotation;
    private bool grounded, mouselocked;
    private Vector3 smoothMoveVelocit;

    // player game data
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private HealthBarUIController healthBarUIController;
    private float health = 100;
    private float currentHealth;
    private int kill, death;

    // weapon data
    [SerializeField]
    private GameObject decoratorUI;
    [SerializeField]
    private SingleShotGunController[] singleShotGunController; // the weapons of player
    private int currentitemIndex, previousItemIndex = -1;
    private bool canShoot = true;
    // private List<Color> bulletImpactColor;

    // Ghost Mode
    [SerializeField]
    private Camera ghostModeCamera;


    public PhotonView Pv { get => pv; set => pv = value; }

    public Rigidbody Rb { get => rb; set => rb = value; }
    public GameObject CameraHolder { get => cameraHolder; set => cameraHolder = value; }
    public Vector3 MoveAmount { get => moveAmount; set => moveAmount = value; }
    public float MouseSensitivity { get => mouseSensitivity; set => mouseSensitivity = value; }
    public float SprintSpeed { get => sprintSpeed; set => sprintSpeed = value; }
    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float SmoothTime { get => smoothTime; set => smoothTime = value; }
    public float VerticalLookRotation { get => verticalLookRotation; set => verticalLookRotation = value; }
    public bool Grounded { get => grounded; set => grounded = value; }
    public bool Mouselocked { get => mouselocked; set => mouselocked = value; }
    public Vector3 SmoothMoveVelocit { get => smoothMoveVelocit; set => smoothMoveVelocit = value; }


    public HealthBarUIController HealthBarUIController { get => healthBarUIController; set => healthBarUIController = value; }
    public float Health { get => health; set => health = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int Kill { get => kill; set => kill = value; }
    public int Death { get => death; set => death = value; }

    // TODO : check sync of death on all players when death ++.


    public SingleShotGunController[] SingleShotGunController { get => singleShotGunController; set => singleShotGunController = value; }
    public int CurrentitemIndex { get => currentitemIndex; set => currentitemIndex = value; }
    public int PreviousItemIndex { get => previousItemIndex; set => previousItemIndex = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    // public List<Color> BulletImpactColor { get => bulletImpactColor; set => bulletImpactColor = value; }

    public Camera GhostModeCamera { get => ghostModeCamera; set => ghostModeCamera = value; }
   

    private void Awake()
    {
        
        if (Pv.IsMine)
        {
           
            CurrentHealth = health;
            //playerController.EquipItem(0);
            //bulletImpactColor = new List<Color>();
            //foreach (Color color in PlayfabGameIntialData.instance.BulletImpactColor)
            //{
            //    BulletImpactColor.Add(color);
            //}
        }
        else
        {
            Rb.useGravity = false;
            Rb.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(CameraHolder);
            Destroy(healthBar);
            Destroy(decoratorUI);
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player == Pv.Owner)
            {
                if (player.CustomProperties.ContainsKey("kill"))
                {
                    kill = (int)player.CustomProperties["kill"];
                }

                if (player.CustomProperties.ContainsKey("death"))
                {
                    death = (int)player.CustomProperties["death"];
                }

                if (player.CustomProperties.ContainsKey("currentitemIndex"))
                {
                    currentitemIndex = (int)player.CustomProperties["currentitemIndex"];
                }
            }
        }
        playerController.initialItem();
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if ((targetPlayer == Pv.Owner) && !Pv.IsMine)
        {
            if (changedProps.ContainsKey("death"))
            {
                death = (int)changedProps["death"];
            }

            if (changedProps.ContainsKey("currentitemIndex"))
            {
                currentitemIndex = (int)changedProps["currentitemIndex"];
                playerController.EquipItem(currentitemIndex);
            }

            if (changedProps.ContainsKey("kill"))
            {
                kill = (int) changedProps["kill"];
            }
        }
    }
}
