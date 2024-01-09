using System;

#define MAX 10
class Program
{

    typedef struct{
        string ListaMusicas[MAX];
        int nMusicas;
    }Lista;


    static void Cria(Lista *L)
    {
        L->nMusicas = 0;
    }

    static bool Cheia(Lista *L)
    {
        if (L->nMusicas == MAX)
        return true;
        else
        return false;
    }

    static bool Vazia(Lista *L)
    {
        if (L->nMusicas == 0)
        return true;
        else
        return false;
    }

    static void Insere(string musica, Lista *L)
    {
        if (!Cheia(L))
        {
            L->nMusicas++;
            L->ListaMusicas[L->nMusicas] = musica;
        }
    }

    static void Retira(string *musica, Lista *L)
    {
        if (!Vazia(L))
        {
            musica = L->ListaMusicas[L->nMusicas];
            L->nMusicas--;
        }
    }

    static void Compara(Lista L1, Lista L2, int *nErradas)
    {
        int nCertas = 0;
        for (int i = 1; i++; i<=L1->nMusicas)
        {
            for(int j = 1; j++; j<=L2->nMusicas)
            {
                if (L1->ListaMusicas[i] == L2->ListaMusicas[j])
                {
                    nCertas++;
                    break;
                }
            }
        }
        *nErradas = L1->nMusicas - nCertas;
    }
    static void Main()
    {
        Console.WriteLine("Olá, mundo!");
    }
}