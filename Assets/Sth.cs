using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sth : MonoBehaviour
{
    public SpriteRenderer r;

    public float abc { get; set; }

    public void Start()
    {
        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("renderer: " + r.bounds);
            Debug.Log("sprite: " + r.sprite.bounds);
        }
    }
}
