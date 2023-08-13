using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    public GameObject target;
    public GameObject beam;

    private bool isBeam = false;
    
    

    private void OnEnable()
    {
        BossMonster.startBeamAnimation += StartBeamAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeam) // 임시 애니메이션
        {
            beam.transform.Translate(0, -1, 0);
            if(beam.transform.localPosition.y <= 5.0f)
            {
                isBeam = false;
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
        Destroy(this.gameObject);
    }

    private void BeamAnim() // 빔 애니메이션 관리 함수 // 후에 수정
    {
        // 임시 애니메이션
        
    }

    private void OnDisable()
    {
        BossMonster.startBeamAnimation -= StartBeamAnimation;
    }
}
