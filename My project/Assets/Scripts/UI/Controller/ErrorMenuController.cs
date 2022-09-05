using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// controller
public class ErrorMenuController : MonoBehaviour
{
    [SerializeField] GameObject TitleMenu;
    [SerializeField] TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Laucher.instance.IsCreateRoomFailed) // service [ Laucher ]
        {
            errorText.text = Laucher.instance.CreateRoomFailedMessage; // service [ Laucher ]
        }
        else
        {
            this.gameObject.SetActive(false); // view [ ErrorMenu ] 
        }
    }

    public void Back()
    {
        Laucher.instance.IsJoinedLobby = true;
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
