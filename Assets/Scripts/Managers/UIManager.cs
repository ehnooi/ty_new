using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Button StartB;
    [SerializeField] private Button RankingB;
    [SerializeField] private GameObject StartP;
    [SerializeField] private GameObject RankingP;

    private GameObject CurPanel = null;

    public void Awake()
    {
        Instance = this;
        StartP.SetActive(false);
        RankingP.SetActive(false);
    }

    public void OnStartBClick()
    {
        CurPanel = StartP; 
        CurPanel.SetActive(true);
        StartB.gameObject.SetActive(false);
        RankingB.gameObject.SetActive(false);
    }

    public void OnRankingBClick()
    {
        CurPanel = RankingP;
        CurPanel.SetActive(true);
        StartB.gameObject.SetActive(false);
        RankingB.gameObject.SetActive(false);
    }

    public void OnBackBClick()
    {
        if (CurPanel != null)
        {
            CurPanel.SetActive(false);
            CurPanel = null;
        }
        StartB.gameObject.SetActive(true);
        RankingB.gameObject.SetActive(true);
    }
}
