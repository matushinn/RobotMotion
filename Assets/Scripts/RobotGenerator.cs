using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGenerator : MonoBehaviour {

    public GameObject prefab;
    List<RobotGroup> groups = new List<RobotGroup>();

	// Use this for initialization
	void Start () {
        for (int cnt=0; cnt < 2; cnt++){
            groups.Add(new RobotGroup(prefab,0.2f+0.4f*cnt,0.3f));
        }
	}

    int MouseCode()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            return 3;
        }
        else if (Input.GetMouseButtonDown(0)){
            return 1;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        int mcode = MouseCode();
        if (mcode == 1 || mcode == 2)
        {
            RobotMotion[] motions = new RobotMotion[]
            {
                new RobotMotion(
            (r, p) =>
            {
                if (p >= 0.5)
                {
                    r.Pose("Left");
                }
            }, 2f
            ), new RobotMotion(
            (r, p) =>
            {
                if (p >= 0.5)
                {
                    r.Pose("Right");
                }
            }, 2f
            ),
        };
            List<int>[] selected = new List<int>[]
            {
                new List<int>(){0,0},
                new List<int>(){0,0},
            };

            for(int cnt=0;cnt<groups.Count;cnt++)
            {
                groups[cnt].MotionRandom(motions,selected[cnt]);
            }
            RobotMotion dance = new RobotMotion(
            (r, p) =>
            {
                if (p >= 0.5)
                {
                    r.GetComponent<Transform>().localRotation=Quaternion.Euler(0,360f*p,0);
                }
            }, 2f
            );
            if (selected[0][mcode - 1] > selected[1][mcode - 1])
            {
                Debug.Log("左");
                groups[0].MotionAll(dance);
            }
            if (selected[0][mcode - 1] < selected[1][mcode - 1])
            {
                Debug.Log("右");
                groups[0].MotionAll(dance);
            }
            else
            {
                Debug.Log("引き分け");
            }
        }
	}
}
