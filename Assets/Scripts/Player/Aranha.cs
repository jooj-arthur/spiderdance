using System.Collections;
using UnityEngine;
using TMPro;
using Random = System.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Aranha : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;

    // configurações de movimento
    public float speed, jumpForce;

    // contadores e flags
    public int numeroAcertos = 0, qtdMusicas = 0, pontuacao;
    public bool teste = true, funcaoExecutada = false, funcaoExecutada2 = false;

    public Transform GroundCheck;

    public LayerMask Ground;

    public GroundCheck gCheck;

    // objetos e componentes
    public Transform player;
    public Lista listaJogador = new Lista(), listaCerta = new Lista();
    public Lista[] listasCertas = new Lista[5];
    public TextMeshProUGUI textMusic, FinalJogo, Objetivo;
    public Random rand = new Random();
    public AudioClip sfxVenceuJogo, sfxPerdeuJogo;
    public AudioController audioController;
    public Image[] imagens = new Image[5];
    public Image song;
    public string mRemovida;
    public Button[] botoes = new Button[7];

    void InicializaListas()
    {
        char startChar = 'a';
        for (int i = 0; i < listasCertas.Length; i++)
        {
            listasCertas[i] = new Lista();
            for (int j = 0; j < 5; j++)
            {
                listasCertas[i].ListaMusicas[j] = startChar.ToString();
                startChar++;
            }
        }
    }

    void Start()
    {
        Debug.Log("Iniciei");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InicializaListas();
        int generoEscolhido = rand.Next(0, 5);
        listaCerta = listasCertas[generoEscolhido];
        string[] objetivos = {
            "Faz eu rir nao, mano", //emo a,b,c,d,e
            "Agora o bicho vai pegar!!!", //rock f,g,h,i,j
            "Me sentindo uma diva!", //pop k,l,m,n,o
            "Nao me chame de normie!", //alternativo, indie p,q,r,s,t
            "Hoje eu quero um dia de sossego, eu quero paz" //sertanejo u,v,w,x,y
        };
        Objetivo.text = objetivos[generoEscolhido];
        foreach (Button botao in botoes)
        {
            botao.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        Catch();
        moveX = Input.GetAxisRaw("Horizontal");
        if (player != null)
        { // essa verificação é necessária?
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
        textMusic.text = $"{qtdMusicas}/5";

        if (teste && qtdMusicas == 5)
        {
            MensagemFinal();
            teste = false;
        }
        if (gCheck.Grounded() && Input.GetButtonDown("Jump"))
            Jump();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (moveX != 0)
        {
            transform.eulerAngles = new Vector3(0f, moveX > 0 ? 0f : 180f, 0f);
        }
        anim.SetBool("isRunning", moveX != 0);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJumping", true);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("isJumping", false);
    }

    void Catch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("SpiderCatching", -1);
        }
    }
    public void MensagemFinal()
    {
        Time.timeScale = 0;
        textMusic.gameObject.SetActive(false);
        song.gameObject.SetActive(false);
        Objetivo.text = "";
        FinalJogo.text = "Deseja remover alguma musica coletada?";
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].gameObject.SetActive(true);
            botoes[i].onClick.AddListener(delegate { task(i); });
        }
        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].sprite = listaJogador.ListaSprites[i];
        }
    }
    public void task(int i)
    {
        Time.timeScale = 1.0f;
        int nErros;
        foreach (Button botao in botoes)
        {
            botao.gameObject.SetActive(false);
        }
        if (i < 5 && !funcaoExecutada2)
        {
            listaJogador.RemoveMusica(listaJogador.ListaMusicas[i], ref qtdMusicas);
            funcaoExecutada2 = true;
        }
        if (!funcaoExecutada)
        {
            listaJogador.ComparaMusicas(listaJogador, listaCerta, ref numeroAcertos, qtdMusicas);
            funcaoExecutada = true;
        }
        nErros = qtdMusicas - numeroAcertos;
        bool venceu = numeroAcertos > 2;
        anim.SetBool(venceu ? "nice" : "bad", true);
        audioController.ToqueSFX(venceu ? sfxVenceuJogo : sfxPerdeuJogo);
        Invoke("TerminaJogo", 5.0f);
        pontuacao = numeroAcertos * 200 - nErros * i;
        if (pontuacao < 0)
            pontuacao = 0;
        FinalJogo.text = $"Voce acertou {numeroAcertos} musica{(numeroAcertos != 1 ? "s" : "")}, e fez {pontuacao} pontos!";
    }
    public void ReiniciarJogo()
    {
        numeroAcertos = 0;
        qtdMusicas = 0;
        pontuacao = 0;
        teste = true;
        funcaoExecutada = false;
        funcaoExecutada2 = false;

        foreach (Button botao in botoes)
        {
            botao.gameObject.SetActive(true);
        }

        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("nice", false);
        anim.SetBool("bad", false);
    }
    void TerminaJogo()
    {
        SceneManager.LoadScene("Menu");
        Invoke("ReiniciarJogo",1.0f);
    }
}