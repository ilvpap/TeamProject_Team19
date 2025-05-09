using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Item
{
    public override void Activate(Player player)
    {
        player.Boost(value); // Item.cs에서 Value 값 가져옴
    }
}
