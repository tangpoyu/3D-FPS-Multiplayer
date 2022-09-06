using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DecoratorUIModel : MonoBehaviour
{
    private bool isOpen;
    [SerializeField]
    private DecoratorUIController decoratorUIController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Transform buttonSpawnPoint;

    [SerializeField]
    private GameObject button;

    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }
    public Transform Colors { get => buttonSpawnPoint; set => buttonSpawnPoint = value; }
    public GameObject Button { get => button; set => button = value; }

    private void Start()
    {
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        foreach (var x in playerController.PlayerModel.BulletImpactColor.Select((value, index) => new { value, index }))
        {
            GameObject obj = Instantiate(button);
            obj.GetComponent<Image>().color = x.value;
            obj.transform.SetParent(buttonSpawnPoint, false);
            obj.GetComponent<bulletImapctButtonModel>().DecoratorUI = decoratorUIController;
        }
    }
}
