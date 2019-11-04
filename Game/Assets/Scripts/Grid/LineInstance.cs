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

    /// ------- Funções de fazer a linha cair ---------- 

    // Faz as linhas cairem enquanto elas existirem
    IEnumerator MakeItFall() {

        while (!GameManager.Instance.GameOver) {
            this.transform.position += Vector3.down * VelocityLinesFall;

            yield return new WaitForSeconds(0.02f);
        }


    }


}
