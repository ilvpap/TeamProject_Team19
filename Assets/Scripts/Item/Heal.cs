using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
  public override void ApplyEffect(PlayerController player)
    {
        //player.currentHP += value;                      //현재HP + value
        //if (player.currentHP > player.maxHP)            //현재HP가 최대HP보다 크면     예) 55 > 50
        //    player.currentHP = player.maxHP;            //현재HP는 최대HP와 같아진다       50 = 50
    }
}
