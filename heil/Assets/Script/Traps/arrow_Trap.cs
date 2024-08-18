using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_Trap : MonoBehaviour
{
    [SerializeField] private float attack_cool_down;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Arrow;
    [SerializeField] private AudioClip arrow_trap;
    private float coolDown_timer;
    private void Attack()
    {
        coolDown_timer = 0;

        Soundmanager.Instance.PlaySound(arrow_trap);
        Arrow[FindArrow()].transform.position = firePoint.position;
        Arrow[FindArrow()].GetComponent<Enemy_projectiles>().Active_projectiles();
    }
    private int FindArrow()
    {
        for (int i = 0; i < Arrow.Length; i++) 
        {
            if (!Arrow[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        coolDown_timer += Time.deltaTime;

        if (coolDown_timer >= attack_cool_down)
            Attack();
    }
}
