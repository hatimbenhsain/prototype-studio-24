using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Timeline;

public class Swimmer : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration=1f;
    public float backwardAcceleration=1f;
    public float maxVelocity=10f;
    [Tooltip("Deceleration always takes effect")]
    public float deceleration=5f;
    public float lateralAcceleration=1f;
    public float lateralMaxVelocity=5f;

    [Tooltip("Instant speed gain at the end of a stroke")]
    public float boostSpeed=1f;
    [Tooltip("Swimmer maintains coasting speed when not boosting")]
    public float coastingSpeed=3f;  //maintains coasting speed when not boosting

    [Tooltip("Timer for before boost takes effect")]
    public float boostTimer=0f;
    [Tooltip("Total time it takes before boost takes effect")]
    public float boostTime=1f;     //time before boost takes effect

    private CharacterController controller;
    private Rigidbody body;
    //private PlayerInput playerInput;
    //private SwimmerCamera swimmerCamera;

    [Header("Rotation")]
    public float rotationAcceleration=180f;
    public float rotationMaxVelocity=180f;
    private Vector3 rotationVelocity=Vector3.zero;
    public float rotationDeceleration=180f;
    public float maxRotationXAngle=60f;

    public float maxTiltAngle=10f; //max angle to tilt when moving laterally
    public float angleTiltSpeed=1f; //speed to tile when moving laterally


    [Header("Collision")]
    [Tooltip("Factor to multiply anti-collision vector to mess with bounce. Usually 1.")]
    public float collisionSpeedFactor=1f;
    [Tooltip("Min reaction speed when reacting with anything even if the relative velocity is 0.")]
    public float minCollisionSpeed=0.01f;
    [Tooltip("Min distance to move player out of object it's clipping into.")]
    public float minMoveoutStep=0.001f;
    [Tooltip("How many maximum moveout steps can the player perform every fixedupdate.")]
    public int maxMoveoutEffort=100;
    [Tooltip("Distance to keep object away from any collider.")]
    public float skinWidth=0.05f;
    [Tooltip("Min angle to rotate player when colliding with something.")]
    public float minCollisionRotationAmount=0.1f;
    [Tooltip("Max angle to rotate player when colliding with something.")]
    public float maxCollisionRotationAmount=1f;
    [Tooltip("Minimum force of collision to make player rotate.")]
    public float minCollisionForceToRotate=0.01f;
    [Tooltip("Cap force of collision to make player rotate.")]
    public float maxCollisionForceToRotate=5f;

    [Header("Misc.")]


    private Vector3 prevVelocity;

    ArrayList allHits=new ArrayList();

    private CapsuleCollider capsule;


    private Vector3 forcesToAdd; //Forces to add at the beginning of the next frame; this is used for e.g. for ring boosts


    public bool yAxisInverted=false;

    void Start()
    {
        controller=GetComponent<CharacterController>();
        body=GetComponent<Rigidbody>();
        capsule=GetComponentInChildren<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen


    }

    void Update(){
        // if(playerInput.movedForwardTrigger){
        //     boostTimer=0f;
        //     animator.SetTrigger("boostForward");
        //     playerInput.movedForwardTrigger=false;
        // }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move(){
        Vector3 currentVelocity=new Vector3(body.velocity.x,body.velocity.y,body.velocity.z);
        Vector3 playerVelocity=currentVelocity;

        //Deceleration of rotation speed
        Vector3 antiRotationVector=rotationVelocity;
        Vector3 prevRotationVelocity=rotationVelocity;
        antiRotationVector=antiRotationVector.normalized*rotationDeceleration*Time.fixedDeltaTime;

        rotationVelocity=rotationVelocity-=antiRotationVector;

        Vector2 look=Vector2.zero;

        if(Input.GetKey(KeyCode.LeftArrow)){
            look+=Vector2.left;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            look+=Vector2.right;
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            if(yAxisInverted) look-=Vector2.up;
            else look+=Vector2.up;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            if(yAxisInverted) look+=Vector2.up;
            else look-=Vector2.up;
        }

        //Cancel rotation deceleration if it goes past the direction
        if(look==Vector2.zero){
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

        if(look!=Vector2.zero){//Setting rotation speed
            rotationVelocity+=new Vector3(look.y,look.x,0f)*rotationAcceleration*Time.fixedDeltaTime;
            rotationVelocity=Vector3.ClampMagnitude(rotationVelocity,rotationMaxVelocity);
        }

        //Finding rotation to do
        Vector3 newRotation=body.rotation.eulerAngles;
        newRotation+=rotationVelocity*Time.fixedDeltaTime;
        //Clamping x rotation
        if(newRotation.x<180f){
            newRotation.x=Mathf.Clamp(newRotation.x,-maxRotationXAngle,maxRotationXAngle);
        }else{
            newRotation.x=Mathf.Clamp(newRotation.x,360f-maxRotationXAngle,360f+maxRotationXAngle);
        }

        bool movingForward=Input.GetKey(KeyCode.A);
        bool movingBackward=Input.GetKey(KeyCode.Z);
        bool movingLeft=Input.GetKey(KeyCode.Q);
        bool movingRight=Input.GetKey(KeyCode.W);

        bool movingUp=false;
        bool movingDown=false;

        //Tilt player if moving laterally
        float targetRotationZ=0f;
        if(movingLeft && !movingRight){
            targetRotationZ=maxTiltAngle;
        }else if(movingRight && !movingLeft){
            targetRotationZ=-maxTiltAngle;
        }
        if(newRotation.z>=180f){
            targetRotationZ=360f+targetRotationZ;
        }
        newRotation.z=Mathf.Lerp(newRotation.z,targetRotationZ,Time.fixedDeltaTime*angleTiltSpeed);

        Quaternion newRotationQ=Quaternion.Euler(newRotation);

        //Deceleration
        Vector3 decelerationVector=currentVelocity;
        decelerationVector=decelerationVector.normalized*deceleration*Time.fixedDeltaTime;

        playerVelocity=playerVelocity-=decelerationVector;

        //Cancel deceleration if it goes past the direction
        if(!movingForward && !movingBackward && !movingLeft && !movingRight && !movingUp && !movingUp){
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


        //Rotating player
        body.MoveRotation(newRotationQ);


        boostTimer+=Time.fixedDeltaTime;

        //Boosting player velocity at the end of the swimstroke
        if(boostTimer>boostTime && boostTimer-Time.fixedDeltaTime<=boostTime){
            playerVelocity+=transform.forward*boostSpeed;
        }

        //Adding external forces, for e.g. from ring booster
        playerVelocity+=forcesToAdd;
        forcesToAdd=Vector3.zero;

        //Adding velocity from swimming
        if(movingForward && !movingBackward){
            if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,transform.forward)>=90f){
                playerVelocity+=transform.forward*acceleration*Time.fixedDeltaTime;
            }
        }else if(movingBackward && !movingForward){
            if(playerVelocity.magnitude<coastingSpeed || Vector3.Angle(playerVelocity,-transform.forward)>=90f){
                playerVelocity+=-transform.forward*backwardAcceleration*Time.fixedDeltaTime;
            }
            boostTimer=boostTime+1f;
        }else{
            boostTimer=boostTime+1f;
        }

        //Lateral movement
        if(movingLeft && !movingRight && Mathf.Abs((body.rotation*playerVelocity).x)<lateralMaxVelocity){
            playerVelocity+=-transform.right*lateralAcceleration*Time.fixedDeltaTime;
        }else if(movingRight && !movingLeft && Mathf.Abs((body.rotation*playerVelocity).x)<lateralMaxVelocity){
            playerVelocity+=transform.right*lateralAcceleration*Time.fixedDeltaTime;
        }

        //Vertical movement
        if(movingUp && !movingDown && Mathf.Abs((body.rotation*playerVelocity).y)<lateralMaxVelocity){
            playerVelocity+=transform.up*lateralAcceleration*Time.deltaTime;
        }else if(movingDown && !movingUp && Mathf.Abs((body.rotation*playerVelocity).y)<lateralMaxVelocity){
            playerVelocity+=-transform.up*lateralAcceleration*Time.fixedDeltaTime;
        }

        playerVelocity=Vector3.ClampMagnitude(playerVelocity,maxVelocity);

        // Using MovePosition because it interpolates movement smoothly and keeps velocity in next frame
        body.MovePosition(body.position+playerVelocity*Time.fixedDeltaTime);

        prevVelocity=body.velocity;

        allHits.Clear();

    }

    // Boost can be from external effect for e.g. ring
    public void Boost(Vector3 force){
        forcesToAdd+=force;
    }

    // //Things to animate right after boost happens
    // void BoostAnimation(){
    //     swimmerCamera.BoostAnimation();
    // }

    // private void OnCollisionStay(Collision other) {
    //     ContactPoint[] myContacts = new ContactPoint[other.contactCount];
    //     for(int i = 0; i < myContacts.Length; i++)
    //     {
    //         myContacts[i] = other.GetContact(i);
    //         allHits.Add(myContacts[i]);
    //     }
    // }

    // //This function is for movement of player out of a collider without moving too fast
    // //It doesn't move the player but it returns the Vector3 to move it by
    // public Vector3 MoveOutOf(Collider other, Vector3 pos, Vector3 normal){
    //     Vector3 prevVelocity=body.velocity;

    //     Vector3 dir=new Vector3();
    //     switch(capsule.direction){
    //         case 0:
    //             dir=transform.right;
    //             break;
    //         case 1:
    //             dir=transform.up;
    //             break;
    //         case 2:
    //             dir=transform.forward;
    //             break;
    //     }
    //     Vector3 point1=pos+capsule.center+dir*(capsule.height/2f-capsule.radius);
    //     Vector3 point2=pos+capsule.center-dir*(capsule.height/2f-capsule.radius);
    //     RaycastHit[] hits;

    //     bool colliding=true;
    //     int tries=0;
    //     Vector3 movement=Vector3.zero; //Amount to move the collider 

    //     LayerMask mask = LayerMask.GetMask("Default");

    //     while(colliding && tries<maxMoveoutEffort){
    //         hits=Physics.CapsuleCastAll(point1+movement,point2+movement,capsule.radius+skinWidth,normal,0f,mask,QueryTriggerInteraction.Ignore);
    //         colliding=false;
    //         foreach(RaycastHit hit in hits){
    //             if(hit.collider==other){
    //                 colliding=true;
    //                 movement+=normal*minMoveoutStep;
    //                 break;
    //             }
    //         }
    //         tries++;
    //     }

    //     return movement;

    // }

    public Vector3 GetVelocity(){
        return body.velocity;
    }
}


/*
TO-DO:
    + MAKE LATERAL/FORWARD MOVEMENT ONLY ONE AT A TIME
    + ADD BOOST
    + EXPRIMENT WITH LERP INSTEAD OF LINEAR DECELERATION
    + LOOK INTO PHYSICS SNAPPING
    + MAKE CAMERA COLLISIONS work
*/

/* REMOVED BUT COULD STILL BE USEFUL CODE:

Dampening post-collision speed:
        //Checking if the player just collided and went flying off => dampen speed
        
        [Tooltip("Sometimes when colliding with something the character goes flying off. We use this value to limit the speed resulting.")]
        public float maxCollisionSpeed=4f;

        if(justCollided){
            
            if(collisionNumber==1){

            }
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

*/