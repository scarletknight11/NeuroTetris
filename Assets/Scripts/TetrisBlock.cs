using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour {

    float prevTime;
    float fallTime = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time - prevTime > fallTime)
        {
            transform.position += Vector3.down;
            //DELETE LAYER IF POSSIBLE

            if (!CheckValidMove())
            {
                transform.position += Vector3.up;
                
                enabled = false;
                //CREATE A NEW TETRIS BLOCK
            } 
            else
            {
                //UPDATE THE GRID

            }

            prevTime = Time.time;
        }

    }

    bool CheckValidMove()
    {
        foreach(Transform child in transform)
        {
            Vector3 pos = PlayField.instance.Round(child.position);
            if(!PlayField.instance.CheckInsideGrid(pos))
            {
                return false;
            }
        }

        return true;
    }
}
