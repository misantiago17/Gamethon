using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeBlocks : MonoBehaviour
{
    // Quando colide com um bloco verifica se o tiro tem o mesmo valor do bloco,
    // se tiver o mesmo valor do bloco o bloco faz um merge com o bloco adjacente que tiver o mesmo valor dele
    // se nao tiver bloco adjacente com mesmo valor só aumenta em *2 o valor

        // Toda vez que ele fizer um merge ele ganha ponto
        // Toda vez que eleda clera na fileira ele ganah pontos

    public GameObject PointEffect;
    public Text textPoints;
    public Text textLosePoints;

    public int PontoMergeTriplo = 100;
    public int PontoMerge = 100;
    public int PontoClearFileira = 100;

    private int currentPoints = 0;

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

    private void updateTextPoints()
    {
        textPoints.text = currentPoints.ToString();
        textLosePoints.text = currentPoints.ToString();
    }

    // Verifica se há merges para serem feitos, fazer isso enquanto ?
    public bool MergeCheck(GameObject bloquinho)
    {
        int lineInitial = RandomizeBlocks.Instance.initLine;
        Debug.Log("linha inicial: " + lineInitial);
        Debug.Log("linha final: " + (lineInitial + BlockGrid.Instance.numHorizontalBlocks - 2));

        GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

        for (int i = lineInitial; i < lineInitial + BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            for (int j = 0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
            {
                if(spawned[i, j] != null)
                {
                    if (spawned[i, j] == bloquinho)
                    {
                        // primeiro elemento da linha
                        if (j == 0)
                        {

                            if (spawned[i, j + 1] != null)
                            {
                                if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                                {
                                    Debug.Log("Primeiro");
                                    Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                                    currentPoints += PontoMerge;
                                    updateTextPoints();
                                    return true;
                                }
                            }

                        }
                        else if (j == BlockGrid.Instance.numHorizontalBlocks - 3) // ultimo
                        {
                            if (spawned[i, j - 1] != null)
                            {
                                //Debug.Log("i:" + i + "j:" + j);
                                if (spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue)
                                {
                                    Debug.Log("Ultimo");
                                    Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, false);
                                    currentPoints += PontoMerge;
                                    updateTextPoints();
                                    return true;
                                }
                            }
                            
                        }
                        else
                        {
                            //Debug.Log() // Verificar bug do 8

                            if (spawned[i, j - 1] != null && spawned[i, j + 1] != null &&
                                spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue
                            && spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                            {
                                //Debug.Log("i:" + i + "j:" + j);
                                Debug.Log("Caso de tres");
                                Merge(spawned[i, j], spawned[i, j - 1], spawned[i, j + 1], spawned, i, j, false);
                                currentPoints += PontoMergeTriplo;
                                updateTextPoints();
                                return true;
                            }
                            

                            // dir
                            else if (spawned[i, j - 1] != null && spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j - 1].GetComponent<BlockManager>().BlockValue)
                            {
                                //Debug.Log("i:" + i + "j:" + j);
                                Debug.Log("Meio esquerda");
                                Merge(spawned[i, j], spawned[i, j - 1], null, spawned, i, j, false);
                                currentPoints += PontoMerge;
                                updateTextPoints();
                                return true;
                            }
                            
                            // esq
                            else if (spawned[i, j + 1] != null && spawned[i, j].GetComponent<BlockManager>().BlockValue == spawned[i, j + 1].GetComponent<BlockManager>().BlockValue)
                            {
                                //Debug.Log("i:" + i + "j:" + j);
                                Debug.Log("Meio direita");
                                Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                                currentPoints += PontoMerge;
                                updateTextPoints();
                                return true;
                            }

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
            //Point Effector
            //Instantiate(PointEffect, spawned[i, j - 1].transform.position, Quaternion.identity);
            if(PointEffect != null)
                Instantiate(PointEffect, new Vector2(spawned[i, j /*+ 1*/].transform.position.x, spawned[i, j].transform.position.y), Quaternion.identity);

            spawned[i, j].GetComponent<BlockManager>().BlockValue = spawned[i, j+1].GetComponent<BlockManager>().BlockValue * 2;
            spawned[i, j].GetComponent<BlockManager>().updateBlockText();


            float auxPosXPre1 = spawned[i, j-1].transform.position.x;
            float auxPosXPos1 = auxPosXPre1;
            Destroy(spawned[i, j - 1].gameObject);
            spawned[i, j - 1] = null;

            float auxPosXPre2 = spawned[i, j+1].transform.position.x;
            float auxPosXPos2 = auxPosXPre2;
            Destroy(spawned[i, j + 1].gameObject);
            spawned[i, j + 1] = null;


            int k = j;
            while (k > 0)
            {
                k -= 1;
                if (spawned[i, k] != null)
                {
                    auxPosXPre1 = spawned[i, k].transform.position.x;
                    spawned[i, k].transform.position = new Vector3(auxPosXPos1, spawned[i, k].transform.position.y);
                    spawned[i, k + 1] = spawned[i, k];
                    auxPosXPos1 = auxPosXPre1;
                    spawned[i, k] = null;
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
                    auxPosXPos2 = auxPosXPre2;
                    spawned[i, k] = null;
                }
            }

        // Caso normal (ele vai para o bloco de maior peso)
        } else {

            //Point Effector
            //Instantiate(PointEffect, spawned[i, j - 1].transform.position, Quaternion.identity);
            if(PointEffect != null)
                Instantiate(PointEffect, new Vector2(spawned[i, j /*+ 1*/].transform.position.x, spawned[i, j].transform.position.y), Quaternion.identity);

            float auxPosXPre = spawned[i, j].transform.position.x;
            float auxPosXPos = auxPosXPre;

            Destroy(spawned[i, j].gameObject);
            spawned[i, j] = null;


            if (right)
            {

                spawned[i, j + 1].GetComponent<BlockManager>().BlockValue *= 2;
                spawned[i, j + 1].GetComponent<BlockManager>().updateBlockText();

                int k = j;
                while (k > 0)
                {
                    k -= 1;
                    if (spawned[i, k] != null)
                    {
                        auxPosXPre = spawned[i, k].transform.position.x;
                        spawned[i, k].transform.position = new Vector3(auxPosXPos, spawned[i, k].transform.position.y);
                        spawned[i, k + 1] = spawned[i, k];
                        auxPosXPos = auxPosXPre;    
                        spawned[i, k] = null;

                    }
                }

            } else
            {

                spawned[i, j - 1].GetComponent<BlockManager>().BlockValue *= 2;
                spawned[i, j - 1].GetComponent<BlockManager>().updateBlockText();

                int k = j;
                while (k < BlockGrid.Instance.numHorizontalBlocks - 2)
                {
                    k += 1;
                    if (spawned[i, k] != null)
                    {
                        auxPosXPre = spawned[i, k].transform.position.x;
                        spawned[i, k].transform.position = new Vector3(auxPosXPos, spawned[i, k].transform.position.y);
                        spawned[i, k - 1] = spawned[i, k];
                        auxPosXPos = auxPosXPre;
                        spawned[i, k] = null;
                    }
                }

            }
        }
    }


}
