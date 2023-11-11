using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuckyDraw : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI prizedisplaytext;
    [SerializeField] private TextMeshProUGUI currentmodetext;  
    [SerializeField] private TextMeshProUGUI timetext;
    [SerializeField] private TextMeshProUGUI shotcounttext;
    [SerializeField] private TextMeshProUGUI warningtext;

    [SerializeField] private int Tier1Counter, Tier2Counter, Tier3Counter,Tier4Counter;    
    [SerializeField] private float TimeLimit;
    [SerializeField] List<string> Tier1Prize = new List<string>();
    [SerializeField] List<string> Tier2Prize = new List<string>();
    [SerializeField] List<string> Tier3Prize = new List<string>();
    [SerializeField] List<string> Tier4Prize = new List<string>();
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Targets;
    public bool PlayerIsInZone = false;
    private Collider playercol;

    private string PrizeTierPrefix = "";
    private string Prizename = "";
    private string warning = "";
    private bool GameModeOn = false;
    private int shotcount = 0;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        prizedisplaytext.text = "";
        playercol = Player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        shotcounttext.text = shotcount.ToString();
        warningtext.text = warning;
        float minutes = Mathf.FloorToInt(timer/ 60);
        float seconds = Mathf.FloorToInt(timer % 60);      
        timetext.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (GameModeOn == true)
        {
            Targets.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F1))
            {
                timer = TimeLimit;
                warning = "";
                GameModeOn = false;
                PrizeSummary();
            }
            currentmodetext.text = "Game Mode";
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameModeOn = false;
                warning = "Time Out";
                PrizeSummary();
            }
            if(PlayerIsInZone == true)
            {
                warning = "";
            }
            else
            {
                warning = "Stand in Pink Area to earn points";
            }
        }
        else
        {
            Targets.SetActive(false);
            currentmodetext.text = "Practice Mode";

            if (Input.GetKeyDown(KeyCode.F1) && PlayerIsInZone == true)
            {
                timer = TimeLimit;
                GameModeOn = true;
                prizedisplaytext.text = "";
                warning = "";
                shotcount = 0;
            }
            else if (Input.GetKeyDown(KeyCode.F1) && PlayerIsInZone == false)
            {
                warning = "Stand in Pink Area before starting";
            }
        }

        
    }

    public void PrizeSummary()
    {
        if(shotcount >= Tier4Counter)
        {
            PrizeTierPrefix = "[Tier4]";
            Prizename = Tier4Prize[Random.Range(0, Tier4Prize.Count)];
        }
        else if(shotcount >= Tier3Counter && shotcount < Tier4Counter)
        {
            PrizeTierPrefix = "[Tier3]";
            Prizename = Tier3Prize[Random.Range(0, Tier3Prize.Count)];
        }
        else if(shotcount >= Tier2Counter && shotcount < Tier3Counter)
        {
            PrizeTierPrefix = "[Tier2]";
            Prizename = Tier2Prize[Random.Range(0, Tier2Prize.Count)];
        }
        else if(shotcount >= Tier1Counter && shotcount < Tier2Counter)
        {
            PrizeTierPrefix = "[Tier1]";
            Prizename = Tier1Prize[Random.Range(0, Tier1Prize.Count)];
        }
        prizedisplaytext.text = string.Format("Prize Received: "+PrizeTierPrefix+Prizename);
        
        shotcount = 0;
    }

    public void AddPoint(int point)
    {
        if(GameModeOn==true&&PlayerIsInZone==true)
        {
            shotcount += point;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
