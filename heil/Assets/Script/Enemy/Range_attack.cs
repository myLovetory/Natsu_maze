using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_attack : MonoBehaviour
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] blades;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("audio")]
    [SerializeField] private AudioClip enemy_blade;


    
    //References
    private Animator anim;
    
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown )
            {
                cooldownTimer = 0;
                anim.SetTrigger("RangeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        Soundmanager.Instance.PlaySound(enemy_blade);
        cooldownTimer = 0;
        blades[FindFireball()].transform.position = firepoint.position;
        blades[FindFireball()].GetComponent<Enemy_projectiles>().Active_projectiles();
    }
    private int FindFireball()
    {
        for (int i = 0; i < blades.Length; i++)
        {
            if (!blades[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    //tạo cái khung cù l*n 
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);



        return hit.collider != null;
    }
    //hiển thị cái khung cù l*n
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}


