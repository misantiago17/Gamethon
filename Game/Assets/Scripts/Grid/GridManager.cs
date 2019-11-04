using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Nessa classe vai randomizar os blocos para criar a lista
    // fazer a rotação das linhas que estão sendo criadas e excluídas
    // Ela vai ficar no gameobject raíz Grid

    private static GridManager _instance;
    public static GridManager Instance { get { return _instance; } }

    public float SpawnBlockTimer = 7;

    public GameObject LinePrefab;

    private List<GameObject> lineList = new List<GameObject>();

    private int lastLineID = 0;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        StartCoroutine(SpawnBlockLine());
    }

    /// ------- Funções de controle da lista de lines ----------
     
    public void RemoveBlockFromLine(GameObject block) {

        for(int i=0; i<lineList.Count; i++) {

            if(lineList[i].GetComponent<LineInstance>().GetInstanceID() == block.GetComponent<BlocoInstance>().getBlockLineID()) {
                lineList[i].GetComponent<LineInstance>().RemoveBlockFromLine(block);

                // Se a linha está vazia apaga seu gameobject
                if (lineList[i].GetComponent<LineInstance>().getNumOfBlocks() == 0) {
                    Destroy(lineList[i]);
                    lineList.RemoveAt(i);
                }
            }

        }
    }

    /// ------- Funções de criação da grid ----------

    private void createLine() {

        GameObject line = Instantiate(LinePrefab, this.transform.position, LinePrefab.transform.rotation, this.transform);
        int blocksCount = line.GetComponent<LineInstance>().getNumOfBlocks();
        line.GetComponent<LineInstance>().SetLineData(lastLineID, blocksCount);

        lastLineID++;
        lineList.Add(line);
    }

    IEnumerator SpawnBlockLine() {

        while (!GameManager.Instance.GameOver) {

            createLine();
            yield return new WaitForSeconds(SpawnBlockTimer);
        }
    }

}
