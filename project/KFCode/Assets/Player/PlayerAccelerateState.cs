using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccelerateState : PlayerGroundedState
{
    public PlayerAccelerateState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
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

        if(player.currentSpeed < player.maxSpeed)
        {
            // 访问权限有待商榷
            player.currentSpeed += player.acceleration;
            player.currentSpeed = player.currentSpeed > player.maxSpeed ? player.maxSpeed : player.currentSpeed ;
            player.setVelocity(xInput * player.currentSpeed, rb.velocity.y);
        }
        else
        {
            stateMachine.changeState(player.moveState);
        }

        if(xInput == 0|| xInput == -player.facingDir)
        {
            stateMachine.changeState(player.decelerateState);
        }


    }
}
