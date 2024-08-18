using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : Enemy_Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float check_Delay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float check_timer;
    private bool attacking;


    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            check_timer += Time.deltaTime;
            if (check_timer > check_Delay)
            {
                checkforPlayer();
            }
        }
    }

    private void checkforPlayer()
    {
        Calculate_Direction();

        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            
            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                check_timer = 0;
            }
        }
    }
    private void Calculate_Direction()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
   
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //
        Stop();
    }
}
