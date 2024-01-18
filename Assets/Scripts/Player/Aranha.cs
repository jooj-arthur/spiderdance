using System.Collections;
// using System.Collections.Generic; // vê se funciona sem essa
using UnityEngine;
using TMPro;
using Random = System.Random;
using UnityEngine.UI;
public class Aranha : MonoBehaviour {
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;
    public float speed, jumpForce;
    public int numeroAcertos = 0, qtdMusicas = 0, pontuacao, removeu = 0;
    public bool isGrounded, teste = true;
    public Transform player;
    public Lista listaJogador = new Lista(), listaCerta = new Lista();
    public Lista[] listasCertas = new Lista[5];
    public TextMeshProUGUI textMusic, FinalJogo, Objetivo;
    public Random rand = new Random();
    public AudioClip sfxVenceuJogo, sfxPerdeuJogo;
    public AudioController audioController;
    public Image q1,q2,q3,q4,q5;
    public string mRemovida;
    public Button b1,b2,b3,b4,b5;

    public bool funcaoExecutada = false;


    
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
				Objetivo.text = "Encontre todas as musicas do Radiohead";
                //a,b,c,d,e
				break;
			case 1:
				Objetivo.text = "Encontre todas as musicas de Rock";
                //f,g,h,i,j
				break;
			case 2:
				Objetivo.text = "Encontre todas as musicas Pop";
                //k,l,m,n,o
				break;
			case 3:
				Objetivo.text = "Encontre todas os musicas Gospel";
                //p,q,r,s,t
				break;
			case 4:
				Objetivo.text = "Encontre todas as musicas Sertanejas";
                //u,v,w,x,y
				break;
		}

        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        b4.gameObject.SetActive(false);
        b5.gameObject.SetActive(false);

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
        if(teste == true && qtdMusicas == 5){
            MensagemFinal();
            teste = false;
        }
        
        if (isGrounded) {
            if (Input.GetButtonDown("Jump")) {
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
        /*
        if (numeroAcertos > 2)
        {
            audioController.ToqueSFX(sfxVenceuJogo);
            anim.SetBool("nice", true);
        }
        else
        {
            audioController.ToqueSFX(sfxPerdeuJogo);
            anim.SetBool("bad", true);
        }
        */
        Invoke("TerminaJogo", 0.3f);
    }
    public void TerminaJogo() {
        b1.gameObject.SetActive(true);
        b2.gameObject.SetActive(true);
        b3.gameObject.SetActive(true);
        b4.gameObject.SetActive(true);
        b5.gameObject.SetActive(true);
        b1.onClick.AddListener(delegate {task(0);});
        b2.onClick.AddListener(delegate {task(1);});
        b3.onClick.AddListener(delegate {task(2);});
        b4.onClick.AddListener(delegate {task(3);});
        b5.onClick.AddListener(delegate {task(4);});
        q1.sprite = listaJogador.ListaSprites[0];
        q2.sprite = listaJogador.ListaSprites[1];
        q3.sprite = listaJogador.ListaSprites[2];
        q4.sprite = listaJogador.ListaSprites[3];
        q5.sprite = listaJogador.ListaSprites[4];
        Time.timeScale = 0;

        /*
        pontuacao = (numeroAcertos * 200);
        FinalJogo.text = $"Voce acertou {numeroAcertos} musica{(numeroAcertos != 1 ? "s" : "")}, e fez {pontuacao} pontos!";
        Time.timeScale = 0;
        */
    }

    public void task(int i){
        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        b4.gameObject.SetActive(false);
        b5.gameObject.SetActive(false);
        int nErros;
        listaJogador.RemoveMusica(listaJogador.ListaMusicas[i], ref qtdMusicas);
        if (!funcaoExecutada)
        {
            listaJogador.ComparaMusicas(listaJogador, listaCerta, ref numeroAcertos, ref removeu);
            funcaoExecutada = true;
        }
        nErros = (5-(numeroAcertos+removeu));
        if (numeroAcertos > 2)
        {
            audioController.ToqueSFX(sfxVenceuJogo);
            anim.SetBool("nice", true);
            Invoke("terminaa", 0.3f);
        }
        else
        {
            audioController.ToqueSFX(sfxPerdeuJogo);
            anim.SetBool("bad", true);
            Invoke("terminaa", 0.3f);
        }
        pontuacao = (numeroAcertos * 200) - (nErros*25);
        FinalJogo.text = $"Voce acertou {numeroAcertos} musica{(numeroAcertos != 1 ? "s" : "")}, e fez {pontuacao} pontos!";

    }

    void terminaa(){
        Time.timeScale = 0;
    }


    
    
}
