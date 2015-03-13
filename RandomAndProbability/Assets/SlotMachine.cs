using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour 
{
    public Text Reel1;
    public Text Reel2;
    public Text Reel3;
    public Text BetResult;
    public Text InputBet;
    public Text YouWin;
    public Text Money;
    private int currentResult;
    private int currentMoney;
    private int maxBet = 25;
    private int inputBetAmount;
    private ArrayList reelNum;
    private int numProb = 30;
	// Use this for initialization
	void Start () 
    {
        currentResult = 0;
        currentMoney = 100;
        Money.text = "" + currentMoney;
        reelNum = new ArrayList();
        for(int i = 0; i < numProb; i++)
        {
            reelNum.Add(0);
        }

        int remainingDivide = (100 - numProb) / 9; 
        for(int i = 1; i < 10; i++)
        {
            for(int j = 0; j <= remainingDivide && reelNum.Count < 100; j++)
            {
                reelNum.Add(i);
            }
        }
        Debug.Log(reelNum.Count);
	}
	
    public void InputBetAmount()
    {
        inputBetAmount = StringToInt(InputBet.text);
    }

    private int StringToInt(string bet)
    {
        int result = 0;
        for (int index = 0; index < bet.Length; index++)
        {
            result = result * 10 + (int)(bet[index] - '0');
        }
        return result;
    }

    public void PullLever()
    {
        if(currentMoney <= 0 || currentMoney < inputBetAmount)
        {
            return;
        }
        YouWin.gameObject.SetActive(false);
        if(inputBetAmount > maxBet)
        {
            inputBetAmount = maxBet;
        }
        currentMoney -= inputBetAmount;
        Money.text = "" + currentMoney;
        reelNum[0] = 1;
       
        int reelOne = (int)reelNum[Random.Range(0, 100)];
        int reelTwo = (int)reelNum[Random.Range(0, 100)];
        int reelTri = (int)reelNum[Random.Range(0, 100)];

        


        Reel1.text = "" + reelOne;
        Reel2.text = "" + reelTwo;
        Reel3.text = "" + reelTri;
        if (reelOne == reelTwo && reelTwo == reelTri)
        {
            currentResult = 500 * inputBetAmount;
            BetResult.text = "" + currentResult;
            currentMoney += currentResult;
            Money.text = "" + currentMoney;
            YouWin.gameObject.SetActive(true);
            return;
        }
        BetResult.text = "" + 0;
    }
}
