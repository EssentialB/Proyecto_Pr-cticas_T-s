using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabelleroController : MonoBehaviour
{
   public float velocity = 20;
    public float velocity2 = 50; 
    public float fuerzaSalto = 25;
    

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    bool puedeSaltar = true;
    const int ANIMATION_IDLE = 0;
    const int ANIMATION_WALK = 1;
    const int ANIMATION_RUN = 2;
    const int ANIMATION_JUMP = 3;
    const int ANIMATION_ATACK = 4;

    private Vector3 lastCheckPosition;

    void Start()
    {       
        rb = GetComponent<Rigidbody2D>(); 
        sr = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y); 
        ChangeAnimation(ANIMATION_IDLE);

        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false; 
            ChangeAnimation(ANIMATION_WALK);
        }
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y); 
            sr.flipX = true;
            ChangeAnimation(ANIMATION_WALK); 
        } 
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(velocity2, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_RUN);
        }  
        if(Input.GetKey(KeyCode.LeftArrow ) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(-velocity2, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_RUN);
        } 
        if(Input.GetKeyUp(KeyCode.Space) && puedeSaltar){
            ChangeAnimation(ANIMATION_JUMP);
            Saltar();
            puedeSaltar = false;
        }
        if(Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACK);
        }
         
    }

    private void Saltar(){
        rb.AddForce(new Vector2(0,fuerzaSalto), ForceMode2D.Impulse);
    }
    
    void OnCollisionEnter2D(Collision2D other){        
        puedeSaltar = true;
         if(other.gameObject.name == "Muerte"){
            if(lastCheckPosition != null){
                transform.position = lastCheckPosition;
            }
        }
    } 
     void OnTriggerEnter2D(Collider2D other){
        lastCheckPosition = transform.position;

    }
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);
    }
}
