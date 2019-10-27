using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    public GameObject Player;

    public Sprite[] Tile;
    public RuntimeAnimatorController[] animController;

    //1 2 4 8 16
    [HideInInspector] public int BlockValue = 1;

    // --------- Controle de Text e Value do bloco

    private TextMeshProUGUI bolinhaText;
    private TextMeshProUGUI bloquinhoText;

    public void updateBlockText()
    {
        if (BlockValue == 1) {
            gameObject.GetComponent<SpriteRenderer>().sprite = Tile[0];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animController[0];

        }
        if (BlockValue == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Tile[1];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animController[1];

        }
        if (BlockValue == 4)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Tile[2];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animController[2];

        }
        if (BlockValue == 8)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Tile[3];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animController[3];

        }
        if (BlockValue == 16)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Tile[4];
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animController[4];

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pega o texto da bolinha
        //bolinhaText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //bolinhaText.text = BlockValue.ToString();

        bloquinhoText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tirinho")
        {
            int playerNum = collision.gameObject.GetComponent<BallData>().getNum();

            Destroy(collision.gameObject);

            if (playerNum == BlockValue) {
                // spawn abaixo da posicao do bloco
                Player.gameObject.GetComponent<SpawnBall>().respawnBall(this.transform.position.x);
                BlockValue *= 2;
                updateBlockText();
                //gameObject.GetComponent<Animator>().SetTrigger("Death");

                GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

                int index = 0;
                for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
                {
                    for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                    {
                        if (spawned[i, j] == this.gameObject)
                            index = j;
                    }
                }

                // junta os blocos
                MergeBlocks.Instance.MergeCheck(this.gameObject);

                bool acabouRepticoes = false;

                // Após isso verifica se a linha tem valor repetido entre si após o merge
                while (!acabouRepticoes)
                {
                    for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                    {
                        if (j != BlockGrid.Instance.numHorizontalBlocks - 3)
                        {
                            if (spawned[index, j] == spawned[index, j++])
                            {
                                MergeBlocks.Instance.MergeCheck(spawned[index, j]);
                                Debug.Log("ENTREI AA");
                            }
                        }
                        else
                        {
                            acabouRepticoes = true;
                        }
                    }
                }

                // 


            }

        }
    }
}
