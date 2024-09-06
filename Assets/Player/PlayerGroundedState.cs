using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void enter()
    {
        base.enter();
        // 进入地面状态后，将土狼时间重新初始化
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();


        // 避免当dash后退出dashState转入idleState时播放moveState的动画
        if (!player.isGroundDetected())
        {
            stateMachine.changeState(player.airState);
        }

        // 正常起跳
        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected())
        {
            stateMachine.changeState(player.jumpState);
            player.coyoteUsageTimer = 0f;  // 防止二段跳
        }

    }
}
