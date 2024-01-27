using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicaNaCena : MonoBehaviour {
    [SerializeField] private Musica musica;
    public bool canGrab = false;
    public float amp, freq;
    Vector3 initpos;
    Aranha aranha;
    private void OnTriggerEnter2D(Collider2D other) {
        aranha = other.GetComponent<Aranha>();
        if (other.CompareTag("Player")) {
            canGrab = true;
            GetComponent<SpriteRenderer>().color=Color.yellow;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        aranha = null;
        if (other.CompareTag("Player")) {
            canGrab = false;
            GetComponent<SpriteRenderer>().color=Color.white;
        }
    }

    private void Start(){
        initpos = transform.position;
    }

    private void Update(){
    transform.position = new Vector3(initpos.x,Mathf.Sin(Time.time * freq) * amp + initpos.y,0);

    if (Input.GetButtonDown("Fire1") && canGrab) {
            Debug.Log("pegou");
            aranha.listaJogador.AddMusica(musica.nome, musica.sprite, ref aranha.qtdMusicas);
            Destroy(this.gameObject);
        }

    }
    

}
