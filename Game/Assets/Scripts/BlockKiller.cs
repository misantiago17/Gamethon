using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockKiller : MonoBehaviour
{
    public GameObject LosePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

            for(int i=0; i<BlockGrid.Instance.numHorizontalBlocks - 2; i++)
            {
                for(int j=0; j< BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                {
                    if(spawned[i,j] == collision.gameObject)
                    {
                        spawned[i, j] = null;
                        Destroy(collision.gameObject);

                        RandomizeBlocks.Instance.initLine++;

                        LosePanel.SetActive(true);
                        // Condição de derrota
                    }
                }
            }
        }
    }
}
