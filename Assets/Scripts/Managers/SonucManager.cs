using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{

    [SerializeField]
    Text sonucTxt;

    [SerializeField]
    GameObject sonucPanel;

    [SerializeField]
    GameObject haklarPanel, puanPanel;

    [SerializeField]
    AudioClip bitisSesi;

    [SerializeField]
    GameObject reklamPanel;

    int sesDurumu;

    void Start()
    {
        sesDurumu = PlayerPrefs.GetInt("SesDurumu");



        if(sonucPanel)
        {
            sonucPanel.GetComponent<CanvasGroup>().alpha = 0;
            sonucPanel.transform.DORotate(new Vector3(0, 0, -90f), 0.02f);
            sonucPanel.SetActive(false);
        }


        if(reklamPanel)
        {
            reklamPanel.GetComponent<CanvasGroup>().alpha = 0;
            reklamPanel.SetActive(false);
        }

    }


    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene("GamePlay");
    }


    public void SonucuGoster(int toplamPuan)
    {
        if(sonucTxt)
        {
            sonucTxt.text = toplamPuan.ToString() + " P";
        }

        if(sonucPanel)
        {
            sonucPanel.SetActive(true);

            if(PlayerPrefs.GetInt("SesDurumu") == 1)
            {
                AudioSource.PlayClipAtPoint(bitisSesi, Camera.main.transform.position);
            }



            haklarPanel.SetActive(false);
            puanPanel.SetActive(false);


            sonucPanel.transform.DORotate(new Vector3(0, 0, 0f),1.2f).SetEase(Ease.OutBack);
            sonucPanel.GetComponent<CanvasGroup>().DOFade(1, 1.2f);

            reklamPanel.SetActive(true);
            reklamPanel.GetComponent<CanvasGroup>().DOFade(1, 2f);

        }


        
    }

    public void GameExit()
    {
        Application.Quit();

    }

}
