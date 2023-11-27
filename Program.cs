using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace MonkeyTypeTyper
{
    internal class Program
    {
        static int desiredWPM, delay;

        static void Main(string[] args)
        {
            //Prompt the user the WPM that they want.
            desiredWPM = AnsiConsole.Ask<int>("What is your target WPM?");
            delay = CalculateKeystrokeDelay(desiredWPM);

            //Initialize the Chrome Driver
            using (var driver = new ChromeDriver())
            {
                try
                {
                    //Navigate to Monkeytype
                    driver.Navigate().GoToUrl("https://monkeytype.com/");

                    #region Accepting Cookies and etc.

                    //Upon first-time visit on a fresh browser, monkeytype asks for cookie permissions.
                    WaitForElementAndAct(
                        driver,
                        By.CssSelector("button[aria-label='Consent']"),
                        element => element.Click(),
                        TimeSpan.FromSeconds(10)
                    );

                    WaitForElementAndAct(
                        driver,
                        By.ClassName("rejectAll"),
                        element => element.Click(),
                        TimeSpan.FromSeconds(10)
                    );

                    //Now that we have accepted all the cookies, we can just go ahead and start typing.

                    #endregion

                    #region Word reading & Typing
                    Console.ReadLine();


                    IReadOnlyCollection<IWebElement> wordElements = driver.FindElements(By.CssSelector("#words .word"));
                    List<string> wordsToType = new List<string>();

                    foreach (var wordElement in wordElements)
                    {
                        string word = string.Join("", wordElement.FindElements(By.TagName("letter")).Select(e => e.Text));
                        wordsToType.Add(word);
                    }

                    TypeWords(driver, wordsToType, TimeSpan.FromSeconds(5), delay);

                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public static void WaitForElementAndAct(IWebDriver driver, By by, Action<IWebElement> action, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));

                action(element);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element not found within the specified timeout: {by}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void TypeWords(IWebDriver driver, List<string> words, TimeSpan timeout, int delayBetweenKeystrokes)
        {
            try
            {
                var actions = new Actions(driver);

                foreach (var word in words)
                {
                    foreach (char c in word)
                    {
                        actions.SendKeys(c.ToString()).Perform();
                        Thread.Sleep(delayBetweenKeystrokes);
                    }
                    //Typing a space after each word
                    actions.SendKeys(" ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong when trying to type in a word! Error stack: {ex}");
            }
        }

        public static int CalculateKeystrokeDelay(int wpm)
        {
            if (wpm <= 0)
                throw new ArgumentException("WPM must be greater than 0.");

            int charactersPerMinute = wpm * 5; //Standard word length is 5 characters
            return 60000 / charactersPerMinute; //Delay in milliseconds
        }
    }
}
