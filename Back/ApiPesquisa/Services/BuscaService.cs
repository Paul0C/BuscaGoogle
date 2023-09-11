using System.Net;
using ApiPesquisa.Models;
using ApiPesquisa.Services.Interfaces;
using EasyAutomationFramework;
using HtmlAgilityPack;
using javax.lang.model.element;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ApiPesquisa.Services
{
    public class BuscaService : IBuscaService
    {
        public List<Resultado> RealizarPesquisa(string busca)
        {
            try
            {      
                var options = new ChromeOptions();  // Vi algumas formas de resolver esse problema, em várias usavam
                options.AddArguments("headless");   // esse HtmlDocument, achei um site em que ensinava como manipular 
                                                    //  e mexer com ele, essa primeira parte é a inicialização e carregamento do HtmlDocument da minha URl
                var driver = new ChromeDriver(options);

                var urlGoogle = $"https://www.google.com/search?q={busca.ToLower()}";

                driver.Navigate().GoToUrl(urlGoogle);
                var web = new HtmlWeb();
                var html = new HtmlDocument();
                html.LoadHtml(driver.PageSource);

                return GetResultados(html);  

            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        private List<Resultado> GetResultados(HtmlDocument html){
            var resultados = new List<Resultado>();

            var nodes = html.DocumentNode.SelectNodes("//*[@class=\"yuRUbf\"]/div/span/a/h3"); // Como o que eu preciso está no h3 e no a,
                                                                                               // eu seleciono esses nodes com esses caminhos
            if(nodes != null){
                foreach(var node in nodes){     
                    var linkAux = node.Ancestors("a").First(nodeA => nodeA.GetAttributeValue("jsname", "").Equals("UWckNb"));  // nessa linha eu seleciono o 'a' ancestral de h3                  
                    var resultado = new Resultado{      // e aqui eu seleciono o texto de h3 e também seleciono o link do ancestral de h3, que é o a
                        Titulo = node.InnerText,        // e seleciono o valor do atributo do href de a
                        Link = linkAux.GetAttributeValue("href", "")
                    };
                    resultados.Add(resultado);                                   
                }
            }

            return resultados;
        }
    }
}