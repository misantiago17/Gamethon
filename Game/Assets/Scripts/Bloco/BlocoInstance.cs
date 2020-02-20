using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource), typeof(SpriteRenderer))]
public class BlocoInstance : MonoBehaviour {

    public int MaxBlockValue = 16;

    [Tooltip("Animação para que um bloco sozinho na fileira ou de valor 16 se auto destrua")]
    public Animation AutoDestructAnimation; 

    [Space (5)]
    public Sprite[] TileSprite;
    public RuntimeAnimatorController[] AnimationController;

    // Required
    private AudioSource audioPunch;

    private Bloco blockData;

    /// ------- Funções de básicas ----------

    private void Start() {
        if (GameObject.FindGameObjectWithTag("PunchSFX"))
            audioPunch = GameObject.FindGameObjectWithTag("PunchSFX").GetComponent<AudioSource>();
    }

    /// ------- Funções de controle de valor do bloco ----------

    public int getBlockValue() {
        return blockData.getValue();
    }

    private void updateBlockValue(int value) {
        blockData.setValue(value);

        GameObject line = this.transform.parent.gameObject;
        line.GetComponent<LineInstance>().UpdateBlockPatternValue(value,blockData.getBlockIndexInLine());

        updateBlockSkin();
    }

    private void updateBlockSkin() {

        int blockValue = blockData.getValue();

        SpriteRenderer spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Animator animator = gameObject.GetComponent<Animator>();

        switch (blockValue) {

            // verde
            case 1:
                spriteRend.sprite = TileSprite[0];
                animator.runtimeAnimatorController = AnimationController[0];
                break;

            // amarelo
            case 2:
                spriteRend.sprite = TileSprite[1];
                animator.runtimeAnimatorController = AnimationController[1];
                break;

            // laranja
            case 4:
                spriteRend.sprite = TileSprite[2];
                animator.runtimeAnimatorController = AnimationController[2];
                break;

            // vermelho
            case 8:
                spriteRend.sprite = TileSprite[3];
                animator.runtimeAnimatorController = AnimationController[3];
                break;

            // roxo
            case 16:
                spriteRend.sprite = TileSprite[4];
                animator.runtimeAnimatorController = AnimationController[4];
                AutodestructBlock();
                break;

            // ideal é ter um bloco com uma imagem propositalmente errada (tipo um ! ou X)  ----- !
            // por enquanto é amarelo
            default:
                Debug.LogError("Valor do bloco errado");
                spriteRend.sprite = TileSprite[0];
                animator.runtimeAnimatorController = AnimationController[0];
                break;

        }

    }

    /// ------- Funções de posicao e localizacao do bloco ----------

    // toda vez que um novo block é criado essa função será chamada
    public void SetBlockData(int value, int lineID, int lineIndex) {

        if(blockData == null) 
            blockData = new Bloco();

        blockData.setMaxPosibleValue(MaxBlockValue);
        blockData.setValue(value);
        blockData.setBlockLineID(lineID);
        blockData.setBlockIndexInLine(lineIndex);

        updateBlockSkin();
    }

    public int getBlockLineID() {
        return blockData.getBlockLineID();
    }

    public int getBlockLineIndex() {
        return blockData.getBlockIndexInLine();
    }

    /// ------- Funções de colisão e merge do bloco ----------

    private void OnCollisionEnter2D(Collision2D collision) {

        // verifica se foi atingido pelo tiro 
        // se for um tiro de mesmo valor realiza o merge
        // caso contrário ignora a colisão com o tiro
        if (collision.gameObject.CompareTag("Tirinho")) {

            // avalia se o tiro tem o componente requerido
            if (collision.gameObject.GetComponent<BulletInstance>()) {
                int bulletValue = collision.gameObject.GetComponent<BulletInstance>().getBulletValue();
                int blockValue = blockData.getValue();

                // Avalia se o tiro possui o mesmo valor que o bloco e se o bloco já não está em seu ultimo valor
                // Se o valor do bloco for diferente o tiro é rebatido e ignorado
                // Se o valor do bloco for igual ou maior que o máximo possivel ele ignora pois o bloco vai se destrui sozinho em pouco tempo
                // Caso contrário dos dois, realiza o merge
                if ((bulletValue == blockValue) && (blockValue < blockData.getMaxPosibleValue())) {

                    // seta o novo valor
                    int newValue = blockValue * 2;
                    updateBlockValue(newValue);

                    // Faz o merge 
                    GameObject line = this.transform.parent.gameObject;
                    line.GetComponent<LineInstance>().MergeCheck(blockData.getBlockIndexInLine());

                    // BIG HUGE OBS: ----------------------------------------------------------------!
                    // o outro código estava tentando repetir as iterações do merge por aqui (coisa nada saudavel)
                    // o objetivo é tentar deixar o merge recursivo pra caralho 
                    // 
                    // Outra coisa que esse código estava fazendo era verificar se o bloco estava sozinho na fileira
                    // Aqui deve ficar somente o codigo que faz o bloco sumir
                    // que deve ser chamado por onde a grid estará vericando se a linha está sozinha ou não
                    //
                    // o merge também está funcionando em funcao do bloco
                    // o ideal é ele funcionar em funcao da linha do bloco com referencia ao bloco atingido 


                    // destroí o tiro
                    Destroy(collision.gameObject);

                    // Respawna o player em outra posição (avaliar se é bom isso ou não) -------- !
                    //Player.gameObject.GetComponent<SpawnBall>().respawnBall(this.transform.position.x);

                }

            } else {
                Debug.LogError("Falta componente BulletInstance no tiro");
            }

        }
        
    }

    public void AutodestructBlock() {
        StartCoroutine(Autodestruct());
    }

    // ------------------- !
    private IEnumerator Autodestruct() {

        if (AutoDestructAnimation) {
            yield return WaitForAnimation(AutoDestructAnimation);
        } else {
            yield return new WaitForSeconds(1.5f);
        }
        // retira da grid
        GridManager.Instance.RemoveBlockFromLine(this.gameObject);

        // Retira do jogo
        this.gameObject.SetActive(false);

        // Retira do jogo
        //Destroy(this.gameObject);

        // Faz o merge das colunas ao lado se ainda houver vizinhos 
        //MergeBlocks.Instance.MergeCheck(this.gameObject);

    }

    private IEnumerator WaitForAnimation(Animation animation) {
        do {
            yield return null;
        } while (animation.isPlaying);
    }

}
