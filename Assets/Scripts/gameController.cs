using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    private Text message;
    static private int from;
    static private int to;
    static private int numberToGuess;
    static private int tries;
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    public static string descriptionParam;
    private string appStoreLink = "http://www.YOUROWNAPPLINK.com";
    Button button;
    InputField playerInputField;

    public void ShareToTW(string linkParameter)
    {

        string nameParameter = "I GUESSED THE NUMBER IN " + tries + " TRIES ! BEAT ME MADAFACA! "; //This has the length limitation of a tweet! 
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + WWW.EscapeURL(nameParameter + "\n" + descriptionParam + "\n" + "Get the Game:\n" + appStoreLink));
    }

    private void Start()
    {
        button = GameObject.Find("Tweet").GetComponent<Button>();
        playerInputField = GameObject.Find("InputField").GetComponent<InputField>();
        from = (int)UnityEngine.Random.Range(0f, 50f);
        to = (int)UnityEngine.Random.Range(51f, 100f);
        numberToGuess = (int)UnityEngine.Random.Range(from, to);
        HideButton();
        message = GameObject.Find("MyMessage").GetComponent<Text>();
        StartCoroutine(Wait("", "I'm thinking of a number... ok? So... yeah, you gotta guess it :)"));
        StartCoroutine(Wait("wait", "wut"));
        StartCoroutine(Wait("huh", "Okay, I got it! The number is between " + from + " and " + to + " !"));
    }
    IEnumerator Wait(string s0, string s1)
    {
        message.text = s0;
        yield return new WaitForSeconds(2);
        message.text = s1;
    }
    public void GetInput(string input)
    {
        try
        {
            Compare(int.Parse(input));
        }
        catch (Exception e)
        {
            StartCoroutine(Wait("Incorrect input! Don't be fooling 'round kiddo!", "The number is between " + from + " and " + to + " !"));
            Debug.Log(e.StackTrace);

        }
    }

    public void Compare(int guessedNumber)
    {

        if (guessedNumber < numberToGuess)
        {
            StartCoroutine(Wait("Nope! It's higher than that! ", "The number is between " + from + " and " + to + " !"));
            playerInputField.text = "";
            tries++;
        }
        if (guessedNumber > numberToGuess)
        {
            StartCoroutine(Wait("Nope! It's smaller than that! ", "The number is between " + from + " and " + to + " !"));
            playerInputField.text = "";
            tries++;
        }

        if (guessedNumber < from)
        {
            StartCoroutine(Wait("No! That's not even between the numbers I'm telling you!", "The number is between " + from + " and " + to + " !"));
            playerInputField.text = "";
            tries++;
        }

        if (guessedNumber > to)
        {
            StartCoroutine(Wait("No! Way to high! That's not between the numbers I'm telling you!", "The number is between " + from + " and " + to + " !"));
            playerInputField.text = "";
            tries++;
        }
        if (guessedNumber == numberToGuess)
        {
            tries++;
            message.text = "Congratulations ! You got it in " + tries + " tries! ";
            ShowButton();
        }
    }
    public void ShowButton()
    {
        button.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        button.gameObject.SetActive(false);
    }
}
