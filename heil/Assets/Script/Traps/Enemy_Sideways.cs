using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movement_Distance;


    private bool moving_left;
    private float left_edge;
    private float right_edge;
    private void Awake()
    {
        //if moving to left that movement distance will < 0 because vector x point to the right
        left_edge = transform.position.x - movement_Distance;
        right_edge = transform.position.x + movement_Distance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<heath>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        if(moving_left)
        {
            if(transform.position.x > left_edge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moving_left = false;
            }
        }
        else
        {
            if(transform.position.x < right_edge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moving_left = true;
            }
        }
    }
}
