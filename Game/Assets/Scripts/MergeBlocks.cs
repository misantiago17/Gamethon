using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBlocks : MonoBehaviour
{
    // Quando colide com um bloco verifica se o tiro tem o mesmo valor do bloco,
    // se tiver o mesmo valor do bloco o bloco faz um merge com o bloco adjacente que tiver o mesmo valor dele
    // se nao tiver bloco adjacente com mesmo valor só aumenta em *2 o valor


    private static MergeBlocks _instance;

    public static MergeBlocks Instance { get { return _instance; } }

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


    // Verifica se há merges para serem feitos, fazer isso enquanto ?
    public bool MergeCheck(GameObject bloquinho)
    {
        GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

        for (int i = 0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
            {
                if (spawned[i, j] == bloquinho)
                {

                    if (j == 0)
                    {
                        if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                        {
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                            return true;
                        }
                    }
                    else if (j == BlockGrid.Instance.numHorizontalBlocks - 3)
                    {
                        if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue)
                        {
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, false);
                            return true;
                        }
                    }
                    else
                    {
                        if(spawned[i, j - 1] != null && spawned[i, j + 1] != null)
                        {
                            if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue
                            && spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                            {
                                Merge(spawned[i, j], spawned[i, j - 1], spawned[i, j + 1], spawned, i, j, false);
                                return true;
                            }
                        }

                        else if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue)
                        {
                            Merge(spawned[i, j], spawned[i, j - 1], null, spawned, i, j, false);
                            return true;
                        }
                        else if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                        {
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                            return true;
                        }

                    }
                }
            }
        }

        return false;

    }


    // Se for o caso de duplas, apaga o menor e vai para o de maior valor, se for o caso de pontas 
    void Merge(GameObject obj1, GameObject obj2, GameObject obj3, GameObject[,] spawned, int i, int j, bool right)
    {

        // caso onde da merge nos três - aumenta o valor do obj1, some os dois do lado e junta os dos lados
        if (obj3 != null)
        {
            //Debug.Log("CASO DE 3 ITENS");

            spawned[i, j].GetComponent<BlockManager>().BlockValue = spawned[i, j+1].GetComponent<BlockManager>().BlockValue * 2;
            spawned[i, j].GetComponent<BlockManager>().updateBlockText();

            //Debug.Log("VALOR CENTRO " + obj2.GetComponent<BlockManager>().BlockValue);

            float auxPosXPre1 = spawned[i, j-1].transform.position.x;
            float auxPosXPos1 = auxPosXPre1;
            Destroy(spawned[i, j - 1].gameObject);
            spawned[i, j - 1] = null;

            float auxPosXPre2 = spawned[i, j+1].transform.position.x;
            float auxPosXPos2 = auxPosXPre2;
            Destroy(spawned[i, j + 1].gameObject);
            spawned[i, j + 1] = null;

            // ate aqui ta tudo certo

            int k = j;
            while (k > 0)
            {
                k -= 1;
                if (spawned[i, k] != null)
                {
                    auxPosXPre1 = spawned[i, k].transform.position.x;
                    spawned[i, k].transform.position = new Vector3(auxPosXPos1, spawned[i, k].transform.position.y);
                    spawned[i, k + 1] = spawned[i, k];

                    // isso aqui ta errado?
                    //auxPosXPre1 = spawned[i, k].transform.position.x;
                    auxPosXPos1 = auxPosXPre1;
                    spawned[i, k] = null;

                    //Debug.Log("VALOR ESQ " + spawned[i, k + 1].GetComponent<BlockManager>().BlockValue);

                }
            }

            k = j;
            while (k < BlockGrid.Instance.numHorizontalBlocks - 2)
            {
                k += 1;
                if (spawned[i, k] != null)
                {
                    auxPosXPre2 = spawned[i, k].transform.position.x;
                    spawned[i, k].transform.position = new Vector3(auxPosXPos2, spawned[i, k].transform.position.y);
                    spawned[i, k - 1] = spawned[i, k];
                    //auxPosXPre2 = spawned[i, k].transform.position.x;
                    auxPosXPos2 = auxPosXPre2;
                    spawned[i, k] = null;


                    //Debug.Log("VALOR DIR " + spawned[i, k - 1].GetComponent<BlockManager>().BlockValue);

                    //spawned[i, k - 1].transform.position = BlockGrid.Instance.getPosInMap(i, k + 1);
                }
            }

        // Caso normal (ele vai para o bloco de maior peso)
        } else {

            //Debug.Log("CASO JUNTA MAIOR");

            float auxPosXPre = spawned[i, j].transform.position.x;
            float auxPosXPos = auxPosXPre;

            Destroy(spawned[i, j].gameObject);
            spawned[i, j] = null;


            if (right)
            {
                //Debug.Log("SE MAIOR A DIREITA");

                spawned[i, j + 1].GetComponent<BlockManager>().BlockValue *= 2;
                spawned[i, j + 1].GetComponent<BlockManager>().updateBlockText();

                //Debug.Log("VALOR MAIOR " + spawned[i, j + 1].GetComponent<BlockManager>().BlockValue);

                // O maior valor fica parado, exclui o valor menor

                //auxPosXPre = spawned[i, j + 1].transform.position.x;
                //spawned[i, j + 1].transform.position = new Vector3(auxPosXPos, spawned[i, j + 1].transform.position.y);
                //auxPosXPos = auxPosXPre;
                //spawned[i, j] = spawned[i, j + 1];
                //spawned[i, j + 1] = null;

                int k = j;
                while (k > 0)
                {
                    k -= 1;
                    if (spawned[i, k] != null)
                    {
                        auxPosXPre = spawned[i, k].transform.position.x;
                        spawned[i, k].transform.position = new Vector3(auxPosXPos, spawned[i, k].transform.position.y);
                        spawned[i, k + 1] = spawned[i, k];

                        // isso aqui ta errado?
                        //auxPosXPre = spawned[i, k].transform.position.x;
                        auxPosXPos = auxPosXPre;    
                        spawned[i, k] = null;

                        //Debug.Log("VALOR ESQ " + spawned[i, k + 1].GetComponent<BlockManager>().BlockValue);

                    }
                }

            } else
            {
                //Debug.Log("SE MAIOR A ESQUERDA");

                spawned[i, j - 1].GetComponent<BlockManager>().BlockValue *= 2;
                spawned[i, j - 1].GetComponent<BlockManager>().updateBlockText();

                //Debug.Log("VALOR MAIOR " + spawned[i, j - 1].GetComponent<BlockManager>().BlockValue);

                // O maior valor fica parado, exclui o valor menor

                //auxPosXPre = spawned[i, j - 1].transform.position.x;
                //spawned[i, j - 1].transform.position = new Vector3(auxPosXPos, spawned[i, j - 1].transform.position.y);
                //auxPosXPos = auxPosXPre;
                //spawned[i, j] = spawned[i, j - 1];
                //spawned[i, j-1] = null;

                // Puxa os demais a esquerda para o lado do maior

                // ate aqui certo

                int k = j;
                while (k < BlockGrid.Instance.numHorizontalBlocks - 2)
                {
                    k += 1;
                    if (spawned[i, k] != null)
                    {
                        auxPosXPre = spawned[i, k].transform.position.x;
                        spawned[i, k].transform.position = new Vector3(auxPosXPos, spawned[i, k].transform.position.y);
                        spawned[i, k - 1] = spawned[i, k];
                        //auxPosXPre = spawned[i, k].transform.position.x;
                        auxPosXPos = auxPosXPre;
                        spawned[i, k] = null;


                        //Debug.Log("VALOR DIR " + spawned[i, k - 1].GetComponent<BlockManager>().BlockValue);

                        //spawned[i, k - 1].transform.position = BlockGrid.Instance.getPosInMap(i, k + 1);
                    }
                }

            }
        }
    }


}
