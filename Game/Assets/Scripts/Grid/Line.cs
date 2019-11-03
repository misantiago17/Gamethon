using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line {
    private int ID;

    private int numberOfBlocks = 0;
    private int maxNumOfBlocks = 5;

    // Pega e Seta valores de ID para a linha
    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    // Pega, seta ou diminui o numero de blocos na linha
    public int getNumOfBlocks() {
        return numberOfBlocks;
    }

    public void setNumOfBlocks(int num) {
        this.numberOfBlocks = num;
    }

    public void decreaseNumOfBlocks() {
        if (numberOfBlocks > 0) {
            this.numberOfBlocks -= 1;
        } else {
            Debug.LogError("Numero de blocos na linha já é igual a 0");
        }
    }

    // Pega e Seta numero máximo de blocos para a linha
    public int getMaxBlockNum() {
        return maxNumOfBlocks;
    }

    public void setMaxBlockNum(int maxNum) {
        this.maxNumOfBlocks = maxNum;
    }

}
