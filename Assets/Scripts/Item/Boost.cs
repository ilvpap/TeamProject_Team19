using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Item
{     
    public override void Activate(Player player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null )
        {
            player.StartCoroutine(BoostRoutine(playerController));
        }
    }

    private IEnumerator BoostRoutine(PlayerController playerController)
    {
        float timer = value;
        while (timer > 0)
        {
            playerController.isBoosted = true;
            Debug.Log("속도 업 효과 진행중... 남은시간: " +((int)timer).ToString("D2"));
            timer -= Time.deltaTime;
            yield return null;
        }
        playerController.isBoosted = false;
        Debug.Log("속도 업 종료");
    }
}
