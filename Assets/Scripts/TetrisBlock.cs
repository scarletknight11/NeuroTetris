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
                PlayField.instance.SpawnNewBlock();
            } 
            else
            {
                //UPDATE THE GRID
                PlayField.instance.UpdateGrid(this);
            }

            prevTime = Time.time;
        }

    }

    //check if object touches the ground or another object an within distance and shape
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

        foreach(Transform child in transform)
        {
            Vector3 pos = PlayField.instance.Round(child.position);
            Transform t = PlayField.instance.GetTransformOnGridPos(pos);
            if(t!=null && t.parent !=transform)
            {
                return false;
            }
        }
        return true;
    }
}
