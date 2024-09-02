using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecelerateState : PlayerGroundedState
{
    public PlayerDecelerateState(PlayerStateMachine _stateMachine, Player _player, global::System.String _animBoolName) : base(_stateMachine, _player, _animBoolName)
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

        if(player.currentSpeed > 0)
        {
            player.currentSpeed -= player.deceleration;
            player.currentSpeed = player.currentSpeed < 0 ? 0 : player.currentSpeed;
            // 减速时的方向根据输入决定
            float decelerateDir = xInput == 0 ? player.facingDir : xInput;
            player.setVelocity(decelerateDir * player.currentSpeed, rb.velocity.y);
        }
        else
        {
            stateMachine.changeState(player.idleState);  // 这意味着在速度降为0前，减速状态无法退出
        }
    }
}
