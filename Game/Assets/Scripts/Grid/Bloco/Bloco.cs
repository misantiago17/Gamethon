using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco {

    private int lineID;
    private int lineIndex;

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

    // Pega e Seta para o index do bloco dentro da line
    public int getBlockIndexInLine() {
        return lineIndex;
    }

    public void setBlockIndexInLine(int index) {
        this.lineIndex = index;
    } 
    
    // Pega e Seta para a ID da linha onde o bloco está presente
    public int getBlockLineID() {
        return lineID;
    }

    public void setBlockLineID(int ID) {
        this.lineID = ID;
    }
}
