using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private float lifetimes;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit == true) return;
        float movementSpeed = speed * Time.deltaTime *direction;
        transform.Translate(movementSpeed, 0, 0);
        lifetimes += Time.deltaTime;
        if(lifetimes > 3) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explore");

        if(collision.tag == "enemy")
        {
            collision.GetComponent<heath>()?.TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetimes = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
