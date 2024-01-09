using System.Collections;
// using System.Collections.Generic; // vê se funciona sem essa
using UnityEngine;
using TMPro;
using Random = System.Random;
public class Aranha : MonoBehaviour {
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;
    public float speed, jumpForce;
    public int addJumps, numeroAcertos = 0, qtdMusicas = 0;
    public bool isGrounded;
    public Transform player;
    public Lista listaJogador = new Lista(), listaCerta = new Lista();
    public Lista[] listasCertas = new Lista[5];
    public TextMeshProUGUI textMusic, FinalJogo, Objetivo;
    public Random rand = new Random();
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        char startChar = 'a';
		for (int i = 0; i < listasCertas.Length; i++) {
			listasCertas[i] = new Lista();
			for (int j = 0; j < 5; j++) {
				listasCertas[i].ListaMusicas[j] = startChar.ToString();
				startChar++;
			}
		}
        int generoEscolhido = rand.Next(0, 5);
		listaCerta = listasCertas[generoEscolhido];
		switch (generoEscolhido) {
			case 0:
				Objetivo.text = "Encontre todas as músicas do Radiohead";
				break;
			case 1:
				Objetivo.text = "Encontre todas as músicas de Rock";
				break;
			case 2:
				Objetivo.text = "Encontre todas as músicas Pop";
				break;
			case 3:
				Objetivo.text = "Encontre todas as músicas Gospel";
				break;
			case 4:
				Objetivo.text = "Encontre todas as músicas Sertanejas";
				break;
		}
    }
    void Update() {
        moveX = Input.GetAxisRaw("Horizontal");
        if (player != null) {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
        textMusic.text = $"{qtdMusicas}/5";
    }
    void FixedUpdate() {
        Move();
        Catch();
        MensagemFinal();
        if (isGrounded) {
            addJumps = 1;
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        }
        else {
            if (Input.GetButtonDown("Jump") && addJumps > 0) {
                addJumps--;
                Jump();
            }
        }
    }
    void Move() {
		rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
		if (moveX != 0) {
			transform.eulerAngles = new Vector3(0f, moveX > 0 ? 0f : 180f, 0f);
			anim.SetBool("isRunning", true);
		} else {
			anim.SetBool("isRunning", false);
		}
	}
    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJumping", true);
    }
    void OnCollisionEnter2D(Collision2D collision) {
        isGrounded = collision.gameObject.tag == "Ground";
        anim.SetBool("isJumping", false);
    }
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
    void Catch() {
        if (Input.GetButtonDown("Fire1")) {
            anim.Play("SpiderCatching", -1);
        }
    }
    void MensagemFinal() {
        if (qtdMusicas == 5) {
            anim.SetBool("gotFive", true);
            Invoke("TerminaJogo", 0.3f);
        }
    }
    void TerminaJogo() {
        listaJogador.ComparaMusicas(listaJogador, listaCerta, ref numeroAcertos);
        FinalJogo.text = $"Voce acertou {numeroAcertos} album{(numeroAcertos != 1 ? "s" : "")}!";
        Time.timeScale = 0;
    }
}