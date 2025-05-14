using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb; //통일 위해서
    private CapsuleCollider2D capsuleCollider;

    public Animator animator;

    private bool IsGround = false;
    private bool IsSliding = false;

    public bool isMagnetActive = false;
    public bool isShielded = false;
    public bool isBoosted = false;

    private int jumpCnt = 0;
    private int maxJumpCnt = 2;

    private Vector2 originColliderSize;
    private Vector2 originColliderOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        originColliderSize = capsuleCollider.size;
        originColliderOffset = capsuleCollider.offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpCnt = 0;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
            animator.SetBool("IsGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = false;
            animator.SetBool("IsGround", false);
        }
    }

    public void onJumpButtonClicked()
    {
        animator.SetTrigger("IsJumping");
    }

    public void Jump()
    {
        if (maxJumpCnt <= jumpCnt)
        {
            return;
        }
        onJumpButtonClicked();
        rb.velocity = new Vector2(rb.velocity.x, 7f); //rb.velocity의 x값을 되돌려주겠다
        //rb.AddForce(new Vector2(0, 350)); //AddForce = 날리는 힘, Y값으로 날려버린다
        jumpCnt++;
    }
    public void Slide(bool isSliding)
    {
        if (isSliding)
        {
            capsuleCollider.size = new Vector2(originColliderSize.x, originColliderSize.y / 2);
            capsuleCollider.offset = new Vector2(originColliderOffset.x, originColliderOffset.y - (capsuleCollider.size.y / 2)); //중심점을 내리는게 아님
        }
        else
        {
            capsuleCollider.size = originColliderSize;
            capsuleCollider.offset = originColliderOffset;
        }

        animator.SetBool("IsSliding", isSliding);
        
    }
}
