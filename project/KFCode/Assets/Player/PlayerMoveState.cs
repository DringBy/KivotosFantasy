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
        Debug.Log("in moveState");
        base.update();

        if (player.isWallDetected() && xInput == 0)
        {
            stateMachine.changeState(player.idleState);
            return;
        }


        if (xInput == 0 && currentXSpeed == 0)
        {
            stateMachine.changeState(player.idleState);
        } 
        else if (xInput == 0 && currentXSpeed != 0)
        {
            player.decelerate(currentXSpeed, currentYSpeed);
        }
        else if (xInput * player.facingDir > 0)  // 同向运动加速
        {
            player.accelerate(currentXSpeed, currentYSpeed, xInput);
        }
        else // 反向运动
        {
            player.reverseAccelerate(currentXSpeed, currentYSpeed, xInput);
        }
    }
}
