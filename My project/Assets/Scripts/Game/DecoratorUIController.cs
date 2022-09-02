using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// controller
public class DecoratorUIController : MonoBehaviour
{
    private bool isOpen;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]                                                                                
    private Transform Colors;                                                                      


    [SerializeField]
    private GameObject button;

    // TODO : access the color of bulletImpact which player owns instead of producing in this class.//
    private List<Color> bulletImpactColor;                                                         //
    ////////////////////////////////////////////////////////////////////////////////////////////////
    
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

        // TODO : access the color of bulletImpact which player owns instead of producing in this class.////
        bulletImpactColor.Add(new Color(255/255f, 0, 91/255f, 99/255f));                                 //
        bulletImpactColor.Add(new Color(255 / 255f, 100 / 255f, 0, 134 / 255f));                        //
        bulletImpactColor.Add(new Color(255 / 255f, 222 / 255f, 0, 132 / 255f));                       //
        bulletImpactColor.Add(new Color(161 / 255f, 4 / 255f, 255 / 255f, 107 / 255f));               //
        bulletImpactColor.Add(new Color(75/255f, 245 / 255f, 0, 128 / 255f));                        //
        bulletImpactColor.Add(new Color(0, 245 / 255f, 212 / 255f, 107 / 255f));                    //
        /////////////////////////////////////////////////////////////////////////////////////////////
      

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
            playerController.UIOpenOrClose(isOpen);
        }
    }

    public void changebulletImapctPrefab(Color color)
    {
        isOpen = false;
        playerController.UIOpenOrClose(isOpen);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        foreach(SingleShotGunController singleShotGunController in playerController.PlayerModel.SingleShotGunController)
        {
            singleShotGunController.changeBulletImapct(color, singleShotGunController.gunModel.itemName);
        }
        //riffle.changeBulletImapct(color, "RIFFLE");
        //pistol.changeBulletImapct(color, "PISTOL");
    }
}
