using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enableGame;

public class TetrisBlock : MonoBehaviour {

    float prevTime;
    float fallTime = 1f;
    egFloat fall = 1f;

    void Awake()
    {
        VariableHandler.Instance.Register(ParameterStrings.STARTING_SPEED, fall);
    }

    void Start()
    {
        ButtonInput.instance.SetActiveBlock(gameObject, this);
        fallTime = GameManager.instance.ReadFallSpeed();
        if(!CheckValidMove())
        {
            GameManager.instance.SetGameIsOver();
        }
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
                //DELETE LAYER IF POSSIBLE
                PlayField.instance.DeleteLayer();
                enabled = false;
                //CREATE A NEW TETRIS BLOCK
                if(!GameManager.instance.ReadGameIsOver())
                {
                    PlayField.instance.SpawnNewBlock();
                }
            } 
            else
            {
                //UPDATE THE GRID
                PlayField.instance.UpdateGrid(this);
            }

            prevTime = Time.time;
        }

            if(Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                //TrafficLight.sendRed();
                SetInput(Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                SetInput(Vector3.right);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                 SetInput(Vector3.forward);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SetInput(Vector3.back);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                //SetInput(Vector3.forward);
                SetRotationInput(new Vector3(90, 0, 0));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                //SetInput(Vector3.back);
                SetRotationInput(new Vector3(-90, 0, 0));
            }
    }

    public void SetInput(Vector3 direction)
    {
        transform.position += direction;
        if(!CheckValidMove())
        {
            transform.position -= direction;
        } 
        else
        {
            PlayField.instance.UpdateGrid(this);
        }
    }

    public void SetRotationInput(Vector3 rotation)
    {
        transform.Rotate(rotation, Space.World);
        if(!CheckValidMove())
        {
            transform.Rotate(-rotation, Space.World);
        }
        else
        {
            PlayField.instance.UpdateGrid(this);
        }
    }

    //check if object touches the ground or another object an within distance and shape
    bool CheckValidMove()
    {
        foreach(Transform child in transform)
        {
            Vector3 pos = PlayField.instance.Round(child.position);
            if (!PlayField.instance.CheckInsideGrid(pos))
            {
                TrafficLight.sendGreen();
                return false;
            }
        }

        foreach(Transform child in transform)
        {
            Vector3 pos = PlayField.instance.Round(child.position);
            Transform t = PlayField.instance.GetTransformOnGridPos(pos);
            if(t!=null && t.parent !=transform)
            {
                TrafficLight.sendYellow();
                return false;
            }
        }
        return true;
    }

    public void SetSpeed()
    {
       fallTime = 0.1f;
    }
}
