using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    //public GameObject Player;

    //public Sprite[] Tile;
    //public RuntimeAnimatorController[] animController;

    //public AudioSource audioPunch;

    //1 2 4 8 16
    //[HideInInspector] public int BlockValue = 1;

    // --------- Controle de Text e Value do bloco

   // private TextMeshProUGUI bolinhaText;
    //private TextMeshProUGUI bloquinhoText;

    /*public void updateBlockText()
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

            StartCoroutine(showAndDie());

        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        // Pega o texto da bolinha
        //bolinhaText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //bolinhaText.text = BlockValue.ToString();

        //bloquinhoText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //audioPunch = GameObject.FindGameObjectWithTag("PunchSFX").GetComponent<AudioSource>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tirinho")
        {
            //int playerNum = collision.gameObject.GetComponent<BallData>().getNum();

            //Destroy(collision.gameObject);

            //audioPunch.PlayOneShot(audioPunch.clip, audioPunch.volume);

            if (/*playerNum == BlockValue*/ false) {
                // spawn abaixo da posicao do bloco
                //Player.gameObject.GetComponent<SpawnBall>().respawnBall(this.transform.position.x);
                //BlockValue *= 2;
                //updateBlockText();
                //gameObject.GetComponent<Animator>().SetTrigger("Death");

                //GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

                int index = 0;
                /*for (int i = RandomizeBlocks.Instance.initLine; i < RandomizeBlocks.Instance.initLine + BlockGrid.Instance.numHorizontalBlocks - 2; i++)
                {
                    for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                    {
                        if (spawned[i, j] == this.gameObject)
                            index = i;
                    }
                }*/

                Debug.Log("Index: " + index);

                Debug.Log("---------------------");

                // junta os blocos
                //MergeBlocks.Instance.MergeCheck(this.gameObject);

                bool acabouRepticoes = false;

                // Após isso verifica se a linha tem valor repetido entre si após o merge
                while (!acabouRepticoes)
                {
                    for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                    {
                        if (j != BlockGrid.Instance.numHorizontalBlocks - 3)
                        {
                           /* if (spawned[index, j] == spawned[index, j++])
                            {
                                Debug.Log("Reverifiquei: " + j);
                                MergeBlocks.Instance.MergeCheck(spawned[index, j]);
                            }*/
                        }
                        else
                        {
                            acabouRepticoes = true;
                        }
                    }
                }

                // Verificar se tem um unico na fileira e deletá-lo
                int count = 0;
                for(int j=0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                {
                    /*if (spawned[index, j] != null)
                        count++;*/
                }

                Debug.Log("Numero de itens na linha: " + count);

                if (count == 1)
                {
                    for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                    {
                        /*if (spawned[index, j] != null)
                        {
                            Debug.Log("Apaguei linha sozinha");
                            Destroy(spawned[index, j].gameObject);
                            spawned[index, j] = null;
                            RandomizeBlocks.Instance.initLine++;
                        }*/
                    }
                }




            }

        }
    }

    /*IEnumerator showAndDie()
    {
        bool wait = true;

        while (wait)
        {
            yield return new WaitForSeconds(1.0f);

            for (int i=RandomizeBlocks.Instance.initLine;i< RandomizeBlocks.Instance.initLine + BlockGrid.Instance.numHorizontalBlocks - 2; i++)
            {
                for (int j=0; j< BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                {
                    if (RandomizeBlocks.Instance.SpawnedBlocks[i,j] == this.gameObject)
                    {
                        if (RandomizeBlocks.Instance.SpawnedBlocks[i, j - 1] == null && RandomizeBlocks.Instance.SpawnedBlocks[i, j + 1] == null)
                        {
                            RandomizeBlocks.Instance.SpawnedBlocks[i, j] = null;
                            Destroy(this.gameObject);
                            wait = false;
                        }
     
                    }
                }
            }
        }
    }*/
}
