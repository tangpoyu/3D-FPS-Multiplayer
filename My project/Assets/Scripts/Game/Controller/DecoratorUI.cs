using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorUI : MonoBehaviour
{
    [SerializeField]
    private Gun riffle, pistol;
    private bool isOpen;
    [SerializeField]
    private PlayerMoveController playerMoveController;
    [SerializeField]
    private WeaponController weaponController;
    [SerializeField]
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            canvasGroup.alpha = (isOpen) ? 1 : 0;
            canvasGroup.blocksRaycasts = (isOpen) ? true : false;
            playerMoveController.Mouselocked = (isOpen) ? false : true;
            weaponController.CanShoot = (isOpen) ? false : true;
        }
    }

    public void changebulletImapctPrefab_pink()
    {
        print("change pink");
        changebulletImapctPrefab("pink");
    }

    public void changebulletImapctPrefab_orange()
    {
        print("change orange");
        changebulletImapctPrefab("orange");
    }

    public void changebulletImapctPrefab_yellow()
    {
        print("change yellow");
        changebulletImapctPrefab("yellow");
    }

    public void changebulletImapctPrefab_green()
    {
        print("change green");
        changebulletImapctPrefab("green");
    }

    public void changebulletImapctPrefab_bule()
    {
        print("change blue");
        changebulletImapctPrefab("blue");
    }

    public void changebulletImapctPrefab_purple()
    {
        print("change purple");
        changebulletImapctPrefab("purple");
    }

    private void changebulletImapctPrefab(string color)
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        riffle.changeBulletImapct(color);
        pistol.changeBulletImapct(color);
        playerMoveController.Mouselocked = true;
        weaponController.CanShoot = true;
    }
}
