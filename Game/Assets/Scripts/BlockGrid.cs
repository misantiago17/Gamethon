using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    private static BlockGrid _instance;

    public static BlockGrid Instance { get { return _instance; } }

    // Pega o topo esquerdo da tela, mede 7 blocks com o comprimento da tela
    // deixa um espaco de 1 block na parte de baixo da tela
    // monta o restante com isso

    public int numHorizontalBlocks = 7;

    // Incluindo o bloco incial onde a bolinha se encontra
    private int numVerticalBlocks;

    private float height;
    private float width;
    private float blockSize;

    [HideInInspector] public Vector3[,] gridWorld = new Vector3[12,12];

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
        height = Screen.height;
        width = Screen.width;
        blockSize = width / numHorizontalBlocks;
        numVerticalBlocks = (int)(height / blockSize);

        // -2 para caber na tela (provavelmente conta errada)
        for (int i = numHorizontalBlocks - 2 ; i >= 0; i--) {
            for(int j=2; j< numVerticalBlocks - 1; j++) {

                gridWorld[i,j - 2] = getPosInMap(i, j);

                // Esse create block precisa ser randomico, assim como o numero dele e fazer os blocos descerem
                //this.GetComponent<BlockManager>().createBlock(getPosInMap(i, j), 1);

            }
        }

    }

    // cada bloco tem que ter seu lugar no mapa
    public Vector3 getPosInMap(int x, int y)
    {
        Vector3 blockPos = Camera.main.ScreenToWorldPoint(new Vector3(blockSize*x + blockSize,blockSize*y + blockSize,0));

        return blockPos;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
