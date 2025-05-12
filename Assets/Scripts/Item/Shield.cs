using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public override void Activate(Player player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            player.StartCoroutine(ShieldRoutine(playerController));           //코루틴 시작
        }
    }

    private IEnumerator ShieldRoutine(PlayerController playerController)
    {
        float timer = value;
        while (timer > 0)
        {
            playerController.isShielded = true; // 무적 상태로 설정
            Debug.Log("무적 효과 진행 중... 남은 시간: " + ((int)timer).ToString("D2"));
            timer -= Time.deltaTime;
            yield return null;
        }
        playerController.isShielded = false; // 무적 해제
        Debug.Log("무적 종료");
    }
}
