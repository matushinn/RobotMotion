using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGenerator : MonoBehaviour {

    //prefabの初期化
    public GameObject prefab;
    //RobotGroupの保存場所
    List<RobotGroup> groups = new List<RobotGroup>();

	// Use this for initialization
	void Start () {

        //二つのRobotGroupを作成
        for(int cnt=0;cnt<2;cnt++)
        {
            groups.Add(new RobotGroup(prefab,0.2f + 0.4f * cnt, 0.3f));
        }
	}

    //マウスクリックのボタンをコードで表すための関数
    int MouseCode()
    {
        //両方のクリック(3)
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            return 3;
        }
        //左クリック(1)
        else if (Input.GetMouseButtonDown(0))
        {
            return 1;
        }
        //右クリック(2)
        else if (Input.GetMouseButtonDown(1))
        {
            return 2;
        }
        //押されていない(0)
        else
        {
            return 0;
        }
    }

    // Update is called once per frame
    void Update () {
        int mcode = MouseCode();
        //右、左クリックをするとランダムな動きをする
        if(mcode == 1 || mcode == 2)
        {
            //robotがどのような動きをするのかの配列を格納
            RobotMotion[] motions = new RobotMotion[] {
                new RobotMotion(
                    (r, p) =>
                    {
                        if (p >= 0.5)
                        {
                            r.Pose("Left");
                        }
                    }, 2f
                    ),
                new RobotMotion(
                    (r, p) =>
                    {
                        if (p >= 0.5)
                        {
                            r.Pose("Right");
                        }
                    }, 2f
                    ),
            };

            //Poseの数を保存するためのリスト
            List<int>[] selected = new List<int>[]
            {
                new List<int>(){0,0},
                new List<int>(){0,0},
            };
            //robotGroupの数だけランダムにモーションさせる
            for(int cnt=0;cnt<groups.Count;cnt++)
            {
                groups[cnt].MotionRandom(motions, selected[cnt]);
            }

            //勝利ダンスのモーションを格納する
            RobotMotion dance = new RobotMotion(
                (r, p) =>
                {
                    //robotを回転させる
                    r.GetComponent<Transform>().localRotation =
                    Quaternion.Euler(0, 360f * p, 0);
                }, 2f
            );

            //左側のグループの方が数が大きい場合、左側はMotionAll
            if (selected[0][mcode-1] > selected[1][mcode - 1])
            {
                Debug.Log("左");
                groups[0].MotionAll(dance);
            }
            //右側のグループの方が数が大きい場合、右側はMotionAll
            else if(selected[0][mcode - 1] < selected[1][mcode - 1])
            {
                Debug.Log("右");
                groups[1].MotionAll(dance);
            }
            //それ以外
            else
            {
                Debug.Log("引き分け");
            }
        }
    }
}
