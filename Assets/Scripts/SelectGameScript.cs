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

    // Start is called before the first frame update
    void Start()
    {
        blackJack.onClick.AddListener(() => GoToBlackJack());
        highLow.onClick.AddListener(() => GoToHighLow());
        fivePoker.onClick.AddListener(() => GoToFivePoker());
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
}
