using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    public GameObject Player;

    [HideInInspector] public int BlockValue = 1;

    // --------- Controle de Text e Value do bloco

    private TextMeshProUGUI bolinhaText;
    private TextMeshProUGUI bloquinhoText;

    public void updateBlockText()
    {
        bloquinhoText.text = BlockValue.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pega o texto da bolinha
        bolinhaText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        bolinhaText.text = BlockValue.ToString();
        bloquinhoText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tirinho")
        {
            int playerNum = collision.gameObject.GetComponent<BallData>().getNum();

            if (playerNum == BlockValue) {
                // spawn abaixo da posicao do bloco
                Player.gameObject.GetComponent<SpawnBall>().respawnBall(this.transform.position.x);
                BlockValue *= 2;
                updateBlockText();
                gameObject.GetComponent<Animator>().SetTrigger("Death");
                // junta os blocos
                MergeBlocks.Instance.MergeCheck(this.gameObject);
            }

        }
    }
}
