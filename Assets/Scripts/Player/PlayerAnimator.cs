using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;
    PlayerStats playerStats;
    float PlayerX;
    float PlayerY;
    bool Dashing;
    bool Grounded;
   public bool Parrying;


    // Start is called before the first frame update
    void Start()
    {

        playerStats = GetComponent<PlayerStats>();
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        updateAnimations(); 
    }
    public void ReadInMovement(float pX, float pY, bool Dashing, bool Grounded) {
        PlayerX = pX;
        PlayerY = pY;
        this.Dashing = Dashing;
        this.Grounded = Grounded;
    }
    void updateAnimations() {
        playerAnimator.SetFloat("PlayerX", PlayerX);
        playerAnimator.SetFloat("PlayerY", PlayerY);
        playerAnimator.SetBool("Dashing", Dashing);
        playerAnimator.SetBool("Grounded", Grounded);
        
    }
}
