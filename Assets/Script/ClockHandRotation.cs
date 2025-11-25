using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClockHandController : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    public Transform hourHand;
    public Transform minuteHand;

    public float minuteSpeed = 200f;
    public bool isClockwise;

    private bool isHolding = false;
    private float totalMinuteAngle = 0f;

    private Vector3 hrRotaion, minRotation;

    public GameManager gameManager;

    private void Awake()
    {
        //hrRotaion = hourHand.rotation
    }
    void Update()
    {
        if (isHolding)
        {
            float direction = isClockwise ? -1f : 1f;
            float minuteAngle = direction * minuteSpeed * Time.deltaTime;

            
            minuteHand.Rotate(0, 0, minuteAngle);

            
            totalMinuteAngle += minuteAngle;

            
            float hourAngle = minuteAngle / 12f;
            hourHand.Rotate(0, 0, hourAngle);
        }
    }

    public void LeftPointerDown(BaseEventData data)
    {
        AudioController.Instance.PlayButtonClickSound();
        isClockwise = false;
        isHolding = true;
    }

    public void LeftPointerUp(BaseEventData data)
    {
        isHolding = false;
    }

    public void RightPointerDown(BaseEventData data)
    {
        AudioController.Instance.PlayButtonClickSound();
        isClockwise = true;
        isHolding = true;
    }

    public void RightPointerUp(BaseEventData data)
    {
        isHolding = false;
    }

    public void Home()
    {
        //Update the Clock 
        hourHand.rotation = Quaternion.Euler(0, 0, 0);
        minuteHand.rotation = Quaternion.Euler(0, 0, 0);
       
        StartCoroutine(gotoHome());
        
    }

    IEnumerator gotoHome()
    {
        yield return new WaitForSeconds(1f);
        gameManager.Home();
    }

}
