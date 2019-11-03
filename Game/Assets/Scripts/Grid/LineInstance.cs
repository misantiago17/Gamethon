using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInstance : MonoBehaviour
{
    // Essa classe vai fazer o management dos blocos dentro da lista e a lista em si
    // retira da lista
    // destroi a lista
    // seta a lista com os blocos
    // reorganiza os blocos dentro da lista (parte do merge)
    // esse objeto vai fazer parte do gameobject de lista

    public GameObject BlockPrefab;
    public int MaxNumOfBlocksPerLine = 7;
    public float SpawnOffsetFromTop = 0.5f;

    // A lista é de gameObejcts e não apenas de BlocoInstance 
    // porque essa classe precisa de acesso ao transform do objeto
    // para setar a posicao inicial dele na tela
    private List<GameObject> line = new List<GameObject>();
    private List<int> blocksPattern = new List<int>();

    private Line lineData;
    private int currentNumOfblocks = 0;

    /// ------- Funções de básicas ----------

    private void Start() {
        createBlocks();
    }

    /// ------- Funções de criação da linha ---------- 

    // toda vez que uma nova linha é criada essa função será chamada
    public void SetLineData(int lineID, int numOfBlocks) {
        lineData.setID(lineID);
        lineData.setNumOfBlocks(numOfBlocks);
    }

    // static para o prefab - a resposta sempre será igual
    public Vector3 SpawnLinePosition() {

        int height = Screen.height;
        int width = Screen.width;
        int blockSize = width / MaxNumOfBlocksPerLine;

        Vector3 LinePos = Camera.main.ScreenToWorldPoint(new Vector3(0, (float)height - SpawnOffsetFromTop, 0));

        return LinePos;
    }

    /// ------- Funções de criação dos blocos dentro da linha ---------- 

    // precisa gerar o padrão da linha e depois com essa lista gerar os blocos na linha
    private void createBlocks() {

        // To Do
        // Fazer isso instanciando bloquinhos
        /*GameObject line = Instantiate(LinePrefab, lineInitialPos, LinePrefab.transform.rotation, this.transform);
        int blocksCount = line.GetComponent<LineInstance>().getNumOfBlocks();
        line.GetComponent<LineInstance>().SetLineData(lastLineID, blocksCount);
        lineList.Add(line);*/

    }

    public int getNumOfBlocks() {
        return currentNumOfblocks;
    }

}
