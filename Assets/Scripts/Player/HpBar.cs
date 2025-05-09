using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] Player player;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        slider.value = player.Stat.HpRatio;
    }
}
