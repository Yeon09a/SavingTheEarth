using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{
    public GameObject baseMapFace;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.activateFace += ActivateFace;
        player.deactivateFace += DeactivateFace;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateFace()
    {
        baseMapFace.SetActive(true);
    }

    private void DeactivateFace()
    {
        baseMapFace.SetActive(false);
    }

    private void OnDisable()
    {
        player.activateFace -= ActivateFace;
        player.deactivateFace -= DeactivateFace;
    }
}
