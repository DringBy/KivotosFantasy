using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }

    //public override void update()
    //{
    //    base.update();

    //    player.setVelocity(xInput * player.maxSpeed, rb.velocity.y);

    //    if (xInput == 0 || xInput == -player.facingDir)
    //    {
    //        stateMachine.changeState(player.decelerateState);
    //    }

    //    if (player.isWallDetected())
    //    {
    //        stateMachine.changeState(player.idleState);
    //    }

    //    // ½øÈë¼õËÙ×´Ì¬
    //    if(xInput == 0 || xInput == -player.facingDir)
    //    {
    //        stateMachine.changeState(player.decelerateState);
    //    }
    //}

    public override void update()
    {
        base.update();

        if (player.isWallDetected() && player.currentSpeed * player.facingDir > 0)
        {
            //player.currentLeftSpeed = 0;
            //player.currentRightSpeed = 0;
            player.currentSpeed = 0;
            stateMachine.changeState(player.idleState);
            return;
        }

        if (xInput == 0 && player.currentSpeed == 0)
        {
            //player.currentLeftSpeed = 0;
            //player.currentRightSpeed = 0;
            //player.currentSpeed = 0;
            stateMachine.changeState(player.idleState);
        } 
        else if (xInput == 0 && player.currentSpeed != 0)
        {
            /*if (player.facingDir == -1)
            {
                player.currentRightSpeed = 0;
                player.currentLeftSpeed = player.currentLeftSpeed - player.deceleration < 0 ? 0 : player.currentLeftSpeed - player.deceleration;
            }
            else if (player.facingDir == 1)
            {
                player.currentLeftSpeed = 0;
                player.currentRightSpeed = player.currentRightSpeed - player.deceleration < 0 ? 0 : player.currentRightSpeed - player.deceleration;
            }*/
            if ((player.currentSpeed - player.facingDir * player.deceleration) * player.currentSpeed > 0)
                player.currentSpeed -= player.facingDir * player.deceleration;
            else
                player.currentSpeed = 0;
        }
        else if (xInput * player.facingDir > 0)
        {
            if (player.facingDir * (player.currentSpeed + xInput * player.acceleration) >= player.maxSpeed)
                player.currentSpeed = player.facingDir * player.maxSpeed;
            else 
                player.currentSpeed += xInput * player.acceleration;
        }
        else
        {
            player.currentSpeed += xInput * player.acceleration - player.facingDir * player.deceleration;
        }
        /*else if(xInput > 0)
        {
            player.currentRightSpeed = player.currentRightSpeed + player.acceleration > player.maxSpeed ? player.maxSpeed : player.currentRightSpeed + player.acceleration;
            player.currentLeftSpeed = player.currentLeftSpeed - player.deceleration < 0 ? 0 : player.currentLeftSpeed - player.deceleration;
        }
        else if(xInput < 0)
        {
            player.currentLeftSpeed = player.currentLeftSpeed + player.acceleration > player.maxSpeed ? player.maxSpeed : player.currentLeftSpeed + player.acceleration;
            player.currentRightSpeed = player.currentRightSpeed - player.deceleration < 0 ? 0 : player.currentRightSpeed - player.deceleration;
        }
        player.currentSpeed = player.currentRightSpeed - player.currentLeftSpeed;
        */
        player.setVelocity(player.currentSpeed, rb.velocity.y);
    }
}
