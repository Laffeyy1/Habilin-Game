using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class LibraryManager : MonoBehaviour, IDataPersistence
{
    [Header("Library Info UI")]
    public GameObject libraryUI;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Library Buttons")]
    public Button sinigangBtn;
    public Button palabokBtn;
    public Button lechonBtn;
    public Button adoboBtn;
    public Button tinolaBtn;
    public Button lugawBtn;
    public Button sopasBtn;
    public Button champoradoBtn;
    public Button bibingkaBtn;

    public Button balisongBtn;
    public Button krisBtn;
    public Button boloBtn;
    public Button barongBtn;
    public Button punyalBtn;
    public Button sundangBtn;
    public Button kampilanBtn;
    public Button arnisBtn;

    public Button lapuBtn;
    public Button kugsanoBtn;
    public Button rajahBtn;
    public Button magellanBtn;
    public Button andresBtn;
    public Button catalinaBtn;
    public Button ladislaoBtn;
    public Button teodroBtn;
    public Button deodatoBtn;

    public Button kuboBtn;
    public Button mactanBtn;
    public Button kkkBtn;
    public Button cedulaBtn;
    public Button manilaBtn;

    [Header("Library Item Assets")]
    public LibraryItem sinigangSO;
    public LibraryItem palabokSO;
    public LibraryItem lechonSO;
    public LibraryItem adoboSO;
    public LibraryItem tinolaSO;
    /*public LibraryItem kareKareSO;*/
    public LibraryItem lugawSO;
    public LibraryItem sopasSO;
    public LibraryItem champoradoSO;
    /*public LibraryItem haloHaloSO;
    public LibraryItem balutSO;
    public LibraryItem putoSO;*/
    public LibraryItem bibingkaSO;
    /*public LibraryItem tahoSO;
    public LibraryItem sisigSO;
    public LibraryItem laingSO;
    public LibraryItem longganisaSO;*/

    public LibraryItem balisongSO;
    public LibraryItem krisSO;
    public LibraryItem boloSO;
    public LibraryItem barongSO;
    public LibraryItem punyalSO;
    public LibraryItem sundangSO;
    public LibraryItem kampilanSO;
    public LibraryItem arnisSO;

    public LibraryItem lapuSO;
    public LibraryItem kugsanoSO;
    public LibraryItem rajahSO;
    public LibraryItem magellanSO;
    public LibraryItem andresSO;
    public LibraryItem catalinaSO;
    public LibraryItem ladislaoSO;
    public LibraryItem teodroSO;
    public LibraryItem deodatoSO;

    public LibraryItem kuboSO;
    public LibraryItem mactanSO;
    public LibraryItem kkkSO;
    public LibraryItem cedulaSO;
    public LibraryItem manilaSO;

    public bool Palabok;
    public bool Pancit;
    public bool Sinigang;
    public bool Adobo;
    public bool Lechon;
    public bool Tinola;
    /*public bool KareKare;*/
    public bool Lugaw;
    public bool Sopas;
    public bool Champorado;
/*    public bool HaloHalo;
    public bool Balut;
    public bool Puto;*/
    public bool Bibingka;
/*    public bool Taho;
    public bool Sisig;
    public bool Laing;
    public bool Longganisa;*/

    public bool Balisong;
    public bool Kris;
    public bool Bolo;
    public bool Barong;
    public bool Punyal;
    public bool Sundang;
    public bool Kampilan;
    public bool Arnis;

    public bool lapuChapter1Finished;
    public bool lapuChapter2Finished;
    public bool lapuChapter3Finished;
    public bool lapuChapter4Finished;

    public bool andresChapter1Finished;
    public bool andresChapter2Finished;
    public bool andresChapter3Finished;
    public bool andresChapter4Finished;
    public bool andresChapter5Finished;

    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (Palabok)
        {
            palabokBtn.interactable = true;
            palabokBtn.transform.Find("Lock").gameObject.SetActive(false);
            palabokBtn.onClick.AddListener(palabokButton);
        }
        if (Pancit)
        {
            sinigangBtn.interactable = true;
            sinigangBtn.transform.Find("Lock").gameObject.SetActive(false);
            sinigangBtn.onClick.AddListener(sinigangButton);
        }
        if (Sinigang)
        {
            lechonBtn.interactable = true;
            lechonBtn.transform.Find("Lock").gameObject.SetActive(false);
            lechonBtn.onClick.AddListener(lechonButton);
        }
        if (Adobo)
        {
            adoboBtn.interactable = true;
            adoboBtn.transform.Find("Lock").gameObject.SetActive(false);
            adoboBtn.onClick.AddListener(adoboButton);
        }
        if (Tinola)
        {
            tinolaBtn.interactable = true;
            tinolaBtn.transform.Find("Lock").gameObject.SetActive(false);
            tinolaBtn.onClick.AddListener(tinolaButton);
        }
        /*if (KareKare)
        {
            kareKareBtn.interactable = true;
            kareKareBtn.transform.Find("Lock").gameObject.SetActive(false);
            kareKareBtn.onClick.AddListener(kareKareButton);
        }*/
        if (Lugaw)
        {
            lugawBtn.interactable = true;
            lugawBtn.transform.Find("Lock").gameObject.SetActive(false);
            lugawBtn.onClick.AddListener(lugawButton);
        }
        if (Sopas)
        {
            sopasBtn.interactable = true;
            sopasBtn.transform.Find("Lock").gameObject.SetActive(false);
            sopasBtn.onClick.AddListener(sopasButton);
        }
        if (Champorado)
        {
            champoradoBtn.interactable = true;
            champoradoBtn.transform.Find("Lock").gameObject.SetActive(false);
            champoradoBtn.onClick.AddListener(champoradoButton);
        }
/*        if (HaloHalo)
        {
            haloHaloBtn.interactable = true;
            haloHaloBtn.transform.Find("Lock").gameObject.SetActive(false);
            haloHaloBtn.onClick.AddListener(haloHaloButton);
        }
        if (Balut)
        {
            balutBtn.interactable = true;
            balutBtn.transform.Find("Lock").gameObject.SetActive(false);
            balutBtn.onClick.AddListener(balutButton);
        }
        if (Puto)
        {
            putoBtn.interactable = true;
            putoBtn.transform.Find("Lock").gameObject.SetActive(false);
            putoBtn.onClick.AddListener(putoButton);
        }*/
        if (Bibingka)
        {
            bibingkaBtn.interactable = true;
            bibingkaBtn.transform.Find("Lock").gameObject.SetActive(false);
            bibingkaBtn.onClick.AddListener(bibingkaButton);
        }
        /*if (Taho)
        {
            tahoBtn.interactable = true;
            tahoBtn.transform.Find("Lock").gameObject.SetActive(false);
            tahoBtn.onClick.AddListener(tahoButton);
        }
        if (Sisig)
        {
            sisigBtn.interactable = true;
            sisigBtn.transform.Find("Lock").gameObject.SetActive(false);
            sisigBtn.onClick.AddListener(sisigButton);
        }
        if (Laing)
        {
            laingBtn.interactable = true;
            laingBtn.transform.Find("Lock").gameObject.SetActive(false);
            laingBtn.onClick.AddListener(laingButton);
        }
        if (Longganisa)
        {
            longganisaBtn.interactable = true;
            longganisaBtn.transform.Find("Lock").gameObject.SetActive(false);
            longganisaBtn.onClick.AddListener(longganisaButton);
        }*/

        // Initialize the button interactability for weapon items
        if (Balisong)
        {
            balisongBtn.interactable = true;
            balisongBtn.transform.Find("Lock").gameObject.SetActive(false);
            balisongBtn.onClick.AddListener(balisongButton);
        }
        if (Kris)
        {
            krisBtn.interactable = true;
            krisBtn.transform.Find("Lock").gameObject.SetActive(false);
            krisBtn.onClick.AddListener(krisButton);
        }
        if (Bolo)
        {
            boloBtn.interactable = true;
            boloBtn.transform.Find("Lock").gameObject.SetActive(false);
            boloBtn.onClick.AddListener(boloButton);
        }
        if (Barong)
        {
            barongBtn.interactable = true;
            barongBtn.transform.Find("Lock").gameObject.SetActive(false);
            barongBtn.onClick.AddListener(barongButton);
        }
        if (Punyal)
        {
            punyalBtn.interactable = true;
            punyalBtn.transform.Find("Lock").gameObject.SetActive(false);
            punyalBtn.onClick.AddListener(punyalButton);
        }
        if (Sundang)
        {
            sundangBtn.interactable = true;
            sundangBtn.transform.Find("Lock").gameObject.SetActive(false);
            sundangBtn.onClick.AddListener(sundangButton);
        }
        if (Kampilan)
        {
            kampilanBtn.interactable = true;
            kampilanBtn.transform.Find("Lock").gameObject.SetActive(false);
            kampilanBtn.onClick.AddListener(kampilanButton);
        }
        if (Arnis)
        {
            arnisBtn.interactable = true;
            arnisBtn.transform.Find("Lock").gameObject.SetActive(false);
            arnisBtn.onClick.AddListener(arnisButton);
        }

        if (lapuChapter1Finished)
        {
            lapuBtn.interactable = true;
            lapuBtn.transform.Find("Lock").gameObject.SetActive(false);
            lapuBtn.onClick.AddListener(lapuButton);

            kugsanoBtn.interactable = true;
            kugsanoBtn.transform.Find("Lock").gameObject.SetActive(false);
            kugsanoBtn.onClick.AddListener(kugsanoButton);

            mactanBtn.interactable = true;
            mactanBtn.transform.Find("Lock").gameObject.SetActive(false);
            mactanBtn.onClick.AddListener(mactanButton);
        }
        if (lapuChapter3Finished)
        {
            rajahBtn.interactable = true;
            rajahBtn.transform.Find("Lock").gameObject.SetActive(false);
            rajahBtn.onClick.AddListener(rajahButton);

            magellanBtn.interactable = true;
            magellanBtn.transform.Find("Lock").gameObject.SetActive(false);
            magellanBtn.onClick.AddListener(magellanButton);

            kuboBtn.interactable = true;
            kuboBtn.transform.Find("Lock").gameObject.SetActive(false);
            kuboBtn.onClick.AddListener(kuboButton);
        }
        if (andresChapter1Finished)
        {
            andresBtn.interactable = true;
            andresBtn.transform.Find("Lock").gameObject.SetActive(false);
            andresBtn.onClick.AddListener(andresButton);

            catalinaBtn.interactable = true;
            catalinaBtn.transform.Find("Lock").gameObject.SetActive(false);
            catalinaBtn.onClick.AddListener(catalinaButton);

            manilaBtn.interactable = true;
            manilaBtn.transform.Find("Lock").gameObject.SetActive(false);
            manilaBtn.onClick.AddListener(manilaButton);
        }
        if (andresChapter2Finished)
        {
            ladislaoBtn.interactable = true;
            ladislaoBtn.transform.Find("Lock").gameObject.SetActive(false);
            ladislaoBtn.onClick.AddListener(ladislaoButton);

            deodatoBtn.interactable = true;
            deodatoBtn.transform.Find("Lock").gameObject.SetActive(false);
            deodatoBtn.onClick.AddListener(deodatoButton);

            teodroBtn.interactable = true;
            teodroBtn.transform.Find("Lock").gameObject.SetActive(false);
            teodroBtn.onClick.AddListener(teodroButton);

            kkkBtn.interactable = true;
            kkkBtn.transform.Find("Lock").gameObject.SetActive(false);
            kkkBtn.onClick.AddListener(kkkButton);

            cedulaBtn.interactable = true;
            cedulaBtn.transform.Find("Lock").gameObject.SetActive(false);
            cedulaBtn.onClick.AddListener(cedulaButton);
        }
    }

    public void LoadData(GameData data)
    {
        Palabok = data.Palabok;
        Pancit = data.Pancit;
        Sinigang = data.Sinigang;
        Adobo = data.Adobo;
        Lechon = data.Lechon;
        Tinola = data.Tinola;
/*        KareKare = data.KareKare;*/
        Lugaw = data.Lugaw;
        Sopas = data.Sopas;
        Champorado = data.Champorado;
/*        HaloHalo = data.HaloHalo;
        Balut = data.Balut;
        Puto = data.Puto;*/
        Bibingka = data.Bibingka;
/*        Taho = data.Taho;
        Sisig = data.Sisig;
        Laing = data.Laing;
        Longganisa = data.Longganisa;*/

        Balisong = data.Balisong;
        Kris = data.Kris;
        Bolo = data.Bolo;
        Barong = data.Barong;
        Punyal = data.Punyal;
        Sundang = data.Sundang;
        Kampilan = data.Kampilan;
        Arnis = data.Arnis;

        lapuChapter1Finished = data.lapuChapter1Finished;
        lapuChapter2Finished = data.lapuChapter2Finished;
        lapuChapter3Finished = data.lapuChapter3Finished;
        lapuChapter4Finished = data.lapuChapter4Finished;

        andresChapter1Finished = data.andresChapter1Finished;
        andresChapter2Finished = data.andresChapter2Finished;
        andresChapter3Finished = data.andresChapter3Finished;
        andresChapter4Finished = data.andresChapter4Finished;
        andresChapter5Finished = data.andresChapter5Finished;
    }

    public void SaveData(GameData data)
    {
        //no need to save anything here all we do here is retrieve data
    }

    public void ShowLibraryItem(LibraryItem item)
    {
        if (item == null)
        {
            Debug.LogWarning("Library Item not assigned.");
            return;
        }

        libraryUI.SetActive(true);
        itemImage.sprite = item.itemImage;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
    }
    public void palabokButton()
    {
        ShowLibraryItem(palabokSO);
        audioManager.ButtonAccept();
    }
    public void sinigangButton()
    {
        ShowLibraryItem(sinigangSO);
        audioManager.ButtonAccept();
    }
    public void lechonButton()
    {
        ShowLibraryItem(lechonSO);
        audioManager.ButtonAccept();
    }
    public void adoboButton()
    {
        ShowLibraryItem(adoboSO);
        audioManager.ButtonAccept();
    }

    public void tinolaButton()
    {
        ShowLibraryItem(tinolaSO);
        audioManager.ButtonAccept();
    }

/*    public void kareKareButton()
    {
        ShowLibraryItem(kareKareSO);
    }*/

    public void lugawButton()
    {
        ShowLibraryItem(lugawSO);
        audioManager.ButtonAccept();
    }

    public void sopasButton()
    {
        ShowLibraryItem(sopasSO);
        audioManager.ButtonAccept();
    }

    public void champoradoButton()
    {
        ShowLibraryItem(champoradoSO);
        audioManager.ButtonAccept();
    }
/*
    public void haloHaloButton()
    {
        ShowLibraryItem(haloHaloSO);
    }

    public void balutButton()
    {
        ShowLibraryItem(balutSO);
    }

    public void putoButton()
    {
        ShowLibraryItem(putoSO);
    }*/

    public void bibingkaButton()
    {
        ShowLibraryItem(bibingkaSO);
        audioManager.ButtonAccept();
    }

/*    public void tahoButton()
    {
        ShowLibraryItem(tahoSO);
    }

    public void sisigButton()
    {
        ShowLibraryItem(sisigSO);
    }

    public void laingButton()
    {
        ShowLibraryItem(laingSO);
    }

    public void longganisaButton()
    {
        ShowLibraryItem(longganisaSO);
    }*/

    public void balisongButton()
    {
        ShowLibraryItem(balisongSO);
        audioManager.ButtonAccept();
    }

    public void krisButton()
    {
        ShowLibraryItem(krisSO);
        audioManager.ButtonAccept();
    }

    public void boloButton()
    {
        ShowLibraryItem(boloSO);
        audioManager.ButtonAccept();
    }

    public void barongButton()
    {
        ShowLibraryItem(barongSO);
        audioManager.ButtonAccept();
    }

    public void punyalButton()
    {
        ShowLibraryItem(punyalSO);
        audioManager.ButtonAccept();
    }

    public void sundangButton()
    {
        ShowLibraryItem(sundangSO);
        audioManager.ButtonAccept();
    }

    public void kampilanButton()
    {
        ShowLibraryItem(kampilanSO);
        audioManager.ButtonAccept();
    }

    public void arnisButton()
    {
        ShowLibraryItem(arnisSO);
        audioManager.ButtonAccept();
    }

    public void lapuButton()
    {
        ShowLibraryItem(lapuSO);
        audioManager.ButtonAccept();
    }
    public void kugsanoButton()
    {
        ShowLibraryItem(kugsanoSO);
        audioManager.ButtonAccept();
    }
    public void rajahButton()
    {
        ShowLibraryItem(rajahSO);
        audioManager.ButtonAccept();
    }
    public void magellanButton()
    {
        ShowLibraryItem(magellanSO);
        audioManager.ButtonAccept();
    }
    public void andresButton()
    {
        ShowLibraryItem(andresSO);
        audioManager.ButtonAccept();
    }
    public void catalinaButton()
    {
        ShowLibraryItem(catalinaSO);
        audioManager.ButtonAccept();
    }
    public void ladislaoButton()
    {
        ShowLibraryItem(ladislaoSO);
        audioManager.ButtonAccept();
    }
    public void teodroButton()
    {
        ShowLibraryItem(teodroSO);
        audioManager.ButtonAccept();
    }
    public void deodatoButton()
    {
        ShowLibraryItem(deodatoSO);
        audioManager.ButtonAccept();
    }
    public void kuboButton()
    {
        ShowLibraryItem(kuboSO);
        audioManager.ButtonAccept();
    }
    public void mactanButton()
    {
        ShowLibraryItem(mactanSO);
        audioManager.ButtonAccept();
    }
    public void kkkButton()
    {
        ShowLibraryItem(kkkSO);
        audioManager.ButtonAccept();
    }
    public void cedulaButton()
    {
        ShowLibraryItem(cedulaSO);
        audioManager.ButtonAccept();
    }
    public void manilaButton()
    {
        ShowLibraryItem(manilaSO);
        audioManager.ButtonAccept();
    }
}