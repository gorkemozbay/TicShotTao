using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{

    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    
    public float capturePercentage = 0.0f;
    public float captureSpeed;
    public float enemeyCaptureSpeed;
    
    public bool isCaptured = false;
    public bool isCapturedByPlayer = false;
    
    public Slider slider;

    private GameObject captureZone;
    private GameObject captureSliderColor;

    private void Start()
    {
        enemeyCaptureSpeed = 5.0f;
        slider.value = 0f;
        slider.gameObject.SetActive(false);

        captureZone = gameObject.transform.GetChild(1).gameObject;
        captureSliderColor = slider.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (GameObject.FindObjectOfType<Manager>().didWin)
            captureSpeed = 0;
        else
            captureSpeed = PlayerData.playerCaptureSpeed;
        // Player Recaptures
        if (capturePercentage <= -100 && CapturingBy() == "Player")
        {
            slider.gameObject.SetActive(true);
            captureZone.SetActive(true);
            capturePercentage += captureSpeed * Time.deltaTime;
        }
        // Enemy Recaptures
        else if (capturePercentage >= 100 && CapturingBy() == "Enemy")
        {
            slider.gameObject.SetActive(true);
            captureZone.SetActive(true);
            capturePercentage -= enemeyCaptureSpeed * Time.deltaTime;
        }
        // Player Captured (Area is 100)
        else if (capturePercentage >= 100)
        {
            isCaptured = true;
            isCapturedByPlayer = true;
            slider.gameObject.SetActive(false);
            captureZone.SetActive(true);
        }
        // Enemy Captured  (Area is 100)
        else if (capturePercentage <= -100)
        {
            isCaptured = true;
            isCapturedByPlayer = false;
            slider.gameObject.SetActive(false);
            captureZone.SetActive(true);
        }
        // Player+Enemy in the Area
        else if (CapturingBy() == "Both")
        {
            slider.gameObject.SetActive(true);
        }
        // Player Winning Area
        else if (CapturingBy() == "Player")
        {
            slider.gameObject.SetActive(true);
            capturePercentage += captureSpeed * Time.deltaTime;
        }
        // Enemy Winning Area
        else if (CapturingBy() == "Enemy")
        {
            slider.gameObject.SetActive(true);
            capturePercentage -= enemeyCaptureSpeed * Time.deltaTime;
        }
        // Area is 0
        if (Mathf.Abs(capturePercentage) <= 0.3f)
        {
            isCaptured = false;
            slider.gameObject.SetActive(false);
        }
        
        // Update SLIDER + Zone
        slider.value = Mathf.Abs(capturePercentage);
        // Player is ahead
        if(capturePercentage >= 0)
        {
            captureSliderColor.GetComponent<Image>().color = new Color32(2, 170, 244, 150);
            captureSliderColor.GetComponent<Image>().fillClockwise = true;
        }
        // Enemy is ahead
        else
        {
            captureSliderColor.GetComponent<Image>().color = new Color32(200, 0, 0, 100);
            captureSliderColor.GetComponent<Image>().fillClockwise = false;
            
        }

        // player captured area
        if (isCaptured && isCapturedByPlayer)
        {
            captureZone.GetComponent<SpriteRenderer>().color = new Color32(2, 170, 244, 100);
        }
        // enemy captured area
        else if (isCaptured && !isCapturedByPlayer)
        {
            captureZone.GetComponent<SpriteRenderer>().color = new Color32(238, 21, 17, 100);
        }
        // uncaptured
        else
        {
            captureZone.SetActive(false);
        }
            
    }

    private string CapturingBy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 7.0f);
        List<string> tags = new List<string>();
        
        // Find all tags
        foreach(Collider2D collider in colliders)
        {
            if (!tags.Contains(collider.tag)){
                tags.Add(collider.tag);
            }
        }

        if (tags.Contains("Player") && tags.Contains("EnemyCapturer"))
        {
            return "Both";
        }
        else if (tags.Contains("Player"))
        {
            return "Player";
        }
        else if (tags.Contains("EnemyCapturer"))
        {
            return "Enemy";
        }
        return "Noone";
    }

    public int whoCaptured()
    {
        if (isCaptured  && isCapturedByPlayer)
        {
            return 1;
        }
        else if (isCaptured && !isCapturedByPlayer)
        {
            return -1;
        }
        return 0;
    }
}
