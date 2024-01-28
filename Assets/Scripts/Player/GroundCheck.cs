using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Collider2D col;
    public float tam;
    public LayerMask Ground;
    void Start()
    {
        col = GetComponent<Collider2D>();
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
