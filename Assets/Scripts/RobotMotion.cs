using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMotion {

    //メソッドをオブジェクトとして保存するActionクラス(RobotScript(モーション対象のロボット),float(モーションが始まってからの時間))
    Action<RobotScript, float> animation;
    //何秒かかるか
    float duration;
    //現在の時間
    float pastTime = 0;

    //コピー
    public RobotMotion(RobotMotion src) : this(src.animation,src.duration)
    {
    }
    public RobotMotion(Action<RobotScript, float> animation, float duration)
    {
        //上のとの区別
        this.animation = animation;
        this.duration = duration;
    }
    //モーションが動くかの確認の関数
    public bool Animate(RobotScript robot, float deltaTime)
    {
        pastTime += deltaTime;
        //割合として引数で渡す
        animation(robot, pastTime / duration);
        //アニメーションが終わったのか？
        return pastTime >= duration;
    }
}
