using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private int randomLevel;
    public float rotationSpeed;
    private float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To rotate
        rotationAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(Vector3.forward * rotationAngle);

        // To use portal
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 1.25f);
        if (collider != null)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<CameraZoom>().startPortalZoom = true;
                FindObjectOfType<CameraMovement>().startPortalFollow(this.transform.position);
                collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                
            }
        }
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (cam != null) 
        {
            if (cam.GetComponent<Camera>().orthographicSize <= 1.25)
                LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        PlayerData.currentLevel += 1;
        if (PlayerData.startingLevels.Count != 0)
        {
            randomLevel = PlayerData.startingLevels[Random.Range(0, PlayerData.startingLevels.Count)];
            PlayerData.startingLevels.Remove(randomLevel);
            SceneManager.LoadScene(randomLevel);
        }
        else
        {
            randomLevel = PlayerData.levels[Random.Range(0, PlayerData.levels.Count)];
            PlayerData.levels.Remove(randomLevel);
            SceneManager.LoadScene(randomLevel);
        }
    }

}
