using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class endpoint : MonoBehaviour
{
    private UImanager manager;


    [SerializeField] private Behaviour[] components;

    private void Start()
    {
        manager = FindObjectOfType<UImanager>();  // Tìm đối tượng UImanager trong cảnh
        if (manager == null)
        {
            Debug.LogError("UImanager không được tìm thấy trong cảnh.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("endpoint"))
        {
            if (manager != null && components != null)
            {
                manager.completed_level();
                foreach (Behaviour component in components)
                {
                    if (component != null)
                    {
                        component.enabled = false;
                    }
                }

            }
            return;
        }
    }


}