using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerCombat : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerAudio playerAudio;
    private float parryTime = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask damageableLayer;
    public Animator PlayerAnimator;
    public float attackRate = 2.43f;
    float parrySpammingTime = 0;
    bool parrySpamming = false;
    float nextAttackTime = 0f;
    private PlayerInputActions playerInputActions;
    float ParryStartTime;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();


    }
    private void OnEnable()
    {
        playerInputActions.Player.Attack.Enable();
        playerInputActions.Player.Parry.started += parry;
        playerInputActions.Player.Parry.canceled += endparry;
        playerInputActions.Player.Parry.Enable();
    }



    private void OnDisable()
    {
        playerInputActions.Player.Attack.Disable();
        playerInputActions.Player.Parry.Disable();
    }
    void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        attackPoint = transform.GetChild(0).GetChild(0).GetComponentInChildren<Transform>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        PlayerAnimator.SetBool("Parry", playerStats.Parrying);
        PlayerAnimator.SetBool("LongParrying", playerStats.LongParrying);
        if (playerStats.isDashing|| DialogueManager.GetInstance().isPlaying || playerStats.isStaggered)
        {
            playerStats.Parrying = false;
            playerStats.LongParrying = false;
           
            return;
        }
    
        if (Time.time >= nextAttackTime)
        {
            if (playerInputActions.Player.Attack.WasPressedThisFrame())
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (parrySpamming)
        {
            playerStats.Parrying = false;
            playerStats.LongParrying = true;
   

        }
     
        if (playerStats.Parrying)
        {
            if (Time.time > ParryStartTime + parryTime)
            {
                playerStats.Parrying = false;
            }
        }
        //Debug.Log(playerStats.LongParrying );
    //    Debug.Log(playerStats.Parrying + " " + parryTime);
    }
    private IEnumerator StopParry(float time) {
        yield return new WaitForSeconds(time);
        parrySpamming = false;
        playerStats.LongParrying = false;
    
    }
    private IEnumerator Attack()
    {
        PlayerAnimator.SetTrigger("Attacking");
        PlayerAnimator.SetBool("Attack", true);
        yield return  new WaitForSeconds(0.2f);
        playerAudio.PlaySound("PlayerSwordSwing");
        
        Invoke("AttackReset", 0.25f);
        Collider2D[] HitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayer);
        foreach (Collider2D damageableObject in HitObjects)
        {
            damageableObject.GetComponent<Damageble>().TakeDamage(playerStats.damage);
            Vector2 direction = (transform.position - damageableObject.transform.position).normalized;
            damageableObject.GetComponent<Damageble>().TakeKnockback(-direction, 20);
           
        }

    }
    void AttackReset() {
        PlayerAnimator.SetBool("Attack", false);
    }
    void parry(InputAction.CallbackContext obj)
    {
        if (!(playerStats.isDashing || DialogueManager.GetInstance().isPlaying || playerStats.isStaggered))
        {
            if (obj.interaction is HoldInteraction)
            {
                playerStats.LongParrying = true;
                PlayerAnimator.SetBool("LongParrying", playerStats.LongParrying);
            }
            if (obj.interaction is TapInteraction)
            {


                if (playerStats.Parrying || parrySpammingTime + 1f > Time.time || ParryStartTime + 0.75f > Time.time)
                {
                    parrySpamming = true;
                    parrySpammingTime = Time.time;
                    parryTime -= 0.1f;
                    parryTime = parryTime < 0.1f ? 0.1f : parryTime;
                   ;
                    StartCoroutine(StopParry(parryTime));
                }
                else
                {
                    parryTime = 0.5f;
                    parrySpamming = false;
                }
                ParryStartTime = Time.time;
                PlayerAnimator.SetTrigger("Parrying");
            }
            playerStats.Parrying = true;
            ParryStartTime = Time.time;
        }
    }
    
    private void endparry(InputAction.CallbackContext obj)
    {
        playerStats.LongParrying = false;
        PlayerAnimator.SetBool("LongParrying", playerStats.LongParrying);
    }
    private void OnDrawGizmosSelected()
    {
        Debug.Log("Draw Gizmos");
       
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



}

