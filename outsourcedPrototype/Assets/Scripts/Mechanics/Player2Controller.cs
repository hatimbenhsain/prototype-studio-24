using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    /// 

    public class Player2Controller : PlayerController
    {
        public Sprite jumpSprite;
        public Sprite idleSprite;
        public Sprite runningSprite;
        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x=0f;
                if(Input.GetKey(KeyCode.D)){
                    move.x+=1f;
                }
                if(Input.GetKey(KeyCode.A)){
                    move.x-=1f;
                }

                if (Input.GetKeyDown(KeyCode.W))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetKeyDown(KeyCode.S))
                    jumpState = JumpState.PossiblyReverse;
                else if (Input.GetKeyUp(KeyCode.W))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();

            if(IsGrounded){
                if(move.x!=0){
                    GetComponent<SpriteRenderer>().sprite=runningSprite;
                }else{
                    GetComponent<SpriteRenderer>().sprite=idleSprite;
                }
            }else{
                GetComponent<SpriteRenderer>().sprite=jumpSprite;
            }
        }

    }
}

