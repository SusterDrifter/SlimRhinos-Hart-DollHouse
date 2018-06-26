using UnityEngine;

/**
 * Call base.UpdateCirBarUI() in the Update() function of 
 * the subclass.
 */
public class CircularBarUIController : MonoBehaviour {

    [SerializeField] private RectTransform button;

    [SerializeField] public float sliderValue = 0f;
    [SerializeField] public float minCorrectVal = 33f;
    [SerializeField] public float maxCorrectVal = 66f;
    [SerializeField] public float duration = 15f;

    [SerializeField] private bool isIncreasing = true;
    [SerializeField] private float minVal = 0f;
    [SerializeField] private float maxVal = 100f;

    public void UpdateCircBarUI()
    {
        ChangeValue();
        MoveSlider();
    }
    /**
     * Manages the value of the circular bar.
     */
    private void ChangeValue()
    {
        // If it is at either end of the value, swap the direction
        if ((isIncreasing && sliderValue >= (maxVal - 1))
            || (!isIncreasing && sliderValue <= (minVal + 1))) {
            isIncreasing = !isIncreasing;
        }

        // Increasing the value
        if (isIncreasing && sliderValue < maxVal) {
            float fillToGo = maxVal - sliderValue;
            float incrBy = fillToGo / duration;
            sliderValue += incrBy;
            return;
        } 

        // Decreasing the value
        if (!isIncreasing && sliderValue > minVal) {
            float fillToGo = sliderValue - minVal;
            float decBy = fillToGo / duration;
            sliderValue -= decBy;
            return;
        }
    } 

    /**
     * Moves the handle position through rotation
     */ 
    private void MoveSlider() {
        sliderValue = Mathf.Clamp(sliderValue, minVal, maxVal);
        float curValue = (sliderValue / 100f) * 180f / 360f;
        float buttonAngle = curValue * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }

    public void SetButton(RectTransform button) {
        this.button = button;
    }
}
