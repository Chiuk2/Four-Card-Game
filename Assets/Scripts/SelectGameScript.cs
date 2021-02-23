using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectGameScript : MonoBehaviour
{
    public Button blackJack;
    public Button highLow;
    public Button fivePoker;
    public Button sevenPoker;
    public Button videoPoker;

    // Start is called before the first frame update
    void Start()
    {
        blackJack.onClick.AddListener(() => GoToBlackJack());
        highLow.onClick.AddListener(() => GoToHighLow());
        fivePoker.onClick.AddListener(() => GoToFivePoker());
        sevenPoker.onClick.AddListener(() => GoToSevenPoker());
        videoPoker.onClick.AddListener(() => GoToVideoPoker());
    }

    private void GoToBlackJack()
    {
        SceneManager.LoadScene("BlackJack");
    }

    private void GoToHighLow()
    {
        SceneManager.LoadScene("HighLow");
    }

    private void GoToFivePoker()
    {
        SceneManager.LoadScene("FivePoker");
    }

    private void GoToSevenPoker()
    {
        SceneManager.LoadScene("SevenPoker");
    }

    private void GoToVideoPoker()
    {
        SceneManager.LoadScene("FiveVideoPoker");
    }
}
