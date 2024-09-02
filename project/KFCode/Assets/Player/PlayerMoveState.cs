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

    //    // 进入减速状态
    //    if(xInput == 0 || xInput == -player.facingDir)
    //    {
    //        stateMachine.changeState(player.decelerateState);
    //    }
    //}

    public override void update()
    {
        base.update();

        if (xInput == 0 && player.currentSpeed == 0)
        {
            stateMachine.changeState(player.idleState);
        } 
        else if (xInput == 0 && player.currentSpeed != 0)
        {
            //if ((player.currentSpeed - player.facingDir * player.deceleration) * player.currentSpeed > 0)
            //    player.currentSpeed -= player.facingDir * player.deceleration;
            //else
            //    player.currentSpeed = 0;
            player.decelerate();
        }
        else if (xInput * player.facingDir > 0)  // 同向运动加速
        {
            //if (player.facingDir * (player.currentSpeed + xInput * player.acceleration) >= player.maxSpeed)
            //    player.currentSpeed = player.facingDir * player.maxSpeed;
            //else 
            //    player.currentSpeed += xInput * player.acceleration;
            player.accelerate(xInput);
        }
        else // 反向运动
        {
            //player.currentSpeed += xInput * player.acceleration - player.facingDir * player.deceleration;
            player.reverseAccelerate(xInput);
        }

        if (player.isWallDetected() && xInput == 0)
        {
            player.currentSpeed = 0;
            stateMachine.changeState(player.idleState);
            return;
        }

        player.setVelocity(player.currentSpeed, rb.velocity.y);
    }
}
