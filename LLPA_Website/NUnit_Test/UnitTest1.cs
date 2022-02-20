using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnit_Test
{
    public class Tests
    {
        private readonly IWebDriver _driver;

        public Tests() => _driver = new OpenQA.Selenium.Chrome.ChromeDriver(Environment.CurrentDirectory);
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void Login_WhenClicked_ShouldLoginWithValidCredentials()
        {
            _driver.Navigate()
                .GoToUrl("http://pizzaapp.eastus.cloudapp.azure.com/login");

            _driver.FindElement(By.Id("login-name"))
                .SendKeys("hkhan1241@gmail.com");

            _driver.FindElement(By.Id("login-pass"))
                .SendKeys("asdf1234");

            _driver.FindElement(By.Id("btnLogon"))
                .Click();

            Assert.AreEqual("Home page - Pizza Order", _driver.Title);


            //_driver.FindElement(By.LinkText("Cerrar Sesión"))
            //.Click();

            _driver.FindElement(By.ClassName("display"))
                .Click();

            _driver.FindElement(By.ClassName("btn-link"))
                .Click();
        }


    }
}