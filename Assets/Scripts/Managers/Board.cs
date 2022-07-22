using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class Board : MonoBehaviour
{
    [SerializeField]
    Tile tilePrefab;

    [SerializeField]
    AudioClip ilkKareAcilisSesi;

    [SerializeField]
    AudioClip kareAcSesi;


    [SerializeField]
    GameObject backObje;

    GridLayoutGroup gridLayoutGroup;


    int xAdet, yAdet;

    int kareninEbati;
    int araBosluk = 5;

    int maviSayac = 0;
    int doluKareSayisi;



    bool maviBool;

    List<Tile> tilePrefabList=new List<Tile>();

    


    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }


    void Start()
    {
       
        
    }


    public void KareleriYerlestir(int xAdet,int yAdet,int doluKareSayisi)
    {
        //backObje.transform.DOScale(0.9f, 0.1f);

        this.gameObject.SetActive(true);
        backObje.gameObject.SetActive(true);

        this.transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        backObje.transform.DORotate(new Vector3(0, 0, 0), 0.1f);

        this.GetComponent<CanvasGroup>().alpha=0;
        backObje.GetComponent<CanvasGroup>().alpha = 0;


        backObje.GetComponent<CanvasGroup>().DOFade(1, 1f);
        this.GetComponent<CanvasGroup>().DOFade(1, 1f);



        this.xAdet = xAdet;
        this.yAdet = yAdet;
        this.doluKareSayisi = doluKareSayisi;

        

        AdeteGoreTileGenislik(xAdet);

        BoardGenislikAyarla(xAdet, yAdet);

        TileCogalt(xAdet,yAdet);
    }


    //karelerin ebatlarını ayarladığımız bölüm burası.
    void AdeteGoreTileGenislik(int xAdet)
    {
        switch(xAdet)
        {
            case 2:
                kareninEbati = 120;
                break;

            case 3:
                kareninEbati = 120;
                break;

            case 4:
                kareninEbati = 100;
                break;

            case 5:
                kareninEbati = 100;
                break;

            case 6:
                kareninEbati = 80;
                break;

            case 7:
                kareninEbati = 80;
                break;

            case 8:
                kareninEbati = 60;
                break;

        }

        gridLayoutGroup.cellSize = new Vector2(kareninEbati,kareninEbati);
        gridLayoutGroup.spacing = new Vector2(araBosluk, araBosluk);


    }


    //karelerin sayısına göre board nesnemizin enini ve boyunu ayarlıyoruz.
    void BoardGenislikAyarla(int xAdet,int yAdet)
    {
        int xGenislik = xAdet * kareninEbati + xAdet  * araBosluk;
        int yGenislik = yAdet * kareninEbati + yAdet * araBosluk;


        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(xGenislik, yGenislik);
        backObje.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(xGenislik + 25, yGenislik + 25);


    }

    //tile nesnemisi instantiete ediyoruz.
    void TileCogalt(int xAdet,int yAdet)
    {
        if (tilePrefabList.Count > 0)
        {
            tilePrefabList.Clear();
        }

        maviSayac = 0;
        maviBool = true;



        int childs = this.transform.childCount;


        if (childs > 0)
        {
            for (var i = childs - 1; i >= 0; i--)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }

        }


        if (tilePrefab)
        {
            for(int i=0;i<xAdet*yAdet;i++)
            {
                Tile tileObje = Instantiate(tilePrefab, transform.position, Quaternion.identity) as Tile;
               
                tileObje.GetComponent<Tile>().SetupWidthHeight(kareninEbati, kareninEbati);
                tileObje.transform.parent = this.transform;

                tilePrefabList.Add(tileObje);
            }
                  
        }

        

        StartCoroutine(MaviRenklariAcRoutine());
    }


    //açılır açılmaz mavi renkli geliyor. sonra sarıya dönüyor.
    IEnumerator MaviRenklariAcRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        if(PlayerPrefs.GetInt("SesDurumu")==1)
        {
            AudioSource.PlayClipAtPoint(ilkKareAcilisSesi, Camera.main.transform.position);
        }

       
       
        while(maviBool)
        {
            yield return new WaitForSeconds(0.01f);

            tilePrefabList[maviSayac].MaviKareRenginiAc();

            maviSayac++;


            if(maviSayac==xAdet*yAdet)
            {
                maviBool = false;

                


                yield return new WaitForSeconds(1f);

                tilePrefabList = tilePrefabList.OrderBy(i => Random.value).ToList();

                for (int i=0;i<doluKareSayisi;i++)
                {
                    tilePrefabList[i].SariRengiOtomatikAcKapat(true);
                }

                if(PlayerPrefs.GetInt("SesDurumu") == 1)
                {
                    AudioSource.PlayClipAtPoint(kareAcSesi, Camera.main.transform.position);
                }

                

                yield return new WaitForSeconds(1.7f);
               
                
                if(PlayerPrefs.GetInt("SesDurumu") == 1)
                {
                    AudioSource.PlayClipAtPoint(kareAcSesi, Camera.main.transform.position);
                }
               


                yield return new WaitForSeconds(1.3f);

              

                RotaysonuCevir();

                
                for (int i = 0; i < xAdet * yAdet; i++)
                {
                    tilePrefabList[i].SariRenginRotasyonunuDegistir();
                }
                yield return new WaitForSeconds(1f);

                TumPrefablariPasifYap();
            }

        }       
    }

   void TumPrefablariPasifYap()
    {
        for (int i = 0; i < xAdet * yAdet; i++)
        {
            tilePrefabList[i].acilmaBeklesin = false;
        }
    }


    public void ButonlariPasifYap()
    {
        for (int i = 0; i < xAdet * yAdet; i++)
        {
            tilePrefabList[i].acilmaBeklesin = true;
            
        }
        Invoke("BoardGorunmezYap", 2f);
    }

    public void TumPrefablariAktifYap()
    {
        for (int i = 0; i < xAdet * yAdet; i++)
        {
            tilePrefabList[i].acilmaBeklesin = true;
            tilePrefabList[i].DogruOlanlarAcilsin();
        }

        Invoke("BoardGorunmezYap", 2f);
    }


   public void BoardGorunmezYap()
    {
        this.transform.DOScale(1.2f, .3f).SetEase(Ease.InBack).OnComplete(BoardDeaktifYap);
        this.GetComponent<CanvasGroup>().DOFade(0, .3f).SetDelay(0.3f);

        backObje.transform.DOScale(1.2f, .3f).SetEase(Ease.InBack);
        backObje.GetComponent<CanvasGroup>().DOFade(0, .3f).SetDelay(0.3f);

    }


    void BoardDeaktifYap()
    {
        this.gameObject.SetActive(false);
        backObje.gameObject.SetActive(false);
    }



   


    void RotaysonuCevir()
    {
        int rastgeleDeger = Random.Range(1, 100);
        Vector3 rot;

        if(rastgeleDeger<50)
        {
            rot = new Vector3(0f, 0f, 90f);
        } else

        {
            rot = new Vector3(0f, 0f, -90f);
        }

        this.transform.DORotate(rot, 1f);
        backObje.transform.DORotate(rot, 1f);
    }

    



}
