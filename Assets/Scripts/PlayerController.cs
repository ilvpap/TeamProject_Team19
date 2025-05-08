// Assets\Scripts\PlayerController.cs

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;     // 자동 이동 속도
    [SerializeField] private float jumpForce = 5f;    // 점프 힘
    [SerializeField] private float slideDuration = 0.8f;  // 슬라이드(대쉬) 유지 시간
    [SerializeField] private float slideCooldown = 1.5f;  // 슬라이드(대쉬) 후 재사용 대기
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode slideKey = KeyCode.S;

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 10f; // 앞으로 빠르게 밀어줄 힘(Impulse로 사용할 값)

    [Header("Enemy Overlap Damage")]
    [SerializeField] private float enemyDamageInterval = 1f; // 적과 겹칠 때, 몇 초마다 데미지를 줄지
    private float enemyDamageTimer = 0f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private float slideCooldownTimer = 0f;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!canMove) return;

        // 자동 전진
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // 점프
        if(Input.GetKeyDown(jumpKey) && !isJumping && !isSliding)
        {
            Jump();
        }

        // 슬라이드(대쉬)
        if(Input.GetKeyDown(slideKey) && !isSliding && slideCooldownTimer <= 0f && !isJumping)
        {
            Slide();
        }

        // 슬라이드(대쉬) 중 시간 체크
        if(isSliding)
        {
            slideTimer -= Time.deltaTime;
            if(slideTimer <= 0f)
            {
                EndSlide();
            }
        }
        else
        {
            // 슬라이드(대쉬) 쿨타임
            if(slideCooldownTimer > 0f)
            {
                slideCooldownTimer -= Time.deltaTime;
            }
        }

        // 적과 겹쳐 있을 때 데미지를 주는 간격 타이머
        if(enemyDamageTimer > 0f)
        {
            enemyDamageTimer -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    private void Slide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        slideCooldownTimer = slideCooldown;

        // 앞으로 빠르게 대쉬 (Impulse로 오른쪽 방향에 강한 힘을 가함)
        rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);

        // 슬라이드(대쉬) 시 콜라이더 크기/위치 등을 조정하거나
        // 애니메이션 트리거를 걸어주는 등의 로직을 추가할 수 있음
    }

    private void EndSlide()
    {
        isSliding = false;
        // 슬라이드(대쉬) 종료 후 처리
        // (콜라이더 원상복귀, 애니메이션 복귀 등 필요한 로직 추가 가능)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 점프 중 착지 판정 (바닥 레이어 체크 등)
        if(collision.contacts[0].normal.y > 0.5f)
        {
            isJumping = false;
        }
    }

    /// <summary>
    /// 적, 장애물 등 트리거 충돌 진입 시
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 장애물 또는 적 태그 확인
        if(collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            GameManager.Instance.TakeDamage();
        }

        // 아이템 태그 확인
        if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                item.ApplyEffect(); // 아이템 효과 발동
            }
        }
    }

    /// <summary>
    /// 적과 계속 겹쳐있는 동안 매 프레임 호출
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            // enemyDamageTimer가 0 이하일 때 데미지를 주고, 다시 간격을 세팅
            if(enemyDamageTimer <= 0f)
            {
                GameManager.Instance.TakeDamage();
                enemyDamageTimer = enemyDamageInterval;
            }
        }
    }

    /// <summary>
    /// 게임 오버 시 이동 정지
    /// </summary>
    public void StopMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
    }
}
