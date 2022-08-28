using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PhotonView pv;
    private const float health = 100;
    private float currentHealth;
    private PlayerManager playerManage;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        playerManage = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<PlayerManager>();
        currentHealth = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        pv.RPC(nameof(RPC_TakeDamage), pv.Owner, damage);
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
            PlayerManager.Find(info.Sender).GetKill();
        }
        UIController.instance.UpdataHealth(currentHealth > 0 ? currentHealth : 0);
    }

    private void Die()
    {
        playerManage.Die();
    }
}
