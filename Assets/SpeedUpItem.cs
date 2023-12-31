using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    public float speedAmount = 10.0f;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.IncreaseSpeed(speedAmount);
            Destroy(gameObject); // アイテムを削除する
        }
    }
}
