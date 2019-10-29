using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco {

    private int lineID;
    private int linePosition;

    // valores possiveis são: 1, 2, 4, 8 ou 16
    private int value = 1;

    private int maxValue = 16;

    // Pega e Seta valores pro bloquinho
    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }

    // Pega e Seta valor máximo do bloco
    public int getMaxPosibleValue() {
        return maxValue;
    }

    public void setMaxPosibleValue(int value) {
        this.maxValue = value;
    }

}
