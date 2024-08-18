using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    private float current_Pos_x;
    private Vector3 veclocity = Vector3.zero;

    [SerializeField] private Transform Player;
    [SerializeField] private float ahead_Distance;
    [SerializeField] private float Camera_Speed;
    private float LookAhead;
    private void Update()
    {
        //room follow
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(current_Pos_x, transform.position.y, transform.position.z),ref veclocity, speed);

        //player follow
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        LookAhead = Mathf.Lerp(LookAhead, (ahead_Distance * Player.localScale.x), Time.deltaTime * Camera_Speed);
    }

    public void MoveTonewRoom(Transform _newRoom)
    {
        current_Pos_x = _newRoom.position.x;
    }
}
