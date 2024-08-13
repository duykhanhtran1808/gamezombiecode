using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;
    public bool IsDead {  get { return isDead; } }
    public void TakeDamage(float damage)
    {
        //EnemyAI enemyAI = GetComponent<EnemyAI>();
        //enemyAI.ProvokeEnemy();
        BroadcastMessage("ProvokeEnemy");
        this.hitPoints -= damage;
        if(this.hitPoints <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        Destroy(this.gameObject, 5f);
    }
}
