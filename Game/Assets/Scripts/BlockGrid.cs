using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    // Pega o topo esquerdo da tela, mede 7 blocks com o comprimento da tela
    // deixa um espaco de 1 block na parte de baixo da tela
    // monta o restante com isso

    public int numHorizontalBlocks = 7;

    // Incluindo o bloco incial onde a bolinha se encontra
    private int numVerticalBlocks;

    private float height;
    private float width;
    private float blockSize;


    // Start is called before the first frame update
    void Start()
    {
        height = Screen.height;
        width = Screen.width;
        blockSize = width / numHorizontalBlocks;
        numVerticalBlocks = (int)(height / blockSize);

        Debug.Log("Num vert blocks " + numVerticalBlocks);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
