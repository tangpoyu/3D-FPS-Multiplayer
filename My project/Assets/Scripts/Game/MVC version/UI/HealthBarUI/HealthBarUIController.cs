using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIController : MonoBehaviour
{
    [SerializeField]
    private HealthBarUIModel healthBarUIModel;

    public void UpdataHealth(float health)
    {
        healthBarUIModel.HealthBar.fillAmount = health / 100;
    }
}
