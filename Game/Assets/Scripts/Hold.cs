using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hold : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bolinha;
    public float TimeToGrowForce = 5.0f;

    private bool holding = false;
    private float timer = 0;

    // Tempo em que a pessoa fica segurando
    private float holdTime = 0;

    // O numero inicial é 4, quando ele quebra um bloco que vale mais que 4, ele passa a ser o novo máximo
    private int MaxChargeNum = 8;
    // A partir de certo numero quebrado ele atualiza o novo minimo do jogo
    private int MinChargeNum = 1;

    // Spawn da bola onde ele acertar um bloco valido, se ele nao acertar nada o spawn é onde a bola acertar no chao
    // fazer uma mira que vc escolhe para onde a bola vai

    private BallData data;

    private TextMeshProUGUI bolinhaText;

    private void Start()
    {
        data = Player.GetComponent<BallData>();

        bolinhaText = Bolinha.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        // freeze a rotacao da bolinha para fazer o trail
        Bolinha.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void OnMouseDown()
    {
        holding = true;

        data.updateNum(MinChargeNum);

        Debug.Log("Numero valor "  + data.getNum());

    }

    private void OnMouseUp()
    {
        holding = false;
        timer = 0;

        // pega a direcao do mouse e aplica a forca na bolinha
        Vector3 direction = (-1)*(Player.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        Player.GetComponent<ThrowBall>().ThrowBallInDirection(direction);

    }

    private void Update()
    {

        if (holding) {
            holdTime += Time.deltaTime;

            if (holdTime >= TimeToGrowForce) {
                holdTime = 0;

                if (data.getNum() < MaxChargeNum)
                {

                    Debug.Log("ASDADFASDF");

                    data.updateNum(data.getNum() * 2);

                    Debug.Log("Numero valor again " + data.getNum());
                    bolinhaText.text = data.getNum().ToString();
                }
            }
        }

    }
}
