using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private Camera_Controller cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<Camera_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveTonewRoom(nextRoom);
                nextRoom.GetComponent<Rooms>().Activated_Room(true);
                previousRoom.GetComponent<Rooms>().Activated_Room(false);
            }
            else
            {
                cam.MoveTonewRoom(previousRoom);
                previousRoom.GetComponent<Rooms>().Activated_Room(true);
                nextRoom.GetComponent<Rooms>().Activated_Room(false);
            }
        }
    }
}
