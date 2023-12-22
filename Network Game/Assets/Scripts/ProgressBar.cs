using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
    }

    public IEnumerator SetValue(int value, float slideDuration)
    {
        float timeElapsed = 0;
        float startValue = slider.value;

        while (timeElapsed < slideDuration)
        {
            slider.value = Mathf.Lerp(startValue, value, timeElapsed / slideDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        slider.value = value;
    }
}
