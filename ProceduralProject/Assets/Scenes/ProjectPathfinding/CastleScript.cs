using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleScript : MonoBehaviour
{
    public float health = 25;
    public float availableAvatars = 1;
    public AvatarScript avatar;
    public Transform prefab;
    public float gameTimer;
    void Start()
    {
        transform.position = new Vector3(3, .5f, 3);
        gameTimer = 60;
        
    }

    // Update is called once per frame
    void Update()
    {
        avatar = FindObjectOfType<AvatarScript>();
        gameTimer -= Time.deltaTime;
        if(health<=0)
        {
            SceneManager.LoadScene("YouLose");
            Destroy(gameObject);
        }
        if(avatar == null) Instantiate(prefab, new Vector3(4, .5f, 3), Quaternion.identity);
        if(gameTimer<=0)
        {
            SceneManager.LoadScene("YouWin");

        }
        
    }
}
