using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathTimeCount : MonoBehaviour
{
    public int Timer = 15;
    public Text CountDownText;
    public Slider showTimer;
    // Start is called before the first frame update
    void Start()
    {
        showTimer.value = Timer;
        CountDownText.text = string.Format("{0}s",15-Timer);
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while(Timer > 0)
        {
            yield return new WaitForSeconds(1);
            Timer--;
            showTimer.value = Timer;
            CountDownText.text = string.Format("{0}s",15-Timer);
        }
    }
}
