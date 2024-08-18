using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sellection_arrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] opstions;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interact_Sound;
    private RectTransform rect;
    private int current_position;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            change_Position(-1);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            change_Position(1);
        }

        //interact
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            interact();
        }
    }
    private void change_Position(int _change)
    {
        current_position += _change;


        if(_change != 0)
        {
            Soundmanager.Instance.PlaySound(changeSound);
        }

        if(current_position < 0)
        {
            current_position = opstions.Length - 1;
        }
        else if(current_position > opstions.Length - 1)
        {
            current_position = 0;
        }

        rect.position = new Vector3(rect.position.x, opstions[current_position].position.y, 0);
    }

    private void interact()
    {
        Soundmanager.Instance.PlaySound(interact_Sound);

        //call button component on each options;
        opstions[current_position].GetComponent<Button>().onClick.Invoke();

    }
}
