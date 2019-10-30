using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBlocks : MonoBehaviour
{
    // Randomiza os blocos que aparecem na primeira linha da grid
    // faz os blocos cairem

    public float SpawnBlockTimer = 5;
    public float FallingBlockVelocity = 0.5f;

    public Hold ballValue;

    private float timer = 0;

    [HideInInspector] public GameObject[,] SpawnedBlocks = new GameObject[500,6];

    private int numLines = 0;

    [HideInInspector] public int initLine = 0;

    private static RandomizeBlocks _instance;

    public static RandomizeBlocks Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnBlockLine();
        StartCoroutine(makeLinesFall());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SpawnBlockTimer)
        {
            spawnBlockLine();
            timer = 0;
        }

    }

    // Coloca os itens na linha
    // Pensar em formas de fazer isso sem deixar um unico sozinho
    void spawnBlockLine()
    {
        // indica a presenca do bloco nos quadrantes
        int[] blockPres = new int[BlockGrid.Instance.numHorizontalBlocks - 1];
        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
            blockPres[i] = Random.Range(0, 2);

        // verifica se nao tem nenhum sozinho
        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            if (i == 0) {
                if (blockPres[i] == 1 && blockPres[i + 1] == 0)
                    blockPres[i + 1] = 1;
            } else if (i == BlockGrid.Instance.numHorizontalBlocks - 3) {
                if (blockPres[i] == 1 && blockPres[i - 1] == 0)
                    blockPres[i - 1] = 1;
            } else {
                if (blockPres[i] == 1 && blockPres[i - 1] == 0 && blockPres[i + 1] == 0) {

                    int choose = Random.Range(0, 2);

                    if (choose == 0)
                        blockPres[i - 1] = 1;
                    else
                        blockPres[i + 1] = 1;
                }
            }
        }

        int[] myValues = new int[4];

        myValues[0] = 1;
        myValues[1] = 2;
        myValues[2] = 4;
        myValues[3] = 8;

        int index = Random.Range(0, myValues.Length);
        int myRandomNumber = myValues[index];

        // Randomiza valores para os bloquinhos
        for(int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            index = Random.Range(0, myValues.Length);
            myRandomNumber = myValues[index];

            blockPres[i] = myRandomNumber;
        }

        // Não permite que tenha dois numero de valores iguais um do lado do outro
        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            if (i == 0)
            {
                if (blockPres[i] == blockPres[i + 1]) {

                    while(blockPres[i] == blockPres[i + 1]) {
                        index = Random.Range(0, myValues.Length);
                        myRandomNumber = myValues[index];

                        blockPres[i + 1] = myRandomNumber;
                    }

                }
            }
            else if (i == BlockGrid.Instance.numHorizontalBlocks - 3)
            {
                if (blockPres[i] == blockPres[i - 1]) {
                    while (blockPres[i] == blockPres[i - 1]) {
                        index = Random.Range(0, myValues.Length);
                        myRandomNumber = myValues[index];

                        blockPres[i - 1] = myRandomNumber;
                    }
                }
            }
            else
            {
                if (blockPres[i] == blockPres[i - 1])
                {
                    while(blockPres[i] == blockPres[i - 1]) {
                        index = Random.Range(0, myValues.Length);
                        myRandomNumber = myValues[index];

                        blockPres[i] = myRandomNumber;
                    }

                }

                if (blockPres[i] == blockPres[i + 1]) {

                    while (blockPres[i] == blockPres[i + 1])
                    {
                        index = Random.Range(0, myValues.Length);
                        myRandomNumber = myValues[index];

                        blockPres[i + 1] = myRandomNumber;
                    }
                }
            }

        }


        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++) {

            if (blockPres[i] != 0)
            {
                SpawnedBlocks[numLines, i] = BlockGrid.Instance.createBlock(BlockGrid.Instance.gridWorld[i, BlockGrid.Instance.numHorizontalBlocks], blockPres[i]);
            }
        }

        if (numLines == 30)
            numLines = 0;
        else
            numLines++;
    }


    // Deixar isso smooth
    IEnumerator makeLinesFall()
    {
        while (true)
        {
            for(int i = 0; i < 30; i++)
            {
                for (int j=0; j<6; j++)
                {
                    if (SpawnedBlocks[i,j] != null)
                    {
                        SpawnedBlocks[i, j].transform.position += Vector3.down * FallingBlockVelocity;
                    }
                }
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    
}
