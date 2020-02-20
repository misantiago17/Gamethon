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

    private int currentValue = 1;

    // Precisa pegar o Ball Data do ultimo tiro

    private GameObject currentBolinha;


    private void Start()
    {

        // freeze a rotacao da bolinha para fazer o trail
        Bolinha.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void OnMouseDown() { 

        if (!Pause.Instance.isOnPause) {
            holding = true;

            currentValue = 1;
            currentBolinha = Instantiate(Bolinha, Player.transform.position, this.transform.rotation);
        }
        
    }

	private void OnMouseUp()
    {
        holding = false;
        timer = 0;

        // pega a direcao do mouse e aplica a forca na bolinha
        Vector3 direction = (-1)*(Player.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        currentBolinha.GetComponent<BulletThrow>().ThrowBallInDirection(direction);
        //GameObject.FindGameObjectWithTag("Aura").GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (holding) {
            holdTime += Time.deltaTime;

            if (holdTime >= TimeToGrowForce) {
                holdTime = 0;

                if (currentValue < MaxChargeNum)
                {
                    currentValue *= 2;
                    currentBolinha.GetComponent<BulletInstance>().UpdateBulletValue(currentValue);
                }
            }
        }        

    }
}
