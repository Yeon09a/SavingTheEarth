using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBubble : MonoBehaviour
{
    public Vector3 moveDir;

    private float speed = 1.5f;

    private bool isMove = false;

    private void OnEnable()
    {
        MiddleOctMonster.startBubbleMove += startBubbleMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.Translate(moveDir * speed * Time.deltaTime);
        }
    }

    IEnumerator BubbleMove() {

        isMove = true;

        yield return new WaitForSeconds(5.0f);

        Destroy(this.gameObject);
    }

    private void startBubbleMove()
    {
        StartCoroutine(BubbleMove());
    }

    private void OnDisable()
    {
        MiddleOctMonster.startBubbleMove -= startBubbleMove;
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
