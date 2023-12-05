using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBeam : MonoBehaviour
{
    public GameObject target;
    public GameObject beam;

    private bool isBeam = false;
    private bool offBeam = false;

    public MiddleOctMonster mOctMon;

    private void Awake()
    {
        mOctMon.startBeamAnimation += StartBeamAnimation;
    }

    private void Start()
    {
        
        // pos 위치 초기화 하기
        
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeam)
        {
            beam.transform.Translate(0, -0.5f, 0);
            if(beam.transform.localPosition.y <= 3.72f)
            {
                isBeam = false;
            }
        }

        if (offBeam)
        {
            beam.transform.Translate(0, 0.2f, 0);
            if (beam.transform.localPosition.y >= 16f)
            {
                offBeam = false;
            }
        }
    }

    private void StartBeamAnimation()
    {
        StartCoroutine(BeamActive());
    }

    IEnumerator BeamActive()
    {
        for (int i = 0; i < 3; i++)
        {
            target.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            target.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        beam.SetActive(true);
        isBeam = true;
        beam.transform.localPosition = new Vector3(0, 16, 0);

        yield return new WaitForSeconds(3.0f);
        offBeam = true;

        yield return new WaitForSeconds(3.0f);

        beam.transform.localPosition = new Vector3(0, 16, 0);


        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
