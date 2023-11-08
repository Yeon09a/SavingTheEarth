using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject startPanel; // 게임 시작 버튼 모음 판넬
    public Button newGameBtn; // 새 게임 버튼
    public Button loadGameBtn; // 이어하기 버튼
    public Button exitGameBtn; // 나가기 버튼
    public GameObject newGamePanel; // 새 게임 판넬
    public GameObject saveFilePanel; // 세이브 파일 판넬
    public GameObject dialogBox; // 대화상자
    public GameObject selectPanel; // 선택지 판넬
    public GameObject bg; // 대화창 배경

    // 새 게임 판넬 관련
    private string playerName; // 플레이어 이름
    public TMP_InputField playerNameInput; // 플레이어 이름 
    public Button okayBtn; // 확인 버튼
    public Button newBackBtn; // 새 게임 판넬 뒤로가기

    // 이어하기 판넬 관련
    public Button loadBackBtn; // 세이브 판넬 뒤로가기

    // 세이브 파일은 따로 스크립트 만들기

    public delegate void SelectDel();


    private void Update()
    {
        // 임시 이동
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneManager.LoadScene("BaseMap");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newGameBtn.onClick.AddListener(OpenNewGamePanel);
        loadGameBtn.onClick.AddListener(OpenSaveFilePanel);
        exitGameBtn.onClick.AddListener(GameExit);
        okayBtn.onClick.AddListener(ClickOkayBtn);
        newBackBtn.onClick.AddListener(ClickNewBackBtn);
        loadBackBtn.onClick.AddListener(ClickLoadBackBtn);
    }
    
    public void OpenNewGamePanel() // 새 게임 판넬 활성화
    {
        startPanel.SetActive(false); // 버튼 모음 비활성화

        newGamePanel.SetActive(true);
    }

    public void OpenSaveFilePanel() // 세이브 파일 판넬 활성화
    {
        startPanel.SetActive(false); // 버튼 모음 비활성화

        saveFilePanel.SetActive(true);
    }

    public void GameExit() // 게임 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

    #region 새 게임 관련 코드
    private void ClickOkayBtn() // 확인 버튼 클릭
    {
        playerName = playerNameInput.text;
        TitleDialog titleDialog = dialogBox.GetComponentInParent<TitleDialog>();
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrWhiteSpace(playerName))
        {
            bg.SetActive(true);
            dialogBox.SetActive(true);
            titleDialog.SetDialogName("");
            titleDialog.SetDialogContent(0, "이름을 입력해주십시오.");

        } else
        {
            bg.SetActive(true);
            dialogBox.SetActive(true);
            titleDialog.SetDialogName("");
            titleDialog.SetDialogContent(1, playerName + " 로/으로 하시겠습니까?");

            // 선택지 함수 titledialog에 만들기
            string[] selectText = { "예", "아니오" };
            titleDialog.MakeSelect(selectText, this);
        }
    }

    private void ClickNewBackBtn() // 뒤로가기 버튼 클릭
    {
        startPanel.SetActive(true); // 버튼 모음 활성화
        playerNameInput.text = "";
        newGamePanel.SetActive(false);
    }
    #endregion

    #region 불러오기 관련 버튼
    private void ClickLoadBackBtn() // 뒤로가기 버튼 클릭
    {
        startPanel.SetActive(true); // 버튼 모음 활성화

        saveFilePanel.SetActive(false);
    }
    #endregion

    public void SetSelectResult(string sTag, SelectDel deleteSelect) // 버튼 선택 함수
    {
        Debug.Log(sTag);
        
        if (sTag.Equals("Select0"))
        {
            PlayerData.instance.playerName = playerName;
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneManager.LoadScene("BaseMap");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        else
        {
            deleteSelect();
            bg.SetActive(false);
            dialogBox.SetActive(false);
        }
    }
}
