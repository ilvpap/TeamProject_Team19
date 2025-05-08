using UnityEngine;

public enum ItemType
{
    HealthRecovery,
    ScoreBonus,
    SpeedChange
}

public class Item : MonoBehaviour
{
    [Header("아이템 설정")]
    public ItemType itemType;
    [SerializeField] private float value = 20f; // 회복량 / 점수 증가량 / 속도 변경 값

    public void ApplyEffect()
    {
        switch(itemType)
        {
            case ItemType.HealthRecovery:
                // 체력 회복
                GameManager.Instance.RecoverHealth(value);
                break;
            case ItemType.ScoreBonus:
                // 점수 증가
                GameManager.Instance.AddScore(value);
                break;
            case ItemType.SpeedChange:
                // 플레이어 속도 변경
                PlayerController player = FindObjectOfType<PlayerController>();
                if(player != null)
                {
                    // 예: 일정 시간 동안 moveSpeed 변경 가능
                    // 여기서는 즉시 반영(가산) 예시
                    // 실제 게임에선 코루틴으로 일정 시간 후 원복 처리 등을 구현 가능
                    player.gameObject.transform.Translate(Vector2.right * value * Time.deltaTime);
                }
                break;
        }

        // 아이템은 사용 후 제거
        Destroy(gameObject);
    }
}
