using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
    public override void Activate(Player player)
    {
        player.Magnet(value); // Item.cs에서 Value 값 가져옴
    }
}
