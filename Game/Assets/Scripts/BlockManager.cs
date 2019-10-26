using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    public int BlockValue = 1;

    private TextMeshProUGUI bolinhaText;

    // Start is called before the first frame update
    void Start()
    {
        bolinhaText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        bolinhaText.text = BlockValue.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            int playerNum = collision.gameObject.GetComponent<BallData>().getNum();
        }
    }
}
