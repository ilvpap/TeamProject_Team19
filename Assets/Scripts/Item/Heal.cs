using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
  public override void ApplyEffect(Player player)
    {
        player.Heal(value);  // Item.cs에서 Value 값 가져옴
    }
}
