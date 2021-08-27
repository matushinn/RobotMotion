using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGroup {

    //RobotScriptの保存場所
    List<RobotScript> robots = new List<RobotScript>();

    //コンストラクタ
    public RobotGroup(GameObject prefab, float px, float py)
    {
        //一つのグループにつき9体生成する。
        for(int x = 0; x<3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                //新たに作成するロボットの２次元位置をビューポート座標で求め、ワールド座標に変換して、ゲームオブジェクトを作成する。
                Vector3 p = Camera.main.ViewportToWorldPoint(
                    new Vector3(px+0.1f * x, py + 0.13f * y, 0f));
                p.z = 0f;
                GameObject obj = Object.Instantiate(prefab, p, Quaternion.identity);
                robots.Add(obj.GetComponent<RobotScript>());
            }
        }
    }
    //全てをモーションさせるための関数
    public void MotionAll(RobotMotion motion)
    {
        foreach (var r in robots)
        {
            r.AddMotion(new RobotMotion(motion));
        }
    }
    //ランダムでモーションさせるための関数
    public void MotionRandom(RobotMotion[] motions, List<int> selected)
    {
        foreach(var r in robots)
        {
            //selは0~motionの数未満の乱数を発生
            int sel = Random.Range(0, motions.Length);
            r.AddMotion(new RobotMotion(motions[sel]));
            //poseの数の更新
            selected[sel]++;
        }
    }

}
