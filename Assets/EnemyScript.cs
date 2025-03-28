using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    public event Action OnEnemyDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
            
        }//end if
    }// end ObnTriggerEnter

}// End enemy
