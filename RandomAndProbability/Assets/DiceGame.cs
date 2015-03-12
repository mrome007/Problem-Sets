using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DiceGame : MonoBehaviour 
{
    public Text EditEndTextField;
    public Text YouWin;
    public Text YouLose;
    private string toCompare;
    private string lose = "You Lose";
    private string win = "You Win";

    void Start()
    {
        toCompare = "";
    }

    public void OnEditEndInputField()
    {
        toCompare = EditEndTextField.text;
    }

    private int ThrowDice()
    {
        int randomNum = Random.Range(1, 101);
        if (randomNum < 35)
        {
            randomNum = 6;
        }
        else
        {
            randomNum = Random.Range(1, 6);
        }
        return randomNum;
    }

	public void OnClickPlayButton()
    {
        int randomNum = ThrowDice();
        int result = StringToInt(toCompare);
        if(randomNum == result)
        {
            YouWin.gameObject.SetActive(true);
            string extra = (" " + toCompare);
            YouWin.text = win + extra;
            YouLose.gameObject.SetActive(false);
        }
        else
        {
            YouLose.gameObject.SetActive(true);
            string extra = (" " + toCompare + "-" + randomNum);
            YouLose.text = lose + extra;
            YouWin.gameObject.SetActive(false);
        }
    }

    int StringToInt(string num)
    {
        int result = 0;
        for(int index = 0; index < num.Length; index++)
        {
            result = 10 * result + (int)(num[index]-'0');
        }
        return result;
    }
}
