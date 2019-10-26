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

    // recebe a instancia do bloco

    public void MergeCheck(GameObject bloquinho)
    {
        GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;
        
        for(int i=0; i < BlockGrid.Instance.numHorizontalBlocks - 2; i++)
        {
            for (int j=0; j < BlockGrid.Instance.numHorizontalBlocks - 2; j++)
            {
                if (spawned[i,j] == bloquinho)
                {
                    Debug.Log("junta os bloquinhos");

                    if (j == 0)
                    {
                        Debug.Log("junta os bloquinhos A");

                        if (spawned[i, j] == spawned[i, j + 1]) {
                            Debug.Log("junta os bloquinhos 1");
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                        }
                    }
                    else if (j == BlockGrid.Instance.numHorizontalBlocks - 3)
                    {
                        Debug.Log("junta os bloquinhos B");
                        if (spawned[i, j] == spawned[i, j - 1])
                        {
                            Debug.Log("junta os bloquinhos 2");
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, false);
                        }
                    }
                    else
                    {
                        Debug.Log("junta os bloquinhos C");
                        if (spawned[i, j] == spawned[i, j - 1] && spawned[i, j] == spawned[i, j + 1])
                        {
                            Debug.Log("junta os bloquinhos 3");
                            Merge(spawned[i, j], spawned[i, j - 1], spawned[i, j + 1], spawned, i, j, false);
                        }

                        if (spawned[i, j] == spawned[i, j - 1])
                        {
                            Debug.Log("junta os bloquinhos 4");
                            Merge(spawned[i, j], spawned[i, j - 1], null, spawned, i, j, false);
                        } else  if (spawned[i, j] == spawned[i, j + 1])
                        {
                            Debug.Log("junta os bloquinhos 5");
                            Merge(spawned[i, j], spawned[i, j + 1], null, spawned, i, j, true);
                        }

                    }
                }
            }
        }


    }

    // ideia que seja recursivo (vá juntando os iguais)

    // Se for o caso de duplas, apaga o menor e vai para o de maior valor, se for o caso de pontas 
    void Merge(GameObject obj1, GameObject obj2, GameObject obj3, GameObject[,] spawned, int i, int j, bool right)
    {
        // caso onde da merge nos três - aumenta o valor do obj1, some os dois do lado e junta os dos lados
        if (obj3 != null)
        {
            obj3.GetComponent<BlockManager>().BlockValue *= 2;
            obj3.GetComponent<BlockManager>().updateBlockText();

            spawned[i, j - 1] = null;
            spawned[i, j + 1] = null;

            int k = j - 1;
            while(k > 0){
                k -= 1;
                if (spawned[i, k] != null) {
                    spawned[i, k + 1] = spawned[i, k];
                    spawned[i, k] = null;
                }
            }

            k = j;
            while (k < BlockGrid.Instance.numHorizontalBlocks - 2)
            {
                k += 1;
                if (spawned[i, k] != null)
                {
                    spawned[i, k - 1] = spawned[i, k];
                    spawned[i, k] = null;
                }
            }

        // Caso normal (ele vai para o bloco de maior peso)
        } else {

            spawned[i, j] = null;
            obj2.GetComponent<BlockManager>().BlockValue *= 2;
            obj2.GetComponent<BlockManager>().updateBlockText();

            if (right)
            {
                spawned[i, j + 1] = null;

                int k = j - 1;
                while (k > 0) {
                    k -= 1;
                    if (spawned[i, k] != null)
                    {
                        spawned[i, k + 1] = spawned[i, k];
                        spawned[i, k] = null;
                    }
                }

            } else
            {
                spawned[i, j - 1] = null;

                int k = j;
                while (k < BlockGrid.Instance.numHorizontalBlocks - 2)
                {
                    k += 1;
                    if (spawned[i, k] != null)
                    {
                        spawned[i, k - 1] = spawned[i, k];
                        spawned[i, k] = null;
                    }
                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
