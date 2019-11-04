using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInstance: MonoBehaviour
{
    // Essa classe vai fazer o management dos blocos dentro da lista e a lista em si
    // retira da lista
    // destroi a lista
    // seta a lista com os blocos
    // reorganiza os blocos dentro da lista (parte do merge)
    // esse objeto vai fazer parte do gameobject de lista

    public GameObject BlockPrefab;
    public int MaxNumOfBlocksPerLine = 7;
    public float SpawnOffsetFromTop = 1.5f;
    public float VelocityLinesFall = 0.1f;

    // A lista é de gameObejcts e não apenas de BlocoInstance 
    // porque essa classe precisa de acesso ao transform do objeto
    // para setar a posicao inicial dele na tela
    private List<GameObject> blockList = new List<GameObject>();
    private List<int> blocksPattern = new List<int>();
    private List<float> blocksXPosition = new List<float>();

    private Line lineData;
    private int currentNumOfblocks = 0;

    /// ------- Funções de básicas ----------

    private void Start() {

        GenerateBlockPositionsInLine();
        createBlocks();
        this.transform.localPosition = SpawnLinePosition();
        StartCoroutine(MakeItFall());
    }

    /// ------- Funções de criação da linha ---------- 

    // toda vez que uma nova linha é criada essa função será chamada
    public void SetLineData(int lineID, int numOfBlocks) {

        if (lineData == null)
            lineData = new Line();

        lineData.setID(lineID);
        lineData.setNumOfBlocks(numOfBlocks);
    }

    // static para o prefab - a resposta sempre será igual
    public Vector3 SpawnLinePosition() {

        int height = Screen.height;
        int width = Screen.width;

        Vector3 LinePos = Camera.main.ScreenToWorldPoint(new Vector3(0, (float) height, 0));
        Vector3 pos = new Vector3(0, LinePos.y - SpawnOffsetFromTop, 0);

        return pos;
    }

    /// ------- Funções de controle da lista de blocos ----------

    public void RemoveBlockFromLine(GameObject block) {

        int index = block.GetComponent<BlocoInstance>().getBlockLineIndex();

        if (blockList[index] == block) {
            blockList.RemoveAt(index);
            blocksPattern[index] = 0;
            int num = lineData.getNumOfBlocks() - 1;
            lineData.setNumOfBlocks(num);
        }
    }

    public void UpdateBlockPatternValue(int value, int index) {
        blocksPattern[index] = value;
    }

    /// ------- Funções de posicionamento dos blocos dentro da linha ---------- 

    // Pega o numero de blocos que vão estar presentes na linha e divide o valor de x entre esses blocos
    private void GenerateBlockPositionsInLine() {

        float size = BlockPrefab.GetComponent<Renderer>().bounds.size.x;
        float shiftBack = (size * (-MaxNumOfBlocksPerLine + 1)) / 2;

        // Gera todos os blocos a partir do centro
        // Diminui deles uma quantidade fixa relativa a quantidade de blocos na linha
        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {
            blocksXPosition.Add(i * size + shiftBack);
        }

    }


    /// ------- Funções de criação dos blocos dentro da linha ---------- 

    // precisa gerar o padrão da linha e depois com essa lista gerar os blocos na linha
    private void createBlocks() {

        createPattern();

        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {

            if (blocksPattern[i] != 0) {

                // set block x position in line
                Vector3 blockPosition = new Vector3(blocksXPosition[i], 0, 0);
                GameObject block = Instantiate(BlockPrefab, blockPosition, this.transform.rotation, this.transform);
                int value = blocksPattern[i];
                block.GetComponent<BlocoInstance>().SetBlockData(value, lineData.getID(), i);
                blockList.Add(block);
            }
        }

    }

    public int getNumOfBlocks() {
        return currentNumOfblocks;
    }

    /// ------- Funções de criação dos padrões dos blocos na linha ---------- 

    private void createPattern() {

        randomizePattern();
        randomizeValue();
    }

    // randomiza a presença dos blocos na linha sem que haja um bloco sozinho
    private void randomizePattern() {

        // Randomiza onde terá bloco ou não
        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {
            int exists = Random.Range(0, 2);
            blocksPattern.Add(exists);

            if (exists == 1) currentNumOfblocks++;
        }

        // verifica se nao tem nenhum sozinho
        // Por definição, adiciona se encontra nas pontas e retira se encontra sozinho no meio
        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {

            // Primeiro bloco
            if (i == 0) {
                if (blocksPattern[i] == 1 && blocksPattern[i + 1] == 0)
                    blocksPattern[i + 1] = 1;

            // Bloco no final
            } else if (i == MaxNumOfBlocksPerLine - 1) {
                if (blocksPattern[i] == 1 && blocksPattern[i - 1] == 0)
                    blocksPattern[i - 1] = 1;

            // Bloco está no meio
            } else {
                if (blocksPattern[i] == 1 && blocksPattern[i - 1] == 0 && blocksPattern[i + 1] == 0) {

                    int choose = Random.Range(0, 2);

                    if (choose == 0)
                        blocksPattern[i - 1] = 1;
                    else
                        blocksPattern[i + 1] = 1;
                }
            }
        }


    }

    // sorteia o valor de cada bloco presente na linha sem que haja dois de mesmo valor sendo vizinhos
    private void randomizeValue() {

        int[] possibleValues = { 1, 2, 4, 8};

        int index = 0;
        int randomValue = 1;

        // Randomiza valores para os bloquinhos
        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {

            if (blocksPattern[i] != 0) {
                index = Random.Range(0, possibleValues.Length);
                randomValue = possibleValues[index];

                blocksPattern[i] = randomValue;
            }
        }

        // Não permite que tenha dois numero de valores iguais um do lado do outro
        for (int i = 0; i < MaxNumOfBlocksPerLine; i++) {

            // Primeiro bloco
            if (i == 0) {
                if (blocksPattern[i] == blocksPattern[i + 1]) {

                    while (blocksPattern[i] == blocksPattern[i + 1]) {
                        index = Random.Range(0, possibleValues.Length);
                        randomValue = possibleValues[index];

                        blocksPattern[i] = randomValue;
                    }
                }

            // Bloco no final
            } else if (i == MaxNumOfBlocksPerLine - 1) {
                if (blocksPattern[i] == blocksPattern[i - 1]) {

                    while (blocksPattern[i] == blocksPattern[i - 1]) {
                        index = Random.Range(0, possibleValues.Length);
                        randomValue = possibleValues[index];

                        blocksPattern[i] = randomValue;
                    }
                }

            // Bloco está no meio
            } else {
                if (blocksPattern[i] == blocksPattern[i - 1] || blocksPattern[i] == blocksPattern[i + 1]) {
                    while (blocksPattern[i] == blocksPattern[i - 1] || blocksPattern[i] == blocksPattern[i + 1]) {
                        index = Random.Range(0, possibleValues.Length);
                        randomValue = possibleValues[index];

                        blocksPattern[i] = randomValue;
                    }
                }
            }
        }

    }

    /// ------- Funções de merge ----------
    /// 

    // Verifica se tem merges a serem feitos
    // faz isso até que todos os merges da linha foram completados
    public void MergeCheck(int blockIndex) {

        Debug.Log("avaliando o merge");

        BlocoInstance blockInfo = blockList[blockIndex].GetComponent<BlocoInstance>();

        // Caso não tenha um merge ele para a função recursiva
        bool temMerge = false;

        // Caso 4: Bloco chegou no ultimo valor
        // Puxa todos os blocos à esquerda 1 à direita
        if (blocksPattern[blockIndex] == blockInfo.MaxBlockValue) {

            Merge(blockIndex, true);

        } else {

            //Caso 1: Se for o entre tres blocos, aumenta o valor dele, apaga os dois ao lado, e se houverem vizinhos de qualquer um dos lados, eles assumem a posicao dos vizinhos

            //Caso 2: Se tiver um vizinho a direita igual, aumenta o valor do vizinho a direita, apaga o da esquerda e se houverem viznhos a esquerda puxa todos eles para um a direita

            //Caso 3: Se tiver um vizinho a esquerda igual, aumenta o valor do vizinho a esquerda, apaga o da direita e se houverem viznhos a direita puxa todos eles para um a esquerda

        }




        // Caso 1: Bloco entre blocos
        // Aumenta o valor dele, apaga os dois do lado, puxa todos os vizinhos ao lado um a direita.
        //if (line)

        // Verifica no bloco qual caso que foi feito - precisa receber o bloco? se ele só reolhar a linha várias vezes não serve?
        // recebendo a linha ele avalia se tem dois blocos iguais e vê em qual caso ele entra  (caso o block seja null - cuidado com o caso 3)
        // no caso três olhar sempre dois a frente




        // Ao final avalia se existe bloco sozinho na lista e apaga ele - soma pontos
        // Refaz o código se ainda for visto que está dentro de um desses casos ( talvez usar uma bool que se torna true se entra em um dos casos e se for true ele chama de novo o código)


    }

    // Faz o merge lentamente amem
    private void Merge(int blocoIndex, bool pullRight) {

        // Todos os blocos a esquerda andam 1 a direita e substituiem a posicao do indice em diante
        if (pullRight) {
            // Chama a corotina para fazer esse merge
        } else {

        }

    }


    /// ------- Funções de fazer a linha cair ---------- 

    // Faz as linhas cairem enquanto elas existirem
    IEnumerator MakeItFall() {

        while (!GameManager.Instance.GameOver) {
            this.transform.position += Vector3.down * VelocityLinesFall;

            yield return new WaitForSeconds(0.02f);
        }


    }


}
