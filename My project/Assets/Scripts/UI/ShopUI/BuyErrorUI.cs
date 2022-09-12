using TMPro;
using UnityEngine;

public class BuyErrorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text errorMessage;

    public TMP_Text ErrorMessage { get => errorMessage; set => errorMessage = value; }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
