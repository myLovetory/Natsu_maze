using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heath_collectable : MonoBehaviour
{
    [SerializeField] private float heath_value;
    [SerializeField] private AudioClip hearth_collectable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Soundmanager.Instance.PlaySound(hearth_collectable);
            collision.GetComponent<heath>().Add_Heart(heath_value);
            gameObject.SetActive(false);
        }

    }


}
