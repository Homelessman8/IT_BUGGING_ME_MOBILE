using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public enum bugType
{
    Cockcroah,
    Spider,
    Snail
}

public class BugAI : MonoBehaviour
{
    //Reference HighScore script
    public HighScore health;
    NavMeshAgent agent;
    [SerializeField]
    //Where we want the AI to go
    Transform target;
    Vector3 randomPosition;
    int lives = 1;
    public bugType bug;
    private List<int> validNumbers = new List<int> { 1, 4, 8,};
    [SerializeField]
    GameObject[] greenSplats;
    [SerializeField]
    GameObject[] yellowSplats;
    [SerializeField]
    GameObject[] orangeSplats;
    [SerializeField]Renderer[] renderers;
    [SerializeField]
    Material hitMaterial, originalMaterial;
    [SerializeField]GameObject[] snailDamange;

    bool randomActive;
    //Set the nav agent component
    //Set the end position as destinatino (it will start to move there)
    void Start()
    {
        health = GameObject.Find("ScoreManager").GetComponent<HighScore>();
        target = GameObject.Find("Destroyer").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
        StartCoroutine(RandomMovement());

        //Set life amount based on Bug Type
        switch(bug)
        {
            case bugType.Cockcroah:
            lives = 1;
            break;

            case bugType.Spider:
            lives = 2;
            break;

            case bugType.Snail:
            lives = 5;
            break;
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Sprite Health bar here
        //When AI is stopped, destroy self.
        if(agent.remainingDistance <= agent.stoppingDistance && !randomActive)
        {
            health.Healthpoint = health.Healthpoint - 1;
            

            //Prevent health from going below zero
            if (health.Healthpoint < 0)
            {
                health.Healthpoint = 0;
            }
            Destroy(this.gameObject);
        }
        else if(agent.remainingDistance <= agent.stoppingDistance && randomActive)
        {
            randomActive = false;
            agent.SetDestination(target.transform.position);
        }
    }

    void FixedUpdate()
    {
        var randomNumber = Random.Range(1, 100);
         if (validNumbers.Contains(randomNumber) && !randomActive)
        {
            StartCoroutine(RandomMovement());
        }
    }
    
    //Draws a yellow line along the agents path
    void OnDrawGizmosSelected()
    {
        var nav = GetComponent<NavMeshAgent>();
        if( nav == null || nav.path == null )
            return;

        var line = this.GetComponent<LineRenderer>();
        if( line == null )
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material( Shader.Find( "Sprites/Default" ) ) { color = Color.yellow };
            line.SetWidth( 0.5f, 0.5f );
            line.SetColors( Color.yellow, Color.yellow );
        }

        var path = nav.path;

        line.SetVertexCount( path.corners.Length );

        for( int i = 0; i < path.corners.Length; i++ )
        {
            line.SetPosition( i, path.corners[ i ] );
        }
    }

    private IEnumerator RandomMovement()
    {
        randomActive = true;
        float randomOffset = Random.Range(-3, 3);
        randomPosition = gameObject.transform.position + transform.forward * 5 + transform.right * randomOffset;
        agent.SetDestination(randomPosition);
        yield return new WaitForSeconds(8);
        randomActive = false;
        agent.SetDestination(target.transform.position);
    }

        public void GetHit()
    {
        Debug.Log("BUG HIT!");
        lives -= 1;
        StartCoroutine(Flash());        
    }

    void OnDestroy()
    {
        switch(bug)
        {
            case bugType.Cockcroah:
            Instantiate(orangeSplats[Random.Range(0, 1)], transform.position + transform.up * 0.1f, transform.rotation);
            break;

            case bugType.Spider:
            Instantiate(greenSplats[Random.Range(0, 1)], transform.position + transform.up * 0.1f, transform.rotation);
            break;

            case bugType.Snail:
            Instantiate(yellowSplats[Random.Range(0, 1)], transform.position + transform.up * 0.1f, transform.rotation);
            break;
        }
    }

    private IEnumerator Flash()
    {
        foreach(Renderer render in renderers)
        {
            render.material = hitMaterial;
        }
        
        yield return new WaitForSeconds(0.1f);

        foreach(Renderer render in renderers)
        {
            if(bug == bugType.Snail)
            {
            if(lives == 4)
            {
                snailDamange[1].SetActive(true);
                snailDamange[0].SetActive(false);
            }
            if(lives == 3)
            {
                snailDamange[2].SetActive(true);
                snailDamange[1].SetActive(false);
            }
            if(lives == 2)
            {
                snailDamange[3].SetActive(true);
                snailDamange[2].SetActive(false);
            }
            if(lives == 1)
            {
                snailDamange[4].SetActive(true);
                snailDamange[3].SetActive(false);
            }
        }
            render.material = originalMaterial;
        }
        
        if(lives == 0)
        {
            Destroy(this.gameObject);
        }
    }
}