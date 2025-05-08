using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private bool canChasePlayer = true; // 플레이어 추적 여부
    private Transform playerTransform;

    private void Start()
    {
        if(canChasePlayer)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if(player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    private void Update()
    {
        // 플레이어 추적
        if(canChasePlayer && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);
        }
        else
        {
            // 기타 패턴 이동 로직(예: 좌우 반복 이동 등)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 충돌 시 데미지
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
        }
    }
}
