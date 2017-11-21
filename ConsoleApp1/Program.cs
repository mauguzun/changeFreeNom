using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] emails = File.ReadAllLines("emails.txt");

            foreach(string email in emails)
            {
                MakeAcc acc = new MakeAcc();
                acc.MakeLogin(email);
                acc.ChangeUrl();
                acc.Close();
            }


            
            //ChromeOptions option = new ChromeOptions();

            //option.AddArgument("--window-size=300,300");
            //option.AddArgument("--proxy-server=88.159.140.220:80");

            //ChromeDriver dr = new ChromeDriver(option);



            //var wait = new WebDriverWait(dr,TimeSpan.FromSeconds(10));

            //dr.Navigate().GoToUrl("http://pinterest.com");
            
            //try
            //{
            //    wait.Until(d => d.Title == "Pinterest");
            //}
            //catch(Exception ex )
            //{
            //    Console.WriteLine(ex.Message);
            //}
            

            //Console.WriteLine("S");
       
            //Console.ReadKey();


        }
    }
}
