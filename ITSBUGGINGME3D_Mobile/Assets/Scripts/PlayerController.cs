// using one sound for all bugs


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    //Set the raycast length 
    public float rayLength;
    //Choose which layer to target
    public LayerMask layermask;
    public HighScore livescore;

    //Reference particle effect and click effect
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    //Reference the damage flash
    public DamageFlash flashHit;

    // Reference to the audio clip
    [SerializeField] AudioClip destroySound;

    // Array to hold the audio clips
    public AudioClip[] destroySounds;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Initialize the AudioSource component
    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource component is not attached, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource component
        //audioSource.clip = destroySound;
    }

    private void Update()
    {
        //Check if left mouse button is pressed and ask EventSystems if the cursor is over a UI object
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            //Use camera to create ray and give it mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Shoot ray
            if (Physics.Raycast(ray, out hit, rayLength, layermask))
            {
                //Cast hit effect
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }

                //Cause damage flash
                //refer to damage flash script

                //Debug.Log(hit.collider.name);
                //Destroy game object 
                //Destroy(hit.transform.gameObject);
                hit.transform.GetComponent<BugAI>().GetHit();
                livescore.Score++;

                // Play the destroy sound
                if (destroySounds.Length > 0)
                {
                    // Randomly select one of the audio clips from the array
                    int randomIndex = Random.Range(0, destroySounds.Length);
                    audioSource.clip = destroySounds[randomIndex];
                    audioSource.Play();
                }
            }

        }
    }

}
