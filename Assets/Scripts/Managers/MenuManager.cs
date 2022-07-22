using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject sesAcikObje, sesKapaliObje;

    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject logoObje;

    int sesDurumu;

   



    void Start()
    {

        sesDurumu = 1;

        PlayerPrefs.SetInt("SesDurumu", sesDurumu);


        PaneliAc();
        LogoyuAc();


    }

    
    void PaneliAc()
    {
        menuPanel.GetComponent<CanvasGroup>().alpha = 0;

        menuPanel.transform.DORotate(Vector3.zero, .7f).SetEase(Ease.OutBack);

        menuPanel.GetComponent<CanvasGroup>().DOFade(1f, 1f);


    }


    void LogoyuAc()
    {
        logoObje.GetComponent<CanvasGroup>().alpha = 0;

        logoObje.transform.DOLocalMoveX(-450f,1f).SetEase(Ease.OutBack);

        logoObje.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }


    public void SesAcKapat()
    {
        if(sesAcikObje && sesKapaliObje)
        {
            if(sesDurumu==1)
            {
                

                sesAcikObje.SetActive(false);
                sesKapaliObje.SetActive(true);

                sesDurumu = 0;
                PlayerPrefs.SetInt("SesDurumu", sesDurumu);


            } else if(sesDurumu==0)
            {
             

                sesAcikObje.SetActive(true);
                sesKapaliObje.SetActive(false);

                sesDurumu = 1;
                PlayerPrefs.SetInt("SesDurumu", sesDurumu);
            }         
           
        }
    }




    public void GamePlay()
    {
              SceneManager.LoadScene("GamePlay");
    }


    public void GameExit()
    {
        Application.Quit();
    }




}
