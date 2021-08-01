using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreView : MonoBehaviour {

    public static PreView instance;

    public GameObject[] previewBlocks;

    GameObject currentActive;

    void Awake()
    {
        instance = this;
    }

    public void ShowPreview(int index)
    {
        Destroy(currentActive);

        currentActive = Instantiate(previewBlocks[index], transform.position, Quaternion.identity) as GameObject;
    }

}
