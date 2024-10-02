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

    private int collisionNumber=0;


    public bool grounded=false;

    void Start()
    {
        controller=GetComponent<CharacterController>();
        playerInput=GetComponent<PlayerInput>();
        animator=GetComponent<Animator>();
        //body=GetComponent<Rigidbody>();
    }

    void Update(){
        if(playerInput.movingForward && !playerInput.prevMovingForward){
            boostTimer=0f;
            //animator.SetTrigger("boostForward");
        }
        Move();
    }

    void Move(){
        Vector3 currentVelocity=new Vector3(controller.velocity.x,0f,controller.velocity.z);
        Vector3 playerVelocity=currentVelocity;

        //Deal with collisions
        // foreach (CollisionData hit in allHits)
        // {
            
        //     Vector3 collisionPoint=hit.contact.point;
        //     Vector3 normal=hit.contact.normal;
        //     Vector3 relativeVelocity;
        //     if(hit.contact.otherCollider.TryGetComponent<Rigidbody>(out Rigidbody otherBody)){
        //         relativeVelocity=otherBody.velocity;
        //     }
        //     else{
        //         relativeVelocity=Vector3.zero;
        //     }
        //     relativeVelocity=relativeVelocity-playerVelocity;
        //     Vector3 force=normal*relativeVelocity.magnitude*collisionSpeedFactor;
        //     //Projection of relative vel on normal
        //     //force=collisionSpeedFactor*Vector3.Dot(relativeVelocity,normal)*normal/Vector3.Dot(normal,normal);
        //     force=Vector3.Project(relativeVelocity,normal)*collisionSpeedFactor;
        //     if(Vector3.Angle(force,normal)>=90){
        //         force=Vector3.zero;
        //     }
        //     if(force==Vector3.zero){
        //         force+=normal*minCollisionSpeed;
        //     }
        //     Debug.Log("relative velocity:");
        //     Debug.Log(hit.relativeVelocity);
        //     Debug.Log("player velocity:");
        //     Debug.Log(playerVelocity);
        //     Debug.Log("force:");
        //     Debug.Log(force);

        //     playerVelocity+=force;

        //     Debug.DrawRay(hit.contact.point, hit.contact.normal , Color.magenta, 1f);
        //     Debug.DrawRay(transform.position, relativeVelocity , Color.green, 1f);
        //     Debug.DrawRay(transform.position+Vector3.one*0.1f, force , Color.blue, 1f);
        // }
        // allHits.Clear();

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
            rotationVelocity+=new Vector3(playerInput.look.y,playerInput.look.x,0f)*rotationAcceleration*Time.deltaTime;
            rotationVelocity=Vector3.ClampMagnitude(rotationVelocity,rotationMaxVelocity);
        }else{//Deceleration of rotation speed

        }

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
                    velocityToAdd+=transform.forward*acceleration*Time.deltaTime;
                }
                if(!playerInput.prevMovingForward){
                    boostTimer=0f;
                }
            }else if(playerInput.movingBackward && !playerInput.movingForward){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,-transform.forward)>=90f){
                    velocityToAdd+=-transform.forward*backwardAcceleration*Time.deltaTime;
                }
                boostTimer=boostTime+1f;
            }else{
                boostTimer=boostTime+1f;
            }

            if(playerInput.movingLeft && !playerInput.movingRight){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,-transform.right)>=90f){
                    velocityToAdd+=-transform.right*acceleration*Time.deltaTime;
                }
            }else if(playerInput.movingRight && !playerInput.movingLeft){
                if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,transform.right)>=90f){
                    velocityToAdd+=transform.right*acceleration*Time.deltaTime;
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

    }
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        justCollided=true;
        collisionVelocity=controller.velocity;
        collisionNumber+=1;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject);
        if(other.gameObject.tag=="Ground"){
            grounded=true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(other.gameObject);
        if(other.gameObject.tag=="Ground"){
            grounded=false;
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
