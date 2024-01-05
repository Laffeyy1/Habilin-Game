using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLibraryManager : MonoBehaviour, IDataPersistence
{
    public bool Palabok;
    public bool Pancit;
    public bool Sinigang;
    public bool Adobo;
    public bool Lechon;
    public bool Tinola;
    public bool KareKare;
    public bool Lugaw;
    public bool Sopas;
    public bool Champorado;
    public bool HaloHalo;
    public bool Balut;
    public bool Puto;
    public bool Bibingka;
    public bool Taho;
    public bool Sisig;
    public bool Laing;
    public bool Longganisa;

    public bool Balisong;
    public bool Kris;
    public bool Bolo;
    public bool Barong;
    public bool Punyal;
    public bool Sundang;
    public bool Kampilan;
    public bool Arnis;

    public void LoadData(GameData data)
    {
        Palabok = data.Palabok;
        Pancit = data.Pancit;
        Sinigang = data.Sinigang;
        Adobo = data.Adobo;
        Lechon = data.Lechon;
        Tinola = data.Tinola;
        KareKare = data.KareKare;
        Lugaw = data.Lugaw;
        Sopas = data.Sopas;
        Champorado = data.Champorado;
        HaloHalo = data.HaloHalo;
        Balut = data.Balut;
        Puto = data.Puto;
        Bibingka = data.Bibingka;
        Taho = data.Taho;
        Sisig = data.Sisig;
        Laing = data.Laing;
        Longganisa = data.Longganisa;

        Balisong = data.Balisong;
        Kris = data.Kris;
        Bolo = data.Bolo;
        Barong = data.Barong;
        Punyal = data.Punyal;
        Sundang = data.Sundang;
        Kampilan = data.Kampilan;
        Arnis = data.Arnis;
    }

    public void SaveData(GameData data)
    {
        data.Palabok = Palabok;
        data.Pancit = Pancit;
        data.Sinigang = Sinigang;
        data.Adobo = Adobo;
        data.Lechon = Lechon;
        data.Tinola = Tinola;
        data.KareKare = KareKare;
        data.Lugaw = Lugaw;
        data.Sopas = Sopas;
        data.Champorado = Champorado;
        data.HaloHalo = HaloHalo;
        data.Balut = Balut;
        data.Puto = Puto;
        data.Bibingka = Bibingka;
        data.Taho = Taho;
        data.Sisig = Sisig;
        data.Laing = Laing;
        data.Longganisa = Longganisa;

        data.Balisong = Balisong;
        data.Kris = Kris;
        data.Bolo = Bolo;
        data.Barong = Barong;
        data.Punyal = Punyal;
        data.Sundang = Sundang;
        data.Kampilan = Kampilan;
        data.Arnis = Arnis;
    }
}
