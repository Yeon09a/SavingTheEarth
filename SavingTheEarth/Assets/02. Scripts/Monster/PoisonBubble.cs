using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBubble : MonoBehaviour
{
    public float bubbleRot;
    private Vector3 moveDir;

    private float speed = 2f;

    private bool isMove = false;

    public MiddleOctMonster mOctMon;

    public bool isFirst;

    private void Awake()
    {
        mOctMon.startBubbleMove += startBubbleMove;
        moveDir = (Quaternion.Euler(0, 0, bubbleRot) * transform.position).normalized;
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

        if (!isFirst)
        {
            yield return new WaitForSeconds(2.0f);
        }
        
        isMove = true;

        yield return new WaitForSeconds(5.0f);

        transform.localPosition = new Vector3(-0.2599945f, 0.95f, 0);
        isMove = false;

        this.gameObject.SetActive(false);
    }

    private void startBubbleMove()
    {
        StartCoroutine(BubbleMove());
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
