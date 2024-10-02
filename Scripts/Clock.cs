using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI h1;
    public TextMeshProUGUI h2;
    public TextMeshProUGUI m1;
    public TextMeshProUGUI m2;
    public TextMeshProUGUI s1;
    public TextMeshProUGUI s2;
    public TextMeshProUGUI M1;
    public TextMeshProUGUI M2;
    public TextMeshProUGUI M3;
    public TextMeshProUGUI D1;
    public TextMeshProUGUI D2;
    public TextMeshProUGUI Y1;
    public TextMeshProUGUI Y2;
    public TextMeshProUGUI Y3;
    public TextMeshProUGUI Y4;

    private float delayTime = 1f;
    private long Timer = 0;
    private Stopwatch watch;
    private int sec1 = 0;
    private int sec2 = 0;
    private int min1 = 0;
    private int min2 = 0;
    private int hrs1 = 0;
    private int hrs2 = 0;
    private int dy1 = 2; //input
    private int dy2 = 9;
    private int mth = 9; //input
    private int yr1 = 1; //input
    private int yr2 = 9;
    private int yr3 = 4;
    private int yr4 = 1;

    private string second1;
    private string second2;
    private string minute1;
    private string minute2;
    private string hour1;
    private string hour2;
    private string day1;
    private string day2;
    private string month1;
    private string month2;
    private string month3;
    private string year1;
    private string year2;
    private string year3;
    private string year4;

    private string[,] monthDict = new string[12, 3] {{"J","A","N"},
                                                    {"F","E","B"},
                                                    {"M","A","R"},
                                                    {"A","P","R"},
                                                    {"M","A","Y"},
                                                    {"J","U","N"},
                                                    {"J","U","L"},
                                                    {"A","U","G"},
                                                    {"S","E","P"},
                                                    {"O","C","T"},
                                                    {"N","O","V"},
                                                    {"D","E","C"}};



    void Start()
    {
        watch = new Stopwatch();
        watch.Start();

        day1 = dy1.ToString();
        D1.text = day1;
        day2 = dy2.ToString();
        D2.text = day2;

        year1 = yr1.ToString();
        Y1.text = year1;
        year2 = yr2.ToString();
        Y2.text = year2;
        year3 = yr3.ToString();
        Y3.text = year3;
        year4 = yr4.ToString();
        Y4.text = year4;

        month1 = monthDict[mth, 0];
        M1.text = month1;
        month2 = monthDict[mth, 1];
        M2.text = month2;
        month3 = monthDict[mth, 2];
        M3.text = month3;

        
    }


    void Update()
    {
        
        Timer = watch.ElapsedMilliseconds;


        if (Timer >= delayTime && Time.timeScale != 0f)
        {
            watch.Restart();

            if (sec2 >= 10)
            {
                sec2 = 0;
                sec1 += 1;

                if (sec1 >= 6)
                {
                    sec1 = 0;
                    min2 += 1;

                    if (min2 >= 10)
                    {
                        min2 = 0;
                        min1 += 1;

                        if (min1 >= 6)
                        {
                            min1 = 0;
                            hrs2 += 1;

                            if (hrs2 >= 10)
                            {
                                hrs2 = 0;
                                hrs1 += 1;

                            }

                            if (hrs2 >= 4 && hrs1 >= 2)
                            {
                                hrs2 = 0;
                                hrs1 = 0;
                                dy2 += 1;

                                if (dy2 >= 10)
                                {
                                    dy2 = 0;
                                    dy1 += 1;

                                    if (dy1 >= 3) // fix to add 30 or 31
                                    {
                                        dy1 = 0;
                                        dy2 = 0;
                                        mth += 1;


                                        if (mth >= 12)
                                        {
                                            mth = 0;
                                            yr4 += 1;

                                            if (yr4 >= 10)
                                            {
                                                yr4 = 0;
                                                yr3 += 1;

                                                if (yr3 >= 10)
                                                {
                                                    yr3 = 0;
                                                    yr2 += 1;

                                                    if (yr2 >= 10)
                                                    {
                                                        yr2 = 0;
                                                        yr1 += 1;

                                                        if (yr1 >= 10)
                                                        {
                                                            yr1 = 0;


                                                        }
                                                        year1 = yr1.ToString();
                                                        Y1.text = year1;

                                                    }
                                                    year2 = yr2.ToString();
                                                    Y2.text = year2;

                                                }
                                                year3 = yr3.ToString();
                                                Y3.text = year3;

                                            }
                                            year4 = yr4.ToString();
                                            Y4.text = year4;

                                        }
                                        month1 = monthDict[mth, 0];
                                        M1.text = month1;
                                        month2 = monthDict[mth, 1];
                                        M2.text = month2;
                                        month3 = monthDict[mth, 2];
                                        M3.text = month3;

                                    }
                                    day1 = dy1.ToString();
                                    D1.text = day1;

                                }
                                day2 = dy2.ToString();
                                D2.text = day2;

                            }
                            hour1 = hrs1.ToString();
                            h1.text = hour1;

                            hour2 = hrs2.ToString();
                            h2.text = hour2;

                        }
                        minute1 = min1.ToString();
                        m1.text = minute1;

                    }
                    minute2 = min2.ToString();
                    m2.text = minute2;
                }
                second1 = sec1.ToString();
                s1.text = second1;

            }

            second2 = sec2.ToString();
            s2.text = second2;
            sec2 += 1;

            
        }

    }

}
