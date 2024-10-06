using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Timeline;

public class IceSkater : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration=1f;
    public float backwardAcceleration=1f;
    public float maxVelocity=10f;
    [Tooltip("Deceleration always takes effect")]
    public float deceleration=5f;

    [Tooltip("Instant speed gain at the end of a stroke")]
    public float boostSpeed=1f;
    [Tooltip("Swimmer maintains coasting speed when not boosting")]
    public float coastingSpeed=3f;  //maintains coasting speed when not boosting

    [Tooltip("Timer for before boost takes effect")]
    public float boostTimer=0f;
    [Tooltip("Total time it takes before boost takes effect")]
    public float boostTime=1f;     //time before boost takes effect

    private CharacterController controller;
    //private Rigidbody body;
    private PlayerInput playerInput;

    [Header("Rotation")]
    public float rotationAcceleration=180f;
    public float rotationMaxVelocity=180f;
    private Vector3 rotationVelocity=Vector3.zero;
    public float rotationDeceleration=180f;
    public float maxRotationXAngle=60f;

    public float maxTiltAngle=10f; //max angle to tilt when moving laterally
    public float angleTiltSpeed=1f; //speed to tile when moving laterally

    private Animator animator;

    public float skewingForce=1f;
    public float maxSkewingVelocity=5f;

    [Header("Vertical Movement")]
    public float jumpForce=5f;
    public float gravity=1f;
    public float verticalTerminalSpeed=10f;

    [Header("Misc")]

    [Tooltip("Sometimes when colliding with something the character goes flying off. We use this value to limit the speed resulting.")]
    public float maxCollisionSpeed=4f;
    public float collisionSpeedFactor=1f;
    public float minCollisionSpeed=0.1f;

    private bool justCollided=false;
    private Vector3 collisionVelocity;
    private Vector3 prevVelocity;
    private Vector3 prevPosition;

    private int collisionNumber=0;


    public bool grounded=false;
    public bool onIce=false;

    private AudioSource audioSource;
    private float soundTimer;
    public float soundTime=1f;
    public float soundDelay=0.5f;
    public AudioClip[] skateSounds;

    private ImageChanger imageChanger;

    void Start()
    {
        controller=GetComponent<CharacterController>();
        playerInput=GetComponent<PlayerInput>();
        animator=GetComponent<Animator>();
        //body=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
        imageChanger=FindObjectOfType<ImageChanger>();
    }

    void Update(){
        if(playerInput.movingForward && !playerInput.prevMovingForward){
            boostTimer=0f;
            //animator.SetTrigger("boostForward");
        }
        Move();
        Sound();
    }

    void Move(){
        float v=1f;

        if(!onIce){
            v=0.5f;
        }

        Vector3 currentVelocity=new Vector3(controller.velocity.x,0f,controller.velocity.z);
        Vector3 playerVelocity=currentVelocity;

        //Deceleration of rotation speed
        Vector3 antiRotationVector=rotationVelocity;
        Vector3 prevRotationVelocity=rotationVelocity;
        antiRotationVector=antiRotationVector.normalized*rotationDeceleration*Time.deltaTime;

        rotationVelocity=rotationVelocity-=antiRotationVector;

        //Cancel rotation deceleration if it goes past the direction
        if(playerInput.look==Vector2.zero){
            if(rotationVelocity.x/Mathf.Abs(rotationVelocity.x)!=prevRotationVelocity.x/Mathf.Abs(prevRotationVelocity.x)){
                rotationVelocity.x=0;
            }
            if(rotationVelocity.y/Mathf.Abs(rotationVelocity.y)!=prevRotationVelocity.y/Mathf.Abs(prevRotationVelocity.y)){
                rotationVelocity.y=0;
            }
            if(rotationVelocity.z/Mathf.Abs(rotationVelocity.z)!=prevRotationVelocity.z/Mathf.Abs(prevRotationVelocity.z)){
                rotationVelocity.z=0;
            }
        }

        if(playerInput.look!=Vector2.zero){//Setting rotation speed
            rotationVelocity+=new Vector3(playerInput.look.y,playerInput.look.x,0f)*rotationAcceleration*Time.deltaTime*v;
            rotationVelocity=Vector3.ClampMagnitude(rotationVelocity,rotationMaxVelocity);
        }else{//Deceleration of rotation speed

        }

        Vector3 skew=new Vector3(0f,v*skewingForce*Mathf.Clamp(currentVelocity.magnitude/maxSkewingVelocity,0f,1f)*Time.deltaTime,0f);
        float dir=1;
        if(Vector3.Angle(currentVelocity,transform.forward)>90f){
            dir=-1;
        }
        rotationVelocity+=skew*dir;

        rotationVelocity=new Vector3(0f,rotationVelocity.y,0f);

        //Rotating player
        Vector3 newRotation=transform.rotation.eulerAngles;
        newRotation+=rotationVelocity*Time.deltaTime;

        transform.rotation=Quaternion.Euler(newRotation);
        //body.MoveRotation(Quaternion.Euler(newRotation));

        //Deceleration
        if(grounded){
            Vector3 decelerationVector=currentVelocity;
            decelerationVector=decelerationVector.normalized*deceleration*Time.deltaTime;

            decelerationVector=new Vector3(decelerationVector.x,0f,decelerationVector.z);

            playerVelocity=playerVelocity-=decelerationVector;

            //Cancel deceleration if it goes past the direction
            if(!playerInput.movingForward && !playerInput.movingBackward && !playerInput.movingLeft && !playerInput.movingRight && !playerInput.movingUp && !playerInput.movingUp){
                if(playerVelocity.x/Mathf.Abs(playerVelocity.x)!=currentVelocity.x/Mathf.Abs(currentVelocity.x)){
                    playerVelocity.x=0;
                }
                if(playerVelocity.y/Mathf.Abs(playerVelocity.y)!=currentVelocity.y/Mathf.Abs(currentVelocity.y)){
                    playerVelocity.y=0;
                }
                if(playerVelocity.z/Mathf.Abs(playerVelocity.z)!=currentVelocity.z/Mathf.Abs(currentVelocity.z)){
                    playerVelocity.z=0;
                }
            }
        }

        boostTimer+=Time.deltaTime;

        //Checking if the player just collided and went flying off => dampen speed
        if(justCollided){
            Vector3 velocityChange=playerVelocity-collisionVelocity;
            if(velocityChange.magnitude>=maxCollisionSpeed && Vector3.Angle(collisionVelocity,playerVelocity)>=45){
            //     //playerVelocity=Vector3.zero;

                
                //playerVelocity=collisionVelocity+Vector3.ClampMagnitude(velocityChange,maxCollisionSpeed);
                playerVelocity=collisionVelocity+velocityChange.normalized*(maxCollisionSpeed+Mathf.Sqrt(velocityChange.magnitude-maxCollisionSpeed+1)-1);
            }
            justCollided=false;
        }else{
            collisionNumber=0;
        }

        //Boosting player velocity at the end of the stroke
        if(boostTimer>boostTime && boostTimer-Time.deltaTime<=boostTime){
            playerVelocity+=transform.forward*boostSpeed;
        }

        //Adding velocity
        var velocityToAdd=Vector3.zero;

        if(grounded){
            if(playerInput.movingForward && !playerInput.movingBackward){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,transform.forward)>=90f){
                    velocityToAdd+=transform.forward*acceleration*Time.deltaTime*v;
                }
                if(!playerInput.prevMovingForward){
                    boostTimer=0f;
                }
            }else if(playerInput.movingBackward && !playerInput.movingForward){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,-transform.forward)>=90f){
                    velocityToAdd+=-transform.forward*backwardAcceleration*Time.deltaTime*v;
                }
                boostTimer=boostTime+1f;
            }else{
                boostTimer=boostTime+1f;
            }

            if(playerInput.movingLeft && !playerInput.movingRight){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,-transform.right)>=90f){
                    velocityToAdd+=-transform.right*acceleration*Time.deltaTime*v;
                }
            }else if(playerInput.movingRight && !playerInput.movingLeft){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,transform.right)>=90f){
                    velocityToAdd+=transform.right*acceleration*Time.deltaTime*v;
                }
                boostTimer=boostTime+1f;
            }else{
                boostTimer=boostTime+1f;
            }
        }

        velocityToAdd=Vector3.ClampMagnitude(velocityToAdd,acceleration*Time.deltaTime);
        playerVelocity+=velocityToAdd;


        playerVelocity=Vector3.ClampMagnitude(playerVelocity,maxVelocity);

        //controller.Move(playerVelocity*Time.deltaTime); 
        //body.MovePosition(transform.position+playerVelocity*Time.deltaTime);

        //Vertical movement
        Vector3 verticalVelocity=new Vector3(0f,controller.velocity.y,0f);

        //Gravity
        if(!grounded){
            verticalVelocity-=Time.deltaTime*gravity*Vector3.up;
        }
        verticalVelocity=Vector3.ClampMagnitude(verticalVelocity,verticalTerminalSpeed);

        //Jumping
        if(grounded && playerInput.jumping && !playerInput.prevJumping){
            verticalVelocity=Vector3.up*jumpForce;
        }

        controller.Move((playerVelocity+verticalVelocity)*Time.deltaTime); 

        prevVelocity=controller.velocity;

        imageChanger.AddDistance(Vector3.Distance(new Vector3(prevPosition.x,0f,prevPosition.z),new Vector3(transform.position.x,0f,transform.position.z)));
        prevPosition=transform.position;

    }

    void Sound(){
        if(grounded && (playerInput.movingForward || playerInput.movingBackward || playerInput.movingRight || playerInput.movingLeft)){
            soundTimer+=Time.deltaTime;
            if(soundTimer>=soundDelay && soundTimer-Time.deltaTime<soundDelay){
                PlaySound();
            }
            soundTimer=soundTimer%soundTime;
        }else{
            soundTimer=0f;
        }
    }

    void PlaySound(){
        if(onIce){
            audioSource.pitch=1;
        }else{
            audioSource.pitch=0.5f;
        }
        audioSource.Stop();
        audioSource.clip=skateSounds[Random.Range(0,skateSounds.Length)];
        audioSource.Play();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        justCollided=true;
        collisionVelocity=controller.velocity;
        collisionNumber+=1;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Ground"){
            grounded=true;
            if(other.gameObject.name=="Ice"){
                onIce=true;
                imageChanger.Landed();
                PlaySound();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag=="Ground"){
            grounded=true;
        }
    }
        
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag=="Ground"){
            grounded=false;
            if(other.gameObject.name=="Ice"){
                onIce=false;
            }
        }
    }

    // private void OnCollisionStay(Collision other) {
    //     ContactPoint[] myContacts = new ContactPoint[other.contactCount];
    //     for(int i = 0; i < myContacts.Length; i++)
    //     {
    //         myContacts[i] = other.GetContact(i);
    //         CollisionData c=new CollisionData(myContacts[i],other.relativeVelocity);
    //         allHits.Add(c);
    //     }
    //     Debug.Log("collision enter");
    //     Debug.Log(other.gameObject);
    // }

    // // SHOULD MOVE THIS SOMEWHERE ELSE
    // public struct CollisionData{
    //     public ContactPoint contact;
    //     public Vector3 relativeVelocity;

    //     public CollisionData(ContactPoint c, Vector3 v){
    //         contact=c;
    //         relativeVelocity=v;
    //     }
    // }
}
