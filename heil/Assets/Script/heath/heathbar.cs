using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heathbar : MonoBehaviour
{
    [SerializeField] private heath player_heath;
    [SerializeField] private Image total_heathbar;
    [SerializeField] private Image current_heathbar;

    private void Start()
    {
        total_heathbar.fillAmount = player_heath.current_Heath / 10;
    }

    private void Update()
    {
        current_heathbar.fillAmount = player_heath.current_Heath / 10;

    }
}
