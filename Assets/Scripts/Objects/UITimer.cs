using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private Image clockFilled;

    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color burnColor = Color.red;

    public void UpdateClock(float amount, float maxValue, bool isBurning = false)
    {
        clockFilled.fillAmount = amount / maxValue;

        if (isBurning)
            clockFilled.color = burnColor;
        else
            clockFilled.color = normalColor;
    }
}