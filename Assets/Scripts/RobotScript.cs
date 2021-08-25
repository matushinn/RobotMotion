using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour {
    public bool ManualDance = false;

    Dictionary<string, Sprite> pose = new Dictionary<string, Sprite>()
    {
        { "Normal",null},
        { "Right",null},
        { "Left",null},
        { "Both",null},
    };
    List<RobotMotion> motions = new List<RobotMotion>();

	// Use this for initialization
	void Start () {
        foreach(var key in new List<string>(pose.Keys))
        {
            Texture2D tex = (Texture2D)Resources.Load("Robot_"+key);
            pose[key]=Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
		
	}
    public void AddMotion(RobotMotion m)
    {
        motions.Add(m);
    }
   public void Pose(string p)
    {
        GetComponent<SpriteRenderer>().sprite = pose[p];

    }
	
	// Update is called once per frame
	void Update () {
        /*
        GetComponent<Transform>().Translate(0.0f, 0.01f, 0.0f);
        Debug.Log(GetComponent<Transform>().position);
        */

        if (motions.Count > 0)
        {
            bool finished = motions[0].Animate(this, Time.deltaTime);
            if (finished)
            {
                motions.RemoveAt(0);
            }
        }

        if (ManualDance == true)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                Pose("Both");

            }
            else if (Input.GetMouseButton(0))
            {
                Pose("Left");
            }
            else if (Input.GetMouseButton(1))
            {
                Pose("Right");
            }
            else
            {
                Pose("Normal");
            }
        }
	}
}
