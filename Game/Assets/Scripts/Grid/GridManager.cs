using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Nessa classe vai randomizar os blocos para criar a lista
    // fazer a rotação das linhas que estão sendo criadas e excluídas
    // Ela vai ficar no gameobject raíz Grid

    public float SpawnBlockTimer = 7;

    public GameObject LinePrefab;

    //private List<List<GameObject>> bloquinhosList = new List<List<GameObject>>();
    private List<GameObject> lineList = new List<GameObject>();

    private int lastLineID = 0;

    private LineInstance lineData;
    private Vector3 lineInitialPos;

    private void Start() {
        lineInitialPos = LinePrefab.GetComponent<LineInstance>().SpawnLinePosition();
        StartCoroutine(SpawnBlockLine());
    }

    /// ------- Funções de criação da grid ----------
     
    private void createLine() {

        GameObject line = Instantiate(LinePrefab, lineInitialPos, LinePrefab.transform.rotation, this.transform);
        int blocksCount = line.GetComponent<LineInstance>().getNumOfBlocks();
        line.GetComponent<LineInstance>().SetLineData(lastLineID, blocksCount);
        lineList.Add(line);
    }

    IEnumerator SpawnBlockLine() {

        while (!GameManager.Instance.GameOver) {

            createLine();
            yield return new WaitForSeconds(SpawnBlockTimer);
        }
    }

}
