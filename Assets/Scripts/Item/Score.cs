using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : Item
{
    public override void ApplyEffect(Player player)
    {
        player.Score(value); // Item.cs에서 Value 값 가져옴
    }
}
