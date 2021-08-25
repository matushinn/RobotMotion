using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGroup
{

    List<RobotScript> robots = new List<RobotScript>();

    public RobotGroup(GameObject prefab, float px, float py)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(px + 0.1f * x, py + 0.13f * y, 0f));
                p.z = 0f;
                GameObject obj = Object.Instantiate(prefab, p, Quaternion.identity);
                robots.Add(obj.GetComponent<RobotScript>());
            }
        }
    }
    public void MotionAll(RobotMotion motion)
    {
        foreach(var r in robots)
        {
            r.AddMotion(new RobotMotion(motion));
        }
    }
        public void MotionRandom(RobotMotion[] motion,List<int> selected)
    {
        foreach(var r in robots)
        {
            int sel = Random.Range(0, motion.Length);
            r.AddMotion(new RobotMotion(motion[sel]));
            selected[sel]++;
        }
    }
}