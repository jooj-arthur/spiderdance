using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicaNaCena : MonoBehaviour {
    [SerializeField] private Musica musica;
    public bool canGrab = false;
    public float amp, freq;
    Vector3 initpos;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canGrab = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canGrab = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        Aranha aranha = other.GetComponent<Aranha>();
        if (Input.GetMouseButtonDown(0) && other.CompareTag("Player")) {
            Debug.Log("pegou");
            aranha.listaJogador.AddMusica(musica.nome, musica.sprite, ref aranha.qtdMusicas);
            Destroy(this.gameObject);
        }
    }

    private void Start(){
        initpos = transform.position;
    }

    private void Update(){
    transform.position = new Vector3(initpos.x,Mathf.Sin(Time.time * freq) * amp + initpos.y,0);
    }
    

}
