using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIModel : MonoBehaviour
{
    [SerializeField] Image healthBar;

    public Image HealthBar { get => healthBar; set => healthBar = value; }
}
