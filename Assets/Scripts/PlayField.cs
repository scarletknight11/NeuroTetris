using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour {

    public static PlayField instance;

    public int gridSizeX, gridSizeY, gridSizeZ;
    [Header("Blocks")]
    public GameObject[] blockList;
    public GameObject[] ghostList;


    [Header("Playfield Visuals")]
    public GameObject bottomPlane;
    public GameObject N, S, W, E;

    int randomIndex;

    public Transform[,,] theGrid;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theGrid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
        //CalculatePreview();
        SpawnNewBlock();

    }

    public Vector3 Round(Vector3 vec)
    {
        return new Vector3(Mathf.RoundToInt(vec.x),
                        Mathf.RoundToInt(vec.y),
                        Mathf.RoundToInt(vec.z));
    }

    public bool CheckInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridSizeX &&
            (int)pos.z >= 0 && (int)pos.z < gridSizeZ &&
            (int)pos.y >= 0); 
    }

    //update grid method for position of tetris block
    //using nested for loops
    public void UpdateGrid(TetrisBlock block)
    {
        //if x is less then max number of tetris block on grid of x axis continue to keep generating
        //Delete Possible Parent Objects
        for (int x = 0; x < gridSizeX; x++)
        {
            //continue to generate blocks on till max grid is filled
            for (int z = 0; z < gridSizeZ; z++)
            {
                //continue to have blocks fall from y axis till y axis is all filled up
                for(int y = 0; y < gridSizeY; y++)
                {
                    if(theGrid[x,y,z] != null)
                    {
                        //if grid fills all x,y,z coordinates
                        if (theGrid[x, y, z].parent == block.transform)
                        {
                            theGrid[x, y, z] = null;
                        }
                    }
                }
            }
        }

        //for each child of block generated, fill in all child objects
        foreach(Transform child in block.transform)
        {
            //be able to rotate position of gameobject
            Vector3 pos = Round(child.position);
            //if pos of y is less then gridsize od y axis
            if(pos.y < gridSizeY)
            {
                //gameobject of block is stuck within that x,y,z grid axis
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
            }
        }
    }

    public Transform GetTransformOnGridPos(Vector3 pos)
    {
        if(pos.y > gridSizeY-1)
        {
            return null;
        }
        else
        {
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    //spawn new block one original one lands and fits the grid
    public void SpawnNewBlock()
    {
        Vector3 spawnPoint = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                            (int)transform.position.y + gridSizeY,
                                            (int)(transform.position.z + (float)gridSizeZ / 2));
        //int randomIndex = Random.Range(0, blockList.Length);

        //SPAWN THE BLOCK
        GameObject newBlock = Instantiate(blockList[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        //GHOST
        GameObject newGhost = Instantiate(ghostList[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        newGhost.GetComponent<GhostBlock>().SetParent(newBlock);

        CalculatePreview();
        PreView.instance.ShowPreview(randomIndex);
    }

    public void CalculatePreview()
    {
       randomIndex = Random.Range(0, blockList.Length);
    }

    public void DeleteLayer()
    {
        //repeat iteration top to bottom
        for (int y = gridSizeY-1; y >= 0; y--)
        {
            //CHECK FULL LAYER
            if(CheckFullLayer(y))
            {
                //DELETE ALL BLOCK
                DeleteLayerAt(y);
                //MOVE ALL DOWN BY 1
                MoveAllLayerDown(y);
            }
        }
    }

    bool CheckFullLayer(int y)
   {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if(theGrid[x,y,z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void DeleteLayerAt(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Destroy(theGrid[x, y, z].gameObject);
                theGrid[x, y, z] = null;
            }
        }
    }

    void MoveAllLayerDown(int y)
    {
        for (int i = y; i< gridSizeY; i++)
        {
            MoveOneLayerDown(i);
        }
    }

    void MoveOneLayerDown(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if(theGrid[x,y,z] != null)
                {
                    theGrid[x, y - 1, z] = theGrid[x, y, z];
                    theGrid[x, y, z] = null;
                    theGrid[x, y - 1, z].position += Vector3.down; 
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if(bottomPlane != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeZ / 10);
            bottomPlane.transform.localScale = scaler;

            //REPOSITION
            bottomPlane.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                         transform.position.y, 
                                                         transform.position.z + (float)gridSizeZ / 2);
            //RETILE MATERIAL
            bottomPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeZ);
        }

        if (N != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            N.transform.localScale = scaler;

            //REPOSITION
            N.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                         transform.position.y + (float)gridSizeY/2,
                                                         transform.position.z + gridSizeZ);
            //RETILE MATERIAL
            N.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if (S != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            S.transform.localScale = scaler;

            //REPOSITION
            S.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                         transform.position.y + (float)gridSizeY / 2,
                                                         transform.position.z);
            //RETILE MATERIAL
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }


        if (E != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            E.transform.localScale = scaler;

            //REPOSITION
            E.transform.position = new Vector3(transform.position.x + gridSizeX,
                                                         transform.position.y + (float)gridSizeY / 2,
                                                         transform.position.z + (float)gridSizeZ /2);
            //RETILE MATERIAL
            E.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }

        if (W != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            W.transform.localScale = scaler;

            //REPOSITION
            W.transform.position = new Vector3(transform.position.x,
                                                         transform.position.y + (float)gridSizeY / 2,
                                                         transform.position.z + (float)gridSizeZ / 2);
            //RETILE MATERIAL
            //W.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }
    }
    
}
