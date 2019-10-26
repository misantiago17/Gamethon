using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    private static BlockManager _instance;

    public static BlockManager Instance { get { return _instance; } }

    public GameObject Bolinha;

    public GameObject Block;
    public GameObject BlockParent;

    private int BlockValue = 1;

    // posicao no mundo
    private float PosX;
    private float PosY;

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

    // --------- Controle de Text e Value do bloco

    private TextMeshProUGUI bolinhaText;

    public GameObject createBlock(Vector3 pos, int value){

        PosX = pos.x;
        PosY = pos.y;

        BlockValue = value;

        GameObject block = Instantiate(Block, new Vector3(PosX, PosY, 0), Block.transform.rotation, BlockParent.transform);
        setBloquinhoText(block, value);

        return block;

    }

    private void setBloquinhoText(GameObject bloquinho, int value)
    {
        TextMeshProUGUI txtBlock = bloquinho.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        txtBlock.text = value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pega o texto da bolinha
        bolinhaText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        Debug.Log(bolinhaText);
        bolinhaText.text = BlockValue.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            int playerNum = collision.gameObject.GetComponent<BallData>().getNum();

            if (playerNum == BlockValue) {
                // spawn abaixo da posicao do bloco
                collision.gameObject.GetComponent<SpawnBall>().respawnBall(this.transform.position.x);
                // junta os blocos
            }

        }
    }
}
