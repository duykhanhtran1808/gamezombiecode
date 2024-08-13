using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 5f;
    [SerializeField] Canvas imgDamage;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        imgDamage.enabled = false;
    }


    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        StartCoroutine(DisplayBlood());

    }
    IEnumerator DisplayBlood()
    {
        imgDamage.enabled = true;
        yield return new WaitForSeconds(0.3f);
        imgDamage.enabled = false;
    }

}
