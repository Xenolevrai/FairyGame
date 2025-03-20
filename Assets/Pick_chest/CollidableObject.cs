using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollidableObject : MonoBehaviour
{
    private bool close = true;
    private Collider2D z_Collider;
    [SerializeField]
    private ContactFilter2D z_Filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);
    public Sprite ouvert;
    public Sprite ferme;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject pressE; 

    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ferme;
        z_Collider = GetComponent<Collider2D>();
        if (pressE != null)
        {
            pressE.SetActive(false); 
        }
    }

    protected virtual void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);

        if (z_CollidedObjects.Count > 0)
        {
            OnCollided(z_CollidedObjects[0].gameObject);
        }
        else
        {
            OnNotCollided();
        }
    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        if (pressE != null)
        {
            pressE.SetActive(true);
            Debug.Log("est-ce-que c'est activé ?");

        }
        Debug.Log("frrrr t'abuses ?");
        if (Input.GetKeyDown(KeyCode.E))
        {
            ouver();
        }
    }

    protected virtual void OnNotCollided()
    {
        if (pressE != null)
        {
            pressE.SetActive(false);
        }
    }
    float GetRandomExcludingRange(float min, float max, float excludeMin, float excludeMax)
    {
        float randomValue;
        do
        {
            randomValue = UnityEngine.Random.Range(min, max);
        }
        while (randomValue >= excludeMin && randomValue <= excludeMax); // Répète tant que la valeur est dans la plage interdite

        return randomValue;
    }
    public void ouver()
    {
        if (close == false)
        {
            
        }
        else
        {
            Debug.Log("ahhhhhhhhhhhhhhhhhhhh");
            spriteRenderer.sprite = ouvert;

            if (item1 != null && item2 != null && item3 != null && item4 != null)
            {
                Debug.Log("premier");
                System.Random random = new System.Random();
                int i = random.Next(2, 5);
                while (i > 0)
                {
                    int nombre = random.Next(1, 5);
                    float spawn = GetRandomExcludingRange(-1.5f, 1.5f, -0.3f, 0.3f);
                    float spawn2 = GetRandomExcludingRange(-1.5f, 1.5f, -0.3f, 0.3f);
                    float spawn3 = 0f; 
                    Vector3 spawnPosition = transform.position + new Vector3(spawn, spawn2, spawn3);

                    if (nombre == 1)
                    {
                        Instantiate(item1, spawnPosition, Quaternion.identity);
                        Debug.Log("premier");
                    }
                    if (nombre == 2)
                    {
                        Instantiate(item2, spawnPosition, Quaternion.identity);
                        Debug.Log("deuxieme");
                    }
                    if (nombre == 3)
                    {
                        Instantiate(item3, spawnPosition, Quaternion.identity);
                        Debug.Log("troisieme");
                    }
                    if (nombre == 4)
                    {
                        Instantiate(item4, spawnPosition, Quaternion.identity);
                        Debug.Log("quatrieme");
                    }

                    i--;
                }
            }
            close = false;
        }
    }
}