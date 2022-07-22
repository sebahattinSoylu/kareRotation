using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{

    [SerializeField]
    float width;


    [SerializeField]
    GameObject  leftYellowTile,rightYellowTile,falseImage,blueImage;

    [SerializeField]
    AudioClip kareAcilisSesi,hataSesi;


    GameManager gameManager;
    Board board;

    public bool acilacakOlanmi;

    public bool acilmaBeklesin;


    bool acildimi;

   
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        board = Object.FindObjectOfType<Board>();
        
    }

    void Start()
    {

        


        falseImage.SetActive(false);
        // blueImage.SetActive(false);
        acilmaBeklesin = true;

        acildimi = false;

    }

    

    public void SetupWidthHeight(float width,float height)
    {

        this.transform.DOLocalRotate(new Vector3(0,0,0), 0f);
        this.transform.DOScale(1f, .1f);

        leftYellowTile.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2, height);
        rightYellowTile.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2, height);

        this.width = width;


        float falseEbat=width*0.9f;
        falseImage.GetComponent<RectTransform>().sizeDelta = new Vector2(falseEbat, falseEbat);

        
    }



  


    public void PressButton()
    {
        if(acilmaBeklesin)
        {
            return;
        }

        if(acildimi)
        {

            if(PlayerPrefs.GetInt("SesDurumu") == 1)
            {
                AudioSource.PlayClipAtPoint(hataSesi, Camera.main.transform.position);
            }

           
            this.transform.DOScale(0.9f, .1f).OnComplete(ScaleAc);
            return;
        }


        if(acilacakOlanmi)
        {

            if(PlayerPrefs.GetInt("SesDurumu") == 1)
            {
                AudioSource.PlayClipAtPoint(kareAcilisSesi, Camera.main.transform.position);
            }

           

            acildimi = true;

            
            leftYellowTile.GetComponent<RectTransform>().DOAnchorPosX(-width / 2 - 1f, 0.2f);
            rightYellowTile.GetComponent<RectTransform>().DOAnchorPosX(width / 2 + 1f, 0.2f);
        } else
        {

            if(PlayerPrefs.GetInt("SesDurumu") == 1)
            {
                AudioSource.PlayClipAtPoint(hataSesi, Camera.main.transform.position);
            }

           
            board.TumPrefablariAktifYap();
           
            falseImage.SetActive(true);
        }
       

        gameManager.SonucuKontrolEt(acildimi);


    }

    void ScaleAc()
    {
        this.transform.DOScale(1f, .1f);
    }

    void CloseYellowTiles()
    {
        leftYellowTile.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);
        rightYellowTile.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);
    }


    public void SariRengiOtomatikAcKapat(bool acilsinmi)
    {
        if(acilsinmi)
        {
            this.acilacakOlanmi = true;
            


            leftYellowTile.GetComponent<RectTransform>().DOAnchorPosX(-width / 2 - 1f, 0.2f);
            rightYellowTile.GetComponent<RectTransform>().DOAnchorPosX(width / 2 + 1f, 0.2f);

            Invoke("CloseYellowTiles", 2f);
        }
    }

    public void DogruOlanlarAcilsin()
    {
        if(!acildimi && acilacakOlanmi)
        {
            leftYellowTile.GetComponent<RectTransform>().DOAnchorPosX(-width / 2 - 1f, 0.2f);
            rightYellowTile.GetComponent<RectTransform>().DOAnchorPosX(width / 2 + 1f, 0.2f);
        }

        
    }

    public void MaviKareRenginiAc()
    {
        blueImage.GetComponent<CanvasGroup>().DOFade(0f, .5f);
    }

    public void SariRenginRotasyonunuDegistir()
    {
        Vector3 rot;
        rot = new Vector3(0, 0, 90f);
        this.transform.DOLocalRotate(rot, 0f);


      
    }


}
