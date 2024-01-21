using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Musica {
  public string nome = "";
  public Sprite sprite;
}
public class Lista {
    public string[] ListaMusicas = new string[5];
    public List<Sprite> ListaSprites = new List<Sprite>();

    public void AddMusica(string musica, Sprite sprite, ref int qtdMusicas) {
        if (!Cheia(qtdMusicas)){
            ListaMusicas[qtdMusicas] = musica;
            ListaSprites.Add(sprite);

            qtdMusicas++;

        }
    }
    public bool Vazia(int qtdMusicas){
        return qtdMusicas == 0;
    }
    public bool Cheia(int qtdMusicas){
        return qtdMusicas == 5;
    }
    public void ComparaMusicas(Lista listaJogador, Lista listaCerta, ref int numeroAcertos, int qtdMusicas)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < qtdMusicas; j++)
            {
                if (listaJogador.ListaMusicas[j] == listaCerta.ListaMusicas[i])
                {
                    numeroAcertos++;
                    break;
                }
            }
        }
    }
    
    public void RemoveMusica(string musica, ref int qtdMusicas)
    {
        int n;
        if(!Vazia(qtdMusicas)){
            n = ProcuraMusica(musica);
            for (int i = n; i < ListaMusicas.Length - 1; i++)
            {
                ListaMusicas[i] = ListaMusicas[i + 1];
            }
            --qtdMusicas;
        }
    }


    public int ProcuraMusica(string musica) {
        for (int i = 0; i < 5; i++){
            if (ListaMusicas[i] == musica) {
                return i;
            }
        }
        return 0;
    }
}