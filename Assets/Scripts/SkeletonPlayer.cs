using UnityEngine;

public class SkeletonPlayer : MonoBehaviour

{
    // --------------------------------------------------
    // CONFIGURAÇÕES DE MOVIMENTO
    // --------------------------------------------------

    [Header("Movimento")]
    [SerializeField] private float velocidadeAndar = 2f;
    [SerializeField] private float velocidadeCorrer = 4f;
    [SerializeField] private float velocidadeAndarTras = 1.5f;
    [SerializeField] private float velocidadeRotacao = 120f;


    // --------------------------------------------------
    // CONFIGURAÇÕES DE PULO
    // --------------------------------------------------

    [Header("Pulo")]
    [SerializeField] private float alturaPulo = 1.5f;


    // --------------------------------------------------
    // CONFIGURAÇÕES DE GRAVIDADE
    // --------------------------------------------------

    [Header("Gravidade")]
    [SerializeField] private float gravidade = -9.81f;


    // --------------------------------------------------
    // COMPONENTES
    // --------------------------------------------------

    private Animator animator;
    private CharacterController controller;


    // Guarda a velocidade atual da caveira no eixo vertical.
    private float velocidadeVertical;


    void Start()

    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    {
    }
void Movimento()
{
    bool pressionandoW = Input.GetKey(KeyCode.W);
    bool pressionandoS = Input.GetKey(KeyCode.S);

    bool estaMovendo =
        pressionandoW && !pressionandoS;

    bool estaMovendoParaTras =
        pressionandoS && !pressionandoW;

    if (estaMovendo == trus)
        return estaMovendo;

    if (estaMovendoParaTras == true)
        return estaMovendoParaTras;



    bool estaCorrendo =
            estaMovendo &&
            (Input.GetKey(KeyCode.LeftShift) ||
             Input.GetKey(KeyCode.RightShift));

    if (Input.GetMouseButtonDown(0) && controller.isGrounded)
    {
        animator.SetTrigger("Attack");
    }
}

float direcaoRotacao = 0f;

if (Input.GetKey(KeyCode.A))
{
    direcaoRotacao = -1f;
}

if (Input.GetKey(KeyCode.D))
{
    direcaoRotacao = 1f;
}

transform.Rotate(
    0f,
    direcaoRotacao * velocidadeRotacao * Time.deltaTime,
    0f
);

float velocidadeAtual = 0f;

if (estaMovendo)
{
    if (estaCorrendo)
    {
        velocidadeAtual = velocidadeCorrer;
    }
    else
    {
        velocidadeAtual = velocidadeAndar;
    }
}
else if (estaMovendoParaTras)
{
    velocidadeAtual = -velocidadeAndarTras;
}

bool estaNoChao = controller.isGrounded;

if (estaNoChao && velocidadeVertical < 0f)
{
    velocidadeVertical = -2f;
}

if (estaNoChao && Input.GetKeyDown(KeyCode.Space))
{
    velocidadeVertical =
        Mathf.Sqrt(
            alturaPulo * -2f * gravidade
        );
}

velocidadeVertical +=
    gravidade * Time.deltaTime;

Vector3 movimento =
            transform.forward * velocidadeAtual;

// Acrescenta o movimento vertical causado pela gravidade.
movimento.y = velocidadeVertical;

// Executa os movimentos juntos.
controller.Move(
    movimento * Time.deltaTime
);

bool estaCaindo =
           !controller.isGrounded &&
           velocidadeVertical < -0.1f;

bool estaPulando =
            !controller.isGrounded &&
            velocidadeVertical > 0.1f;

void Update()
{
    // --------------------------------------------------
    // MOVIMENTO PARA FRENTE E PARA TRÁS
    // --------------------------------------------------
    Movimento();



    // --------------------------------------------------
    // CORRIDA
    // --------------------------------------------------

    // Só pode correr quando estiver indo para frente.
    Movimento();


    // --------------------------------------------------
    // ATAQUE
    // --------------------------------------------------

    // O botão esquerdo do mouse executa o ataque.
    // Por enquanto, o ataque só pode ser realizado no chão.
    Movimento();


    // --------------------------------------------------
    // ROTAÇÃO
    // --------------------------------------------------
    Movimento();


    // --------------------------------------------------
    // VELOCIDADE HORIZONTAL
    // --------------------------------------------------
    Movimento();



    // --------------------------------------------------
    // GRAVIDADE
    // --------------------------------------------------
    Movimento();



    // --------------------------------------------------
    // PULO
    // --------------------------------------------------

    // Só permite pular se estiver no chão.
    Movimento();



    // --------------------------------------------------
    // MOVIMENTO FINAL
    // --------------------------------------------------
    Movimento();




    // --------------------------------------------------
    // DETECÇÃO DE QUEDA
    // --------------------------------------------------

    // A caveira está caindo quando:
    //
    // 1. não está encostando no chão;
    // 2. sua velocidade vertical está apontando para baixo.
    Movimento();


    // --------------------------------------------------
    // DETECÇÃO DE PULO
    // --------------------------------------------------

    // A caveira está pulando enquanto estiver no ar
    // e sua velocidade vertical estiver apontando para cima.
    Movimento();


    // --------------------------------------------------
    // ANIMAÇÕES
    // --------------------------------------------------

    animator.SetBool(
            "IsMoving",
            estaMovendo
        );

    animator.SetBool(
        "IsRunning",
        estaCorrendo
    );

    animator.SetBool(
        "IsMovingBackward",
        estaMovendoParaTras
    );

    animator.SetBool(
        "IsFalling",
        estaCaindo
    );

    animator.SetBool(
        "IsJumping",
        estaPulando
    );
}
}

}

