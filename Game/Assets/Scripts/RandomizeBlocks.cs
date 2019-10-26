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

    private GameObject[,] SpawnedBlocks = new GameObject[12,6];

    private int numLines = 0;

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
        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            int rand = Random.Range(0, 2);

            // repensar nesse sorteio de valor - dar um jeito de pegar o valor max e min da bolinha
            int randomValue = Random.Range(1, 2);

            if (rand == 1)
            {
                SpawnedBlocks[numLines, i] = BlockManager.Instance.createBlock(BlockGrid.Instance.gridWorld[i, BlockGrid.Instance.numHorizontalBlocks], randomValue);
            }
        }

        if (numLines == 12)
            numLines = 0;
        else
            numLines++;
    }


    // Deixar isso smooth
    IEnumerator makeLinesFall()
    {
        while (true)
        {
            for(int i = 0; i < 12; i++)
            {
                for (int j=0; j<6; j++)
                {
                    if (SpawnedBlocks[i,j] != null)
                    {
                        SpawnedBlocks[i, j].transform.position += Vector3.down * FallingBlockVelocity;
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
    
}
