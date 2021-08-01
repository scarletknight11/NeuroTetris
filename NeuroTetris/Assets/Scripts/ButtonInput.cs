using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour {

    public static ButtonInput instance;

    public GameObject[] rotateCanvases;
    public GameObject moveCanvas;

    GameObject activeBlock;
    TetrisBlock activeTetris;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void RepositionToActiveBlock()
    {
        if(activeBlock != null)
        {
            transform.position = activeBlock.transform.position;
        }
    }

    public void SetActiveBlock(GameObject block, TetrisBlock tetris)
    {
        activeBlock = block;
        activeTetris = tetris;
    }

    // Update is called once per frame
    void Update()
    {
        RepositionToActiveBlock();
    }

    public void MoveBlock(string direction)
    {
        if (activeBlock != null)
        {
            if(direction == "left")
            {
                activeTetris.SetInput(Vector3.left);
            }
            if (direction == "right")
            {
                activeTetris.SetInput(Vector3.right);
            }
            if (direction == "forward")
            {
                activeTetris.SetInput(Vector3.forward);
            }
            if (direction == "back")
            {
                activeTetris.SetInput(Vector3.back);
            }
        }
    }

    public void SetHighSpeed()
    {
        activeTetris.SetSpeed();
        TrafficLight.sendRed();
    }
}
