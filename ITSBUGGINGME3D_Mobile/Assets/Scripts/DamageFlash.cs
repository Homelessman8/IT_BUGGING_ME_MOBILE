using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color origColor;
    float flashTime = 0.15f;

    // Start is called before the first frame update
    void Start()
    { 
        meshRenderer = GetComponent<MeshRenderer>();
        //fill mesh renderer with original color
        origColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FlashStart();
        //}
    }

    void FlashStart()
    {
        meshRenderer.material.color = Color.white;
        Invoke("FlashStop", flashTime);   
    }

    void FlashStop()
    {
        meshRenderer.material.color = origColor;
    }



}
