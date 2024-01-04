using UnityEngine;
using TMPro;

public class FontColorChange : MonoBehaviour
{
    public TMP_Text uiText;
    private float count = 0f;
    private int period = 0;
    private float changeSpeed = 200f;

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        
        count += changeSpeed * Time.deltaTime;
        if(count >= 255f)
        {
            count = 0f;
            period++;
            period %= 4;
        }

        float r=0, g=0, b=0;
        if(period == 1) 
        {
            r = count;
        }
        else if (period == 2)
        {
            r = 255f;
            g = count;
            b = count;
        }
        else if (period == 3)
        {
            r = 255f - count;
            g = 255f - count;
            b = 255f - count;
        }

        uiText.color = new Color(r/255f, g/255f, b/255f);

        //Debug.Log(r);
        //Debug.Log(g);
        //Debug.Log(b);
    }
}

/*
float r = Random.Range(0f, 255f) / 255f;
float g = Random.Range(0f, 255f) / 255f;
float b = Random.Range(0f, 255f) / 255f;
uiText.color = new Color(r, g, b);*/
