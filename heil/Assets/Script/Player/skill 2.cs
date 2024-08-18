using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill2 : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement PlayerMovement;

    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("audio")]
    [SerializeField] private AudioClip fireball;

    private float cooldownTime = Mathf.Infinity;
    private bool isAttacking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isAttacking && cooldownTime > attackcooldown && PlayerMovement.canAttack())
        {
            SKill2();
        }

        cooldownTime += Time.deltaTime;
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    public void SKill2()
    {
        Soundmanager.Instance.PlaySound(fireball);
        anim.SetTrigger("attack2");
        cooldownTime = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
