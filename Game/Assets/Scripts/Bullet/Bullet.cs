using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet {

    // valores possiveis são: 1, 2, 4 ou 16
    private int value = 1;

    private int maxValue = 8;

    // Pega e Seta valores pro tiro
    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }

    // Pega e Seta valor máximo do tiro
    public int getMaxPosibleValue() {
        return maxValue;
    }

    public void setMaxPosibleValue(int value) {
        this.maxValue = value;
    }

}
