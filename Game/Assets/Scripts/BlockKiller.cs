using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            Debug.Log("ENTREI 1");
            GameObject[,] spawned = RandomizeBlocks.Instance.SpawnedBlocks;

            for(int i=0; i<BlockGrid.Instance.numHorizontalBlocks - 2; i++)
            {
                for(int j=0; j< BlockGrid.Instance.numHorizontalBlocks - 2; j++)
                {
                    if(spawned[i,j] == collision.gameObject)
                    {
                        Debug.Log("ENTREI 2");
                        spawned[i, j] = null;
                        Destroy(collision.gameObject);

                        // Condição de derrota
                    }
                }
            }
        }
    }
}
