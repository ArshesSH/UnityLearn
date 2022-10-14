using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingManager : MonoBehaviour
{
    // Start is called before the first frame update
    enum RaceState
    {
        Wait,
        Ready,
        RaceStart,
        RaceEnd
    }

    [SerializeField]
    private GameObject startBlock;
    [SerializeField]
    private GameObject[] aiCars;
    [SerializeField]
    private GameObject playerCar;
    float startBlockMoveSpeed = 10.0f;
    bool isStartBlockMove = false;

    [SerializeField]
    private int maxRapCount = 3;

    CarControllerAI[] aiCarControllers = new CarControllerAI[4];
    CarController playerCarController;

    int curRank = 1;
    int[] rank = new int[5];
    float[] totalRapTime = new float[5];

    RaceState raceState = RaceState.Wait;
    float startTimer = 0.0f;
    string informStr;
    void Start()
    {
        for (int i = 0; i < aiCars.Length; ++i)
        {
            aiCarControllers[i] = aiCars[i].GetComponent<CarControllerAI>();
        }
        playerCarController = playerCar.GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (raceState)
        {
            case RaceState.Wait:
            {

            }
            break;
            case RaceState.Ready:
            {
                startTimer += Time.deltaTime;
                if(isStartBlockMove)
                {
                    MoveBlock();
                }
                if (startTimer < 3.0f)
                {
                    informStr = "3초 뒤 경기가 시작됩니다.";
                }
                else if ( startTimer < 4.0f)
                {
                    informStr = "3";
                }
                else if (startTimer < 5.0f)
                {
                    informStr = "2";
                }
                else if (startTimer < 6.0f)
                {
                    isStartBlockMove = true;
                    informStr = "1";
                }
                else
                {
                    isStartBlockMove = false;
                    startTimer = 0.0f;

                    for (int i = 0; i < aiCarControllers.Length; ++i)
                    {
                        aiCarControllers[i].StartCar();
                    }
                    playerCarController.StartCar();

                    raceState = RaceState.RaceStart;
                }
            }
            break;
            case RaceState.RaceStart:
            {
                if (playerCarController.RapCount == maxRapCount)
                {
                    totalRapTime[4] = playerCarController.GetTotalRapTime();
                    rank[4] = curRank;
                    raceState = RaceState.RaceEnd;
                }

                for (int i = 0; i < aiCarControllers.Length; ++i)
                {
                    if (aiCarControllers[i].RapCount == maxRapCount)
                    {
                        if(rank[i] == 0)
                        {
                            totalRapTime[i] = aiCarControllers[i].GetTotalRapTime();
                            rank[i] = curRank;
                            curRank++;
                            print("RapCounted");
                        }
                    }
                }
            }
            break;
            case RaceState.RaceEnd:
            {
                playerCarController.StopCar();
                for (int i = 0; i < aiCarControllers.Length; ++i)
                {
                    aiCarControllers[i].StopCar();
                }
            }
            break;
        }
    }

    private void MoveBlock()
    {
        startBlock.transform.Translate(Vector3.up * startBlockMoveSpeed * Time.deltaTime);
    }

    private void OnGUI()
    {
        switch (raceState)
        {
            case RaceState.Wait:
            {
                if (GUI.Button(new Rect(600.0f, 30.0f, 150.0f, 30.0f), "준비 버튼"))
                {
                    raceState = RaceState.Ready;
                }
                if (GUI.Button(new Rect(800.0f, 30.0f, 150.0f, 30.0f), "Test"))
                {
                    playerCarController.StartCar();
                    raceState = RaceState.RaceStart;
                }
            }
            break;
            case RaceState.Ready:
            {
                GUI.Box(new Rect(750.0f, 30.0f, 300.0f, 30.0f), informStr);
            }
            break;
            case RaceState.RaceStart:
            {
  

            }
            break;
            case RaceState.RaceEnd:
            {
                GUI.Box(new Rect(750.0f, 30.0f, 300.0f, 30.0f), rank[4].ToString() + "등 입니다.");
                if (GUI.Button(new Rect(500.0f, 30.0f, 150.0f, 30.0f), "게임 리셋"))
                {
                    GameManager.Instance.ChangeScene("01-0 Start");
                }
            }
            break;
        }
    }

}
