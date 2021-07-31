using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour {

    public GameObject bottomPlane;
    public GameObject N, S, W, E;

    public int gridSizeX, gridSizeY, gridSizeZ;
    public Transform[,,] theGrid;

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
