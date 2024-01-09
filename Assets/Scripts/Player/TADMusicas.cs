using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Musica {
  public string nome = "";
}
public class Lista {
    public string[] ListaMusicas = new string[5];
    public void AddMusica(string musica, ref int qtdMusicas) {
        if (!Cheia(qtdMusicas)){
            ListaMusicas[qtdMusicas] = musica;
            qtdMusicas++;
        }
    }
    public bool Vazia(int qtdMusicas){
        return qtdMusicas == 0;
    }
    public bool Cheia(int qtdMusicas){
        return qtdMusicas == 5;
    }
    public void ComparaMusicas(Lista listaJogador, Lista listaCerta, ref int numeroAcertos) {
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                if (listaJogador.ListaMusicas[i] == listaCerta.ListaMusicas[j]) {
                    numeroAcertos++;
                    break;
                }
            }
        }
    }
}
