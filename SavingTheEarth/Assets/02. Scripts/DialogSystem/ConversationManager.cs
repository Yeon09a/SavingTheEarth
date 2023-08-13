using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogData; // 대화 내용 저장
    Dictionary<int, Sprite> portraitData; // 초상화 목록 저장

    public Sprite[] portraitArr; // 초상화 Sprite 꺼내오기 위한 배열

    void Start()
    {
        dialogData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();
    }

    void GenerateData() // 대사 & 초상화 생성
    {
        // dialogData.Add(~~~(id), new string[] { "~~~:portraitIndex", "~~~:portraitIndex" }); // 후에 수정

        // portraitData.Add(~~~(id) + n, portraitArr[n]); // 후에 수정
    }

    public string GetDialog(int id, int dialogIndex) // 대사 리턴 (id = 대화상대 객체id , dialogIndex = 대사 인덱스)
    {
        if (dialogIndex == dialogData[id].Length) // 대화가 끝나면
            return null; // 종료
        else // 대사가 남아있으면
            return dialogData[id][dialogIndex]; // 대사 반환
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex]; // 초상화 이미지 반환
    }
}
