using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Transform Colors;
    [SerializeField]
    private GameObject button;
    private List<Color> bulletImpactColor;

    private void Awake()
    {
        bulletImpactColor = new List<Color>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        bulletImpactColor.Add(new Color(255/255f, 0, 91/255f, 99/255f));
        bulletImpactColor.Add(new Color(255 / 255f, 100 / 255f, 0, 134 / 255f));
        bulletImpactColor.Add(new Color(255 / 255f, 222 / 255f, 0, 132 / 255f));
        bulletImpactColor.Add(new Color(161 / 255f, 4 / 255f, 255 / 255f, 107 / 255f));
        bulletImpactColor.Add(new Color(75/255f, 245 / 255f, 0, 128 / 255f));
        bulletImpactColor.Add(new Color(0, 245 / 255f, 212 / 255f, 107 / 255f));
      

        foreach (var x in bulletImpactColor.Select((value, index) => new {value,index}))
        {
           GameObject obj = Instantiate(button);
           obj.GetComponent<Image>().color = x.value;
           obj.transform.SetParent(Colors, false);
        }
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

    public void changebulletImapctPrefab(Color color)
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        riffle.changeBulletImapct(color, "Riffle");
        pistol.changeBulletImapct(color, "Pistol");
        playerMoveController.Mouselocked = true;
        weaponController.CanShoot = true;
    }
}
