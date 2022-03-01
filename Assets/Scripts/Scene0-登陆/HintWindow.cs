using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintWindow : MonoBehaviour
{
    public Text showHint;
    // Start is called before the first frame update
    public void onButton()
    {
        transform.gameObject.SetActive(false);
    }
}
