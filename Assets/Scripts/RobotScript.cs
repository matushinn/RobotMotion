using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour {
    //マウスの反応の制御の変数
    public bool ManualDance = false;
    //画像の保存場所(連想配列)
    Dictionary<string, Sprite> pose = new Dictionary<string, Sprite>()
    {
        {"Normal", null },
        {"Right",  null },
        {"Left",   null },
        {"Both",   null },
    };
    //RobotMotionの保存場所
    List<RobotMotion> motions = new List<RobotMotion>();

    void Start ()
    {
        //新しいListにpose.keyをコピーして、ResourceフォルダからSpriteの読み込み、画像の幅と回転の中心を指定してcreate
        foreach(var key in new List<string>(pose.Keys))
        {
            Texture2D tex = (Texture2D)Resources.Load("Robot_"+key);
            pose[key] = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }

    //モーションを追加する関数
    public void AddMotion(RobotMotion m)
    {
        motions.Add(m);
    }
    //ポーズ(p)によって画像を挿入する関数
    public void Pose(string p)
    {
        GetComponent<SpriteRenderer>().sprite = pose[p];
    }
    void Update ()
    {
        //モーションが追加されている場合
        if(motions.Count > 0)
        {
            //モーションが終わっているかの確認
            bool finished = motions[0].Animate(this, Time.deltaTime);
            if (finished)
            {
                //終わっていたら削除する
                motions.RemoveAt(0);
            }
        }

        //マウス反応の制御
        if (ManualDance == true)
        {
            //両方のクリックが押されていた場合
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                Pose("Both");
            }
            //左クリックだけが押されていた場合
            else if (Input.GetMouseButton(0))
            {
                Pose("Left");
            }
            //右クリックだけが押されていた場合
            else if (Input.GetMouseButton(1))
            {
                Pose("Right");
            }
            //何も押されていない場合
            else
            {
                Pose("Normal");
            }
        }
    }
}
