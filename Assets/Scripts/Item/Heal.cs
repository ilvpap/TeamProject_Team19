using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
 public override void ApplyEffect(PlayerStats player)
    {
        
        player.curHp += value;                      //현재HP + value
        if (player.curHp > player.maxHp)            //현재HP가 최대HP보다 크면     예) 55 > 50
            player.curHp = player.maxHp;            //현재HP는 최대HP와 같아진다       50 = 50

        
    }
}
