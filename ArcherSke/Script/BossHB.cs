using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHB : MonoBehaviour
{
    public Slider bossslider;

    public void BossMaxHealth(int health)
    {
        bossslider.maxValue = health;
        bossslider.value = health;
    }

    public void BossSetHealth(int health)
    {
        bossslider.value = health;
    }
}
