using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MakeAcc
    {
        string login = "https://my.freenom.com/clientarea.php";
        string filename = "result.txt";
        string error = "error.txt";
        

        private ChromeDriver dr;

        List<string> disabled;
        List<string> changeUrl;

        string dns1 = "dns20.onecloudy.xyz";

        internal void Close()
        {
            dr.Quit();
           
        }

          

        string dns2 = "dns21.onecloudy.xyz";

        private string _email;

        public MakeAcc()
        {
            this.changeUrl = new List<string>();
            disabled = new List<string>();

            _SetupDisabled();

        }

        private void _SetupDisabled()
        {
            disabled.Add(dns1);
            disabled.Add(dns2);
            //disabled.Add("dns5.onecloudy.xyz");
            //disabled.Add("dns6.onecloudy.xyz");
        }

        public void MakeLogin(string @email)
        {
            this._email = @email;
           
            dr = new ChromeDriver();
            var wait = new WebDriverWait(dr, TimeSpan.FromSeconds(30));

            this.dr.Url = login;
            dr.FindElementById("username").SendKeys(@email);
            dr.FindElementById("password").SendKeys("trance12");
            dr.FindElementByCssSelector("[value=Login]").Click();

            Thread.Sleep(3000);

            this.dr.Url = "https://my.freenom.com/clientarea.php?action=domains";

            Thread.Sleep(3000);
            var urls = dr.FindElementsByCssSelector("a.smallBtn.whiteBtn.pullRight");

            foreach(var url in urls)
            {
                this.changeUrl.Add(url.GetAttribute("href"));
            }

            
        }


        public void ChangeUrl()
        {
            foreach(string url in this.changeUrl)
            {
                try
                {
                    dr.Url = url;
                    dr.FindElementByCssSelector("a.dropdown-toggle.dropDown").Click();
                    Thread.Sleep(2000);
                    dr.FindElementByCssSelector("#tabs > ul > li.dropdown.active.open > ul > li:nth-child(1) > a").Click();
                    Thread.Sleep(5000);
                    string fist = dr.FindElementById("ns1").GetAttribute("value");
                   

                    if(this.disabled.Contains(fist))
                        throw new Exception("disabled value");

                    var ns = dr.FindElementById("ns1");
                    ns.Clear();
                    ns.SendKeys(dns1);

                    var ns2 = dr.FindElementById("ns2");
                    ns2.Clear();
                    ns2.SendKeys(dns2);

                    



                    dr.FindElementByXPath("//*[@id='tab3']/div/div[2]/div/form/fieldset/div[6]/p/input").Click();
                    Thread.Sleep(5000);

                    var domain = dr.FindElementByCssSelector("h1.primaryFontColor").Text.Replace("Managing","").Trim();

                    File.AppendAllText(this.filename, domain + ":" + this._email + Environment.NewLine);




                }
                catch(Exception ex)
                {
                    File.AppendAllText(this.error, url + ":" + this._email  + Environment.NewLine);
                    File.AppendAllText("error_big.txt", url + ":" + this._email + ":" + ex.Message + Environment.NewLine);

                }
            }
        }



      

    }
}
