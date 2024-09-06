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

    
    public override void update()
    {
        base.update();

        if (xInput == 0 && player.currentXSpeed == 0)
        {
            stateMachine.changeState(player.idleState);
        } 
        else if (xInput == 0 && player.currentXSpeed != 0)
        {
            //if ((player.currentXSpeed - player.facingDir * player.deceleration) * player.currentXSpeed > 0)
            //    player.currentXSpeed -= player.facingDir * player.deceleration;
            //else
            //    player.currentXSpeed = 0;
            player.decelerate();
        }
        else if (xInput * player.facingDir > 0)  // 同向运动加速
        {
            //if (player.facingDir * (player.currentXSpeed + xInput * player.acceleration) >= player.maxSpeed)
            //    player.currentXSpeed = player.facingDir * player.maxSpeed;
            //else 
            //    player.currentXSpeed += xInput * player.acceleration;
            player.accelerate(xInput);
        }
        else // 反向运动
        {
            //player.currentXSpeed += xInput * player.acceleration - player.facingDir * player.deceleration;
            player.reverseAccelerate(xInput);
        }

        if (player.isWallDetected() && xInput == 0)
        {
            player.currentXSpeed = 0;
            stateMachine.changeState(player.idleState);
            return;
        }

        player.setVelocity(player.currentXSpeed, rb.velocity.y);
    }
}
