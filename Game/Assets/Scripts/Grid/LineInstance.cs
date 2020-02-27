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

    public Animation MergeAnimation;

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

    public int getLineID() {
        return lineData.getID();
    }

    /// ------- Funções de controle da lista de blocos ----------

    public void RemoveBlockFromLine(GameObject block) {

        int index = block.GetComponent<BlocoInstance>().getBlockLineIndex();

        /*blocksPattern[index] = 0;
        int num = lineData.getNumOfBlocks() - 1;
        lineData.setNumOfBlocks(num);*/

        if (blockList[index] == block) {
            //blockList.InsertRange(index, null);
            //blockList.RemoveAt(index);
            blocksPattern[index] = 0;
            int num = lineData.getNumOfBlocks() - 1;
            lineData.setNumOfBlocks(num);
            // Retira do jogo
            Destroy(block);
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

            // bug fix
            } else {
                blockList.Add(null);
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
    public bool MergeCheck(int blockIndex) {

        BlocoInstance blockInfo = blockList[blockIndex].GetComponent<BlocoInstance>();

        // Caso 4: o bloco está em seu ultimo valor e tem blocos ao lado
        if (blocksPattern[blockIndex] == blockList[blockIndex].GetComponent<BlocoInstance>().MaxBlockValue) {

            // Verifica se há blocos à direita e à esquerda --- puxa para direita porque sim
            if ((blockIndex != MaxNumOfBlocksPerLine - 1 && blockIndex != 0) && (blocksPattern[blockIndex - 1] != 0 && blocksPattern[blockIndex + 1] != 0)) {

                StartCoroutine(WaitTillBlockExplode(blockList[blockIndex].GetComponent<BlocoInstance>(), blockIndex, false));
                return true;
            }

         // Caso 1: o bloco está no centro e está entre dois blocos
        } else if ((blockIndex != 0 && blockIndex != MaxNumOfBlocksPerLine - 1) && (blocksPattern[blockIndex-1] != 0 && blocksPattern[blockIndex + 1] != 0)) {

            // - Verifica se há blocos à esquerda e direita são iguais a ele -> caso sim, aumenta seu valor e some com os dois do lado, puxa TODOS para o centro
            if ((blocksPattern[blockIndex - 1] == blocksPattern[blockIndex]) && (blocksPattern[blockIndex + 1] == blocksPattern[blockIndex])) {

                int newValue = blockList[blockIndex].GetComponent<BlocoInstance>().getBlockValue() * 2;
                blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(newValue);
                StartCoroutine(WaitForUpgradeMerge(blockIndex, true, true));
                return true;

                // - Verifica se há blocos à esquerda iguais a ele -> caso sim, aumenta seu valor e some com o da esquerda e puxa todos os blocos da ESQUERDA
            } else if (blocksPattern[blockIndex - 1] == blocksPattern[blockIndex]) {

                int newValue = blockList[blockIndex].GetComponent<BlocoInstance>().getBlockValue() * 2;
                blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(newValue);
                StartCoroutine(WaitForUpgradeMerge(blockIndex, true, false));
                return true;

                // - Verifica se há blocos à direita iguais a ele -> caso sim, aumenta seu valor e some com o da direita e puxa todos os blocos da DIREITA
            } else if (blocksPattern[blockIndex + 1] == blocksPattern[blockIndex]) {

                int newValue = blockList[blockIndex].GetComponent<BlocoInstance>().getBlockValue() * 2;
                blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(newValue);
                StartCoroutine(WaitForUpgradeMerge(blockIndex, false, true));
                return true;
            }

        // Caso 2: o bloco atingido foi o da extrema esquerda ou não é o ultimo bloco e tem blocos à direita 
        } else if (blockIndex == 0 || (blockIndex != MaxNumOfBlocksPerLine-1 && blocksPattern[blockIndex + 1] != 0)) {

            // - Verifica se há blocos à direita iguais a ele-> caso sim, aumenta seu valor e some com a da direita e puxa todos todos os blocos da DIREITA
            if (blocksPattern[blockIndex + 1] == blocksPattern[blockIndex]) {

                int newValue = blockList[blockIndex].GetComponent<BlocoInstance>().getBlockValue() * 2;
                blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(newValue);
                StartCoroutine(WaitForUpgradeMerge(blockIndex, false, true));
                return true;
            }

        // Caso 3: o bloco atingido foi o da extrema direita ou não é o primeiro bloco e tem blocos à esquerda 
        } else if (blockIndex == MaxNumOfBlocksPerLine - 1 || (blockIndex != 0 && blocksPattern[blockIndex - 1] != 0)) {

            // - Verifica se há blocos à esquerda iguais a ele -> caso sim, aumenta seu valor e some com a da esquerda e puxa todos os blocas da ESQUERDA
            if (blocksPattern[blockIndex - 1] == blocksPattern[blockIndex]) {

                int newValue = blockList[blockIndex].GetComponent<BlocoInstance>().getBlockValue() * 2;
                blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(newValue);
                StartCoroutine(WaitForUpgradeMerge(blockIndex, true, false));
                return true;
            }

        // Caso 5: o bloco está sem nenhum bloco ao lado
        // meio se tem em a direita ou a esquerda
        // extrema esquerda se tem ao lado direito
        // extrema direita se tem ao lado esquerdo
        } else if (((blockIndex != 0 && blockIndex != MaxNumOfBlocksPerLine -1) && (blocksPattern[blockIndex - 1] == 0 && blocksPattern[blockIndex + 1] == 0))
                    || (blockIndex == 0 && blocksPattern[blockIndex + 1] == 0) || (blockIndex == MaxNumOfBlocksPerLine - 1 && blocksPattern[blockIndex - 1] == 0)) {

            Debug.Log("É um bloco sozinho");

            blockList[blockIndex].GetComponent<BlocoInstance>().updateBlockValue(16);
            StartCoroutine(WaitTillBlockExplode(blockList[blockIndex].GetComponent<BlocoInstance>(), blockIndex, true));
        }

        // Caso 6: o bloco está upgradou e não tem merge - nada acontece feijoada
        return false;
    }

    // troca o valor no blockPattern
    // upgrade o valor do bloco para o do bloco copia
    private void replaceBlock(int blockCopy, int blockToBeReplaced) {

        blocksPattern[blockToBeReplaced] = blocksPattern[blockCopy];
        int copyValue = blockList[blockCopy].GetComponent<BlocoInstance>().getBlockValue();
        blockList[blockToBeReplaced].GetComponent<BlocoInstance>().updateBlockValue(copyValue);

        blocksPattern[blockCopy] = 0;
        blockList[blockCopy].GetComponent<BlocoInstance>().updateBlockValue(0);
    }


    /// ------- Funções de fazer a linha cair e merge com corotina ---------- 
    /// 

    IEnumerator WaitTillBlockExplode(BlocoInstance bloco, int index, bool alone) {

        bloco.canDestroy = false;

        while (!bloco.canDestroy) {
            yield return new WaitForEndOfFrame();
        }

        blocksPattern[index] = 0;

        if (!alone)
            StartCoroutine(Merge(index - 1, false, true));
      
    }

    IEnumerator WaitForUpgradeMerge(int index, bool pullLeft, bool pullRight) {

        if (pullLeft) {
            blocksPattern[index - 1] = 0;
            blockList[index - 1].GetComponent<BlocoInstance>().updateBlockValue(0);
        }

        if (pullRight) {
            blocksPattern[index + 1] = 0;
            blockList[index + 1].GetComponent<BlocoInstance>().updateBlockValue(0);
        }

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(Merge(index, pullLeft, pullRight));
    }

    // Recebe o index do item que foi incrementado
    IEnumerator Merge(int index, bool pullLeft, bool pullRight) {

        GameManager.Instance.UpdateHUD(100);

        int rightIndex = 0;
        int leftIndex = 0;

        // Se vai puxar o item a esquerda, passa o index para ele
        if (pullLeft) {
            leftIndex = index - 1;
        }

        // se vai puxar o item a direita, passa o index para ele 
        if (pullRight) {
            rightIndex = index + 1;
        }

        while (pullRight || pullLeft) {

            // Verifica se há bloco ao lado -> caso sim, copia e o substitui, depois move o index para o lado e questiona novamente;
            // -> caso não, dele ta a si mesmo;

            // Faz o merge da direita
            if (pullRight) {

                if ((rightIndex != MaxNumOfBlocksPerLine -1) && (blocksPattern[rightIndex + 1] != 0)) {

                    replaceBlock(rightIndex + 1, rightIndex);
                    rightIndex = rightIndex + 1;

                } else {
                    RemoveBlockFromLine(blockList[rightIndex]);
                    pullRight = false;
                }
            } 

            // Faz o merge da esquerda
            if (pullLeft) {

                if ((leftIndex != 0) && (blocksPattern[leftIndex - 1] != 0)) {

                    replaceBlock(leftIndex - 1, leftIndex);
                    leftIndex = leftIndex - 1;

                } else {
                    RemoveBlockFromLine(blockList[leftIndex]);
                    pullLeft = false;
                }
            } 

            // cada bloco teria que ter se merge animation? 
            // melhor chamar de move animation (porque eles estão só se movendo para o lado?)
            // teria uma animacao para cada lado?
            // teria um merge animation dos dois primeiros blocos?
            if (MergeAnimation) {
                yield return WaitForAnimation(MergeAnimation);
            } else {
                yield return new WaitForSeconds(0.2f);
            }
        }

        // Faz a recursão
        MergeCheck(index);
    }

    private IEnumerator WaitForAnimation(Animation animation) {
        do {
            yield return null;
        } while (animation.isPlaying);
    }

    // Faz as linhas cairem enquanto elas existirem
    IEnumerator MakeItFall() {

        while (!GameManager.Instance.GameOver) {
            this.transform.position += Vector3.down * VelocityLinesFall;

            yield return new WaitForSeconds(0.02f);
        }


    }


}
