using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstance : MonoBehaviour {

    public GameObject Aura;
    public Color[] AuraColors;

    [Space(3)]
    public Sprite[] BulletSprite;

    [Space(5)]
    public int MaxBulletValue = 8;

    private Bullet bulletData;

    /// ------- Funções de básicas ----------

    private void Start() {
        bulletData.setMaxPosibleValue(MaxBulletValue);
    }

    /// ------- Funções de controle de valor do tiro ----------

    public int getBulletValue() {
        return bulletData.getValue();
    }

    public void UpdateBulletValue(int value) {
        bulletData.setValue(value);
        updateBulletSkin();
    }

    private void updateBulletSkin() {

        int bulletValue = bulletData.getValue();

        SpriteRenderer spriteRend = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer auraRend = null;

        if (Aura) {
            auraRend = Aura.GetComponent<SpriteRenderer>();
        } else {
            Debug.LogError("Falta componente SpriteRenderer na aura");
        }

        switch (bulletValue) {

            // verde
            case 1:
                spriteRend.sprite = BulletSprite[0];
                if (auraRend) { auraRend.color = AuraColors[0]; }
                break;

            // amarelo
            case 2:
                spriteRend.sprite = BulletSprite[1];
                if (auraRend) { auraRend.color = AuraColors[1]; }
                break;

            // laranja
            case 4:
                spriteRend.sprite = BulletSprite[2];
                if (auraRend) { auraRend.color = AuraColors[2]; }
                break;

            // vermelho
            case 8:
                spriteRend.sprite = BulletSprite[3];
                if (auraRend) { auraRend.color = AuraColors[3]; }
                break;

            // roxo
            case 16:
                spriteRend.sprite = BulletSprite[4];
                if (auraRend) { auraRend.color = AuraColors[4]; }
                break;

            // ideal é ter um tiro com uma imagem propositalmente errada (tipo um ! ou X)  ----- !
            // por enquanto é amarelo
            default:
                Debug.LogError("Valor do tiro errado");
                spriteRend.sprite = BulletSprite[0];
                if (auraRend) { auraRend.color = AuraColors[0]; }
                break;
        }

    }

}
