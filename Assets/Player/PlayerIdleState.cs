using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        rb.velocity = new Vector2(0, 0);
        //player.currentXSpeed = 0;  // 落地后速度为0
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();

        if (player.currentXSpeed != 0)
        {
            stateMachine.changeState(player.moveState);
        }

        // 避免因为在撞墙时moveState和IdleState反复切换导致频闪
        if(player.isWallDetected() && xInput == player.facingDir)
        {
            return;
        }

        if (xInput != 0)
        {
            stateMachine.changeState(player.moveState);
        }
    }
}
