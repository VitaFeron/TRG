using UnityEngine;

public class Lever : MonoBehaviour
{
    public MovementPlatform platform; // —сылка на платформу
    public GameObject leverON;
    public GameObject leverOFF;
    private bool isLeverOnActive = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("King"))
        {
            if (isLeverOnActive == false)
            {
                if (leverOFF.activeInHierarchy)
                {
                    leverOFF.SetActive(false);
                    leverON.SetActive(true);
                    Flip();
                    isLeverOnActive = true;
                }
                else if (leverON.activeInHierarchy)
                { 
                    leverOFF.SetActive(true);
                    leverON.SetActive(false);
                    Flip();
                    isLeverOnActive = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("King"))
        {
            isLeverOnActive = false;
        }
    }

    void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}