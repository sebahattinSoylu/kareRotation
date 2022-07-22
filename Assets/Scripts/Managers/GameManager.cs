using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject araSonucObje;

    [SerializeField]
    Text sonrakiDesenTxt, buAdimdaAlinanPuanTxt;

    [SerializeField]
    AudioClip bolumGecisSesi;

    [SerializeField]
    GameObject[] kalanHaklarObje;

    [SerializeField]
    Text puanTxt;


    [SerializeField]
    GameObject sesAcikObje, sesKapaliObje;

    Board board;

    int kacinciOyun;

    int xAdet, yAdet;

    int dolacakKaresayisi;
    int kacKareDoldu;


    int bolumPuani;
    
    public int kalanHak;

    int toplamPuan;

    int sesDurumu;

    SonucManager sonucManager;
    


    private void Awake()
    {
        board = Object.FindObjectOfType<Board>();
        sonucManager = Object.FindObjectOfType<SonucManager>();
        
    }

    void Start()
    {
        sesDurumu = PlayerPrefs.GetInt("SesDurumu");

       


        if (sesDurumu == 1)
        {

            sesAcikObje.SetActive(true);
            sesKapaliObje.SetActive(false);

        } else if(sesDurumu==0)
        {
            sesAcikObje.SetActive(false);
            sesKapaliObje.SetActive(true);
        }

      

       if(PlayerPrefs.GetInt("ReklamIzlendimi")==1)
        {
            kacinciOyun = PlayerPrefs.GetInt("KacinciOyun");
            toplamPuan = PlayerPrefs.GetInt("ToplamPuan");
        } else

        {
            kacinciOyun = 0;
            toplamPuan = 0;
        }

        puanTxt.text = toplamPuan.ToString();

        kacKareDoldu = 0;

        bolumPuani = 0;
        kalanHak = 3;
       

        puanTxt.text = toplamPuan.ToString();

        for(int i=0;i<kalanHaklarObje.Length;i++)
        {
            kalanHaklarObje[i].SetActive(true);
        }

        araSonucObje.GetComponent<CanvasGroup>().alpha = 0;
        araSonucObje.transform.DORotate(new Vector3(0, 0, 90), 0.1f);
        araSonucObje.transform.DOScale(1.4f, 0.1f);
        araSonucObje.SetActive(false);

      


        KarelerinSayisiniBelirle(kacinciOyun);
    }


    //karelerin sayısını belirliyoruz.
    void KarelerinSayisiniBelirle(int kacinciOyun)
    {
        switch(kacinciOyun)
        {
            case 0:
                xAdet = 2;
                yAdet = 2;
                break;

            case 1:
                xAdet = 3;
                yAdet = 2;
                break;

            case 2:
                xAdet = 3;
                yAdet = 3;
                break;

            case 3:
                xAdet = 4;
                yAdet = 3;
                break;

            case 4:
                xAdet = 4;
                yAdet = 4;
                break;

            case 5:
                xAdet = 5;
                yAdet = 4;
                break;

            case 6:
                xAdet = 5;
                yAdet = 5;
                break;

            case 7:
                xAdet = 6;
                yAdet = 5;
                break;

            case 8:
                xAdet = 6;
                yAdet = 6;
                break;

            case 9:
                xAdet = 7;
                yAdet = 6;
                break;

            case 10:
                xAdet = 7;
                yAdet = 7;
                break;

            case 11:
                xAdet = 8;
                yAdet = 7;
                break;

            case 12:
                xAdet = 8;
                yAdet = 8;

                //xAdet = 2;
                //yAdet = 2;

                break;
        }

        if(board)
        {
            board.KareleriYerlestir(xAdet, yAdet, kacinciOyun + 2);
            dolacakKaresayisi = kacinciOyun + 2;
            //board.KareleriYerlestir(xAdet, yAdet,  2);
            //dolacakKaresayisi = 2;
        }


    }

    public void SonucuKontrolEt(bool sonucDogrumu)
    {
        
        if(sonucDogrumu)
        {
            kacKareDoldu++;
          
            
            if (kacKareDoldu >= dolacakKaresayisi)
            {
                board.ButonlariPasifYap();

                kacKareDoldu = 0;
                bolumPuani = dolacakKaresayisi * 50;
                toplamPuan += bolumPuani;
              
                if (kacinciOyun<12)
                {
                    kacinciOyun++;
                   
                   

                    if (araSonucObje)
                    {
                        
                        sonrakiDesenTxt.text = (dolacakKaresayisi + 1).ToString();
                       
                        puanTxt.text = toplamPuan.ToString();
                        Invoke("AraSonucuGoster", 2.3f);
                    }
                } else
                {
                    Invoke("SonucaGit", 2.3f);
                }

            }
        } else
        {
            bolumPuani = 0;


            if(kacinciOyun>0)
            {
                kacinciOyun--;
            }

            if(kacinciOyun<=0)
            {
                kacinciOyun = 0;
                dolacakKaresayisi = 3;
            }


            sonrakiDesenTxt.text = (dolacakKaresayisi-1).ToString();

            kalanHak--;
            kalanHaklarObje[kalanHak].SetActive(false);

            if (kalanHak<=0)
            {
                kalanHak = 0;
                board.BoardGorunmezYap();
                Invoke("SonucaGit",.2f);
                
                
            } else
            {
                Invoke("AraSonucuGoster", 2.3f);
            }

           

           
        }

        

        
    }

   


    void AraSonucuGoster()
    {
        board.BoardGorunmezYap();

        if(sesDurumu==1)
        {

            AudioSource.PlayClipAtPoint(bolumGecisSesi, Camera.main.transform.position);
        }



        if(kacinciOyun<12)
        {
            araSonucObje.SetActive(true);
            araSonucObje.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutBack);
            araSonucObje.transform.DOScale(.8f, 0.5f);
            araSonucObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            buAdimdaAlinanPuanTxt.text = bolumPuani.ToString() + " Puan";
        }    


    }

    void SonucaGit()
    {

        if (sonucManager)
        {
            PlayerPrefs.SetInt("KacinciOyun", kacinciOyun);
            sonucManager.SonucuGoster(toplamPuan);

            PlayerPrefs.SetInt("ToplamPuan", toplamPuan);

            return;
        }
    }

    public void SonrakiAdimaGec()
    {
        if(kacinciOyun>=12)
        {
            return;
        }


        if(kacinciOyun<12)
        {
            
            araSonucObje.transform.DORotate(new Vector3(0, 0, 90), 0.3f).SetEase(Ease.InBack);
            araSonucObje.transform.DOScale(1.4f, 0.3f);
            araSonucObje.GetComponent<CanvasGroup>().DOFade(0, 0.3f);


            kacKareDoldu = 0;
            Invoke("ArasonucuGorunmezYap", 0.3f);

           
        }
    }


    void ArasonucuGorunmezYap()
    {
        araSonucObje.SetActive(false);
        KarelerinSayisiniBelirle(kacinciOyun);
    }



    public void SesAcKapat()
    {
        if (sesAcikObje && sesKapaliObje)
        {
            if (sesDurumu == 1)
            {

               
                sesAcikObje.SetActive(false);
                sesKapaliObje.SetActive(true);

                sesDurumu = 0;
                PlayerPrefs.SetInt("SesDurumu", sesDurumu);


            }
            else if (sesDurumu == 0)
            {


                sesAcikObje.SetActive(true);
                sesKapaliObje.SetActive(false);

                sesDurumu = 1;
                PlayerPrefs.SetInt("SesDurumu", sesDurumu);
            }

        }
    }

    public void OyundanCik()
    {
        Application.Quit();
    }


}
