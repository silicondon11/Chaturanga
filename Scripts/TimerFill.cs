using System.Diagnostics;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TimerFill : MonoBehaviour
{
    private Renderer rend;
    private float fillTime = 10.0f;

    private float timer = 0f;
    private Stopwatch stopwatch = new Stopwatch();

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        FillProgress();
    }

    private void Update()
    {
        if (timer < fillTime)
        {
            FillProgress();
        }
        else
        {
            rend.material.SetFloat("_FillAmount", 1f);

            Destroy(gameObject);
        }
        
    }

    private void FillProgress()
    {

        float fillAmount = timer / fillTime;
        rend.material.SetFloat("_FillAmount", fillAmount);
        timer += Time.deltaTime;

    }
}

