using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D col;
    public float tam;
    public LayerMask Ground;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Grounded(){
        bool G = Physics2D.OverlapCapsule(transform.position,new Vector2(0,tam),CapsuleDirection2D.Horizontal,0,Ground);
        Debug.Log(G);
        return G;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ou qualquer cor que você preferir
        Gizmos.DrawWireSphere(transform.position, tam);
    }
}
