using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    GameObject plane;
    [SerializeField]
    Material[] groundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        plane = this.gameObject;
        plane.transform.GetComponent<Renderer>().material = groundMaterial[Random.Range(0, groundMaterial.Length)];
    }
}
