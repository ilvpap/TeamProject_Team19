using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnet : Item
{

    public float magnetRange = 5f; //자석 범위
    
    public override void Activate(Player player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();  //player에서 PlayerController 가져옴
        if(playerController != null)            //playerController이 있다면
        {
            Debug.Log("자석 아이템 작동 시작!");
            playerController.StartCoroutine(MagnetRoutine(playerController));   //자석 코루틴 시작
        }                                                                           
    }

    private IEnumerator MagnetRoutine(PlayerController playerController)
    {
        
        playerController.isMagnetActive = true;             //자석 효과 시작
        
        float timer = value;                                // 지속시간
        while (timer > 0)                                   //지속시간이 0보다 크면실행
        {
            Debug.Log("자석 효과 진행 중... 남은 시간: " + ((int)timer).ToString("D2"));  //남은시간 디버그
            AttractItems(playerController);                 //아이템을 끌어당기는거 호출
            timer -= Time.deltaTime;                        //시간 감소
            yield return null;                              //대기
        }
        playerController.isMagnetActive = false;            //자석 효과 종료
        Debug.Log("자석 효과 종료됨");                     // 자석 효과 종료 디버그
    }

    private void AttractItems(PlayerController playerController)
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(
            playerController.transform.position,                // 원형범위안에 오브젝트 찾기
            magnetRange);
        foreach (var item in items)
        {
            if (item.CompareTag("Item"))    //태그가 Item이면
            {
                //아이템에서 플레이어로 방향을 계산
                Vector2 direction = (playerController.transform.position - item.transform.position).normalized;
                // 아이템 Rigidbody2D에 힘을 줌
                item.GetComponent<Rigidbody2D>().AddForce(direction * 5f);
                

            }
        }
    }
}
