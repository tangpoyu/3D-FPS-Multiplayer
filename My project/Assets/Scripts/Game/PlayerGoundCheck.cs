using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Service
public class PlayerGoundCheck : MonoBehaviour
{
    private PlayerMoveController playerController;

    private void Awake()
    {
        // playerController = GetComponentInParent<PlayerController>();
        playerController = GetComponent<PlayerMoveController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject == playerController.gameObject) return;
        
        if(playerController != null) playerController.Grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject == playerController.gameObject) return;
        if (playerController != null) playerController.Grounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject == playerController.gameObject) return;
        if (playerController != null) playerController.Grounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject == playerController.gameObject) return;
        if (playerController != null)  playerController.Grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject == playerController.gameObject) return;
        if (playerController != null) playerController.Grounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject == playerController.gameObject) return;
        if (playerController != null) playerController.Grounded = true;
    }
}
