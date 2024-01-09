using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicaNaCena : MonoBehaviour {
    [SerializeField] private Musica musica;
    public bool canGrab = false;
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
        if (canGrab && Input.GetButtonDown("Fire1") && other.CompareTag("Player")) {
            Debug.Log("pegou");
            aranha.listaJogador.AddMusica(musica.nome, ref aranha.qtdMusicas);
            Destroy(this.gameObject);
        }
    }
}