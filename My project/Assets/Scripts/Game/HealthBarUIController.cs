using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIController : MonoBehaviour
{
    [SerializeField] Image healthBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdataHealth(float health)
    {
        healthBar.fillAmount = health / 100;
    }
}
