using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Nessa classe vai randomizar os blocos para criar a lista
    // fazer a rotação das linhas que estão sendo criadas e excluídas
    // Ela vai ficar no gameobject raíz Grid

    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }

    public float SpawnBlockTimer = 7;

    public GameObject LinePrefab;

    private List<GameObject> lineList = new List<GameObject>();

    private int lastLineID = 0;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        StartCoroutine(SpawnBlockLine());
    }

    /// ------- Funções de controle da lista de lines ----------
     
    public void RemoveBlockFromLine(GameObject block) {

        for(int i=0; i<lineList.Count; i++) {

            if(lineList[i].GetComponent<LineInstance>().GetInstanceID() == block.GetComponent<BlocoInstance>().getBlockLineID()) {
                lineList[i].GetComponent<LineInstance>().RemoveBlockFromLine(block);

                // Se a linha está vazia apaga seu gameobject
                if (lineList[i].GetComponent<LineInstance>().getNumOfBlocks() == 0) {
                    Destroy(lineList[i]);
                    lineList.RemoveAt(i);
                }
            }

        }
    }

    /// ------- Funções de criação da grid ----------

    private void createLine() {

        GameObject line = Instantiate(LinePrefab, this.transform.position, LinePrefab.transform.rotation, this.transform);
        int blocksCount = line.GetComponent<LineInstance>().getNumOfBlocks();
        line.GetComponent<LineInstance>().SetLineData(lastLineID, blocksCount);

        lastLineID++;
        lineList.Add(line);
    }

    private GameObject getLinefromId(int ID) {

        GameObject line = null;

        for (int i = 0; i < lineList.Count; i++) {
            if (lineList[i].GetComponent<LineInstance>().GetInstanceID() == ID)
                return line;
        }

        return line;
    }

    IEnumerator SpawnBlockLine() {

        while (!GameManager.Instance.GameOver) {

            createLine();
            yield return new WaitForSeconds(SpawnBlockTimer);
        }
    }

    /// ------- Funções de merge ----------

        // Esse merge check tem que estar no LineInstance 

    // Verifica se tem merges a serem feitos
    // faz isso até que todos os merges da linha foram completados
    public void MergeCheck(int lineID, GameObject block) {

        Debug.Log("avaliando o merge");
        bool temMerge = false; 

        // Pega a linha do bloco acertado
        GameObject line = getLinefromId(lineID);
        //List<GameObject> blocksInLine = line.GetComponent<LineInstance>().

        // Caso 1: Bloco entre blocos
        // Aumenta o valor dele, apaga os dois do lado, puxa todos os vizinhos ao lado um a direita.
        //if (line)

        // Verifica no bloco qual caso que foi feito - precisa receber o bloco? se ele só reolhar a linha várias vezes não serve?
        // recebendo a linha ele avalia se tem dois blocos iguais e vê em qual caso ele entra  (caso o block seja null - cuidado com o caso 3)
        // no caso três olhar sempre dois a frente

        //Caso 1: Se for o entre tres blocos, aumenta o valor dele, apaga os dois ao lado, e se houverem vizinhos de qualquer um dos lados, eles assumem a posicao dos vizinhos

        //Caso 2: Se tiver um vizinho a direita igual, aumenta o valor do vizinho a direita, apaga o da esquerda e se houverem viznhos a esquerda puxa todos eles para um a direita

        //Caso 3: Se tiver um vizinho a esquerda igual, aumenta o valor do vizinho a esquerda, apaga o da direita e se houverem viznhos a direita puxa todos eles para um a esquerda


        // Ao final avalia se existe bloco sozinho na lista e apaga ele - soma pontos
        // Refaz o código se ainda for visto que está dentro de um desses casos ( talvez usar uma bool que se torna true se entra em um dos casos e se for true ele chama de novo o código)


    }

    private void Merge() {

    }

}
