using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] questions = new GameObject[5];

    [SerializeField]
    private GameObject[] answer = new GameObject[5];

    [SerializeField]
    private GameObject adelaide_Errou;

    [SerializeField]
    private GameObject blur_Errou;

    [SerializeField]
    private GameObject tutorial;

    [SerializeField]
    private GameObject finalScreen;

    [SerializeField]
    private AudioSource pointAudio;

    [SerializeField]
    private int currentStage = 0;

    public int QuantPlay = 0;

    [SerializeField]
    private GameObject caixaSom;
    [SerializeField]
    private GameObject cadeirante;
    [SerializeField]
    private GameObject menino_escorregador;
    [SerializeField]
    private Animator vowelAnimat;

    private void Awake()
    {
        ShuffleQuestions();

        for (int i = 0; i < answer.Length; i++)
        {

            answer[i].SetActive(false);
        }

        caixaSom.SetActive(false);

    }

    private void Start()
    {
        DisableButtons();
    }

    public void CloseTutoAndStart() //FECHA TUTORIAL E INICIA GAME
    {
        tutorial.SetActive(false);

        for (int i = 0; i < answer.Length; i++)
        {
            if (i != 2)
            {
                answer[i].SetActive(true);
            }
            else
            {
                //Ao fechar o tutorial, o Ioio não aparece imediatamente e sim a cadeirante.
                //No final da animação de entrada da mesma, ela chama um evento que faz o Ioio ser ativado.
                cadeirante.SetActive(true);
            }

        }

        //Ativa o menino escorregando, ativado após os outros para garantir que não vai escorregar no ar.
        menino_escorregador.SetActive(true);

        //Ativa icone da caixa de som
        caixaSom.SetActive(true);

        StartTurn();
    }

    private void StartTurn() //INICIA TURNO
    {
        //questions[currentStage].SetActive(true);
        questions[currentStage].GetComponent<Pergunta>().PlayMySound();
        StartCoroutine(WhaitForClick());
    }

    IEnumerator WrongAnswer() //RESPOSTA INCORRETA
    {

        blur_Errou.SetActive(true);
        adelaide_Errou.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);

        blur_Errou.SetActive(false);
        adelaide_Errou.SetActive(false);

    }



    public void repeatSound()
    {
        questions[currentStage].GetComponent<Pergunta>().PlayMySound();
    }

    public void CheckAnswer(int clickedId)
    {

        //IF Para checar se o botão clicado não foi a caixa de som.
        //Caso não tenha sido, irá cair na comparação de respostas.
        //Caso tenha sido, apenas irá repetir o audio da letra.
        if(clickedId != -1)
        {
            
            //Definindo uma variavel para a resposta correta, apenas para melhor legibilidade.
            int correctId = questions[currentStage].GetComponent<Pergunta>().GetMyId();
            QuantPlay++;

            if (clickedId == correctId)
            {
                //Checa se o ID correto é referente a Letra E, caso sim ativa o trigger que faz o garoto escorregar.
                if(clickedId == 2)
                {
                    menino_escorregador.GetComponent<Animator>().SetTrigger("escorregou");
                }


                vowelAnimat.SetInteger("vowel", correctId);
                vowelAnimat.SetTrigger("play_ani");


                CorrectAnswer();
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }

        } else {
            repeatSound();
        }

    }

    private void CorrectAnswer()
    {
        pointAudio.Play();
        if (currentStage < 4)
        {
            currentStage++;
            StartCoroutine(WhaitForNextTurn());

        }
        else
        {
            StartCoroutine(EndGame());
        }
    }


    private void DisableButtons()
    {

        caixaSom.GetComponent<Resposta>().DisableButton();

        for (int i = 0; i < answer.Length; i++)
        {
            answer[i].GetComponent<Resposta>().DisableButton();
        }
    }

    private void EnableButtons()
    {

        caixaSom.GetComponent<Resposta>().EnableButton();

        for (int i = 0; i < answer.Length; i++)
        {
            answer[i].GetComponent<Resposta>().EnableButton();
        }
    }


    IEnumerator WhaitForClick()
    {
        DisableButtons();
        yield return new WaitForSeconds(5.5f);
        EnableButtons();
    }

    IEnumerator WhaitForNextTurn()
    {


        DisableButtons();       
        

      yield return new WaitForSeconds(3.8f);
        vowelAnimat.SetInteger("vowel", 0);
        vowelAnimat.SetTrigger("play_ani");
        EnableButtons();

        StartTurn();
    }

    IEnumerator EndGame()
    {

        yield return new WaitForSeconds(5f);
        finalScreen.SetActive(true);
    }



    private void ShuffleQuestions()     //EMBARALHAR FRASES
    {
        for (int i = 0; i < questions.Length; i++)
        {
            GameObject obj = questions[i];
            int randomizeArray = Random.Range(0, i);
            questions[i] = questions[randomizeArray];
            questions[randomizeArray] = obj;
        }
    }


}
