using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FeedMinutoSeguro
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            XDocument xml = XDocument.Load(@"../../../feed.xml");

            Dictionary<string, int> contador = null;

            List<ContadorGeral> contadorGeral = new List<ContadorGeral>();

            int numeroTopico = 1;
            int ordem;

            foreach (XElement item in xml.Document.Descendants("item"))
            {
                contador = new Dictionary<string, int>();

                List<string> palavras =
                    item
                    .Value
                    .ToLower()
                    .Split(new char[] { ' ', ',', '.', ':', ';', '?', '!', '"', '\'', '>' })
                    .ToList()
                    .RemoveAll();

                foreach (var palavra in palavras)
                {
                    if (!contador.Keys.Contains(palavra))
                        contador.Add(palavra, palavras.Count(p => p == palavra));

                    if (!contadorGeral.Exists(c => c.Palavra.Equals(palavra)) ||
                        contadorGeral.Exists(c => c.Palavra.Equals(palavra) && !c.Topico.Contains(numeroTopico)))
                    {
                        if (contadorGeral.Exists(c => c.Palavra.Equals(palavra)))
                        {
                            ContadorGeral cg = contadorGeral.FirstOrDefault(c => c.Palavra.Equals(palavra));

                            cg.Quantidade += palavras.Count(p => p == palavra);
                            cg.Topico.Add(numeroTopico);
                        }
                        else
                        {
                            List<int> topico = new List<int>();
                            topico.Add(numeroTopico);

                            contadorGeral.Add(new ContadorGeral() { Palavra = palavra, Quantidade = palavras.Count(p => p == palavra), Topico = topico});
                        }
                    }
                }

                Console.WriteLine("\n\nPalavras mais usadas no TOPICO {0}", numeroTopico);
                ordem = 1;
            
                foreach (var itemContador in contador.OrderByDescending(a => a.Value).Take(10))
                {
                    Console.WriteLine("\n{0}ª) \"{1}\"; \nQuantidade de repeticoes: {2}", ordem, itemContador.Key.ToUpper(), itemContador.Value);

                    ordem++;
                }

                numeroTopico++;
            }

            ordem = 1;

            Console.WriteLine("\n\nPalavras mais usada no GERAL");

            foreach (var item in contadorGeral.OrderByDescending(cg => cg.Quantidade).Take(10))
            {

                Console.WriteLine("\n{0}ª) \"{1}\"; \nQuantidade de repeticoes: {2}", ordem, item.Palavra.ToUpper(), item.Quantidade);
                ordem++;
            }

            Console.ReadKey();
            Console.Clear();
        }

        static List<string> RemoveAll(this List<string> palavras)
        {
            palavras.RemoveAll(p => p.Equals("a"));
            palavras.RemoveAll(p => p.Equals("ao"));
            palavras.RemoveAll(p => p.Equals("aos"));
            palavras.RemoveAll(p => p.Equals("as"));
            palavras.RemoveAll(p => p.Equals("ante"));
            palavras.RemoveAll(p => p.Equals("após"));
            palavras.RemoveAll(p => p.Equals("até"));
            palavras.RemoveAll(p => p.Equals("com"));
            palavras.RemoveAll(p => p.Equals("como"));
            palavras.RemoveAll(p => p.Equals("conforme"));
            palavras.RemoveAll(p => p.Equals("contra"));
            palavras.RemoveAll(p => p.Equals("da"));
            palavras.RemoveAll(p => p.Equals("das"));
            palavras.RemoveAll(p => p.Equals("de"));
            palavras.RemoveAll(p => p.Equals("desde"));
            palavras.RemoveAll(p => p.Equals("do"));
            palavras.RemoveAll(p => p.Equals("dos"));
            palavras.RemoveAll(p => p.Equals("e"));
            palavras.RemoveAll(p => p.Equals("em"));
            palavras.RemoveAll(p => p.Equals("entre"));
            palavras.RemoveAll(p => p.Equals("na"));
            palavras.RemoveAll(p => p.Equals("nas"));
            palavras.RemoveAll(p => p.Equals("no"));
            palavras.RemoveAll(p => p.Equals("nos"));
            palavras.RemoveAll(p => p.Equals("o"));
            palavras.RemoveAll(p => p.Equals("os"));
            palavras.RemoveAll(p => p.Equals("para"));
            palavras.RemoveAll(p => p.Equals("perante"));
            palavras.RemoveAll(p => p.Equals("por"));
            palavras.RemoveAll(p => p.Equals("que"));
            palavras.RemoveAll(p => p.Equals("quais"));
            palavras.RemoveAll(p => p.Equals("qual"));
            palavras.RemoveAll(p => p.Equals("sem"));
            palavras.RemoveAll(p => p.Equals("sob"));
            palavras.RemoveAll(p => p.Equals("sobre"));
            palavras.RemoveAll(p => p.Equals("um"));
            palavras.RemoveAll(p => p.Equals("uma"));
            palavras.RemoveAll(p => p.Equals("http"));
            palavras.RemoveAll(p => p.Equals("https"));
            palavras.RemoveAll(p => p.Equals("post"));
            palavras.RemoveAll(p => p.Equals("posts"));
            palavras.RemoveAll(p => p.Equals("get"));
            palavras.RemoveAll(p => p.Equals("put"));
            palavras.RemoveAll(p => p.Equals("nofollow"));
            palavras.RemoveAll(p => p.Equals("on"));
            palavras.RemoveAll(p => p.Equals("off"));
            palavras.RemoveAll(p => p.Equals("azurewebsites"));
            palavras.RemoveAll(p => p.Equals("azurewebsite"));
            palavras.RemoveAll(p => p.Equals("first"));
            palavras.RemoveAll(p => p.Equals("appeared"));
            palavras.RemoveAll(p => p.Equals("related"));
            palavras.RemoveAll(p => p.Equals("jpg"));
            palavras.RemoveAll(p => p.Equals("the"));
            palavras.RemoveAll(p => p.Equals(""));
            palavras.RemoveAll(p => p.Equals((char)13));
            palavras.RemoveAll(p => p.Contains("yarpp"));
            palavras.RemoveAll(p => p.Contains("rss"));
            palavras.RemoveAll(p => p.Contains('<'));
            palavras.RemoveAll(p => p.Contains('<'));
            palavras.RemoveAll(p => p.Contains('>'));
            palavras.RemoveAll(p => p.Contains('/'));
            palavras.RemoveAll(p => p.Contains('='));
            palavras.RemoveAll(p => p.Contains('_'));
            palavras.RemoveAll(p => p.Contains('0'));
            palavras.RemoveAll(p => p.Contains('1'));
            palavras.RemoveAll(p => p.Contains('2'));
            palavras.RemoveAll(p => p.Contains('3'));
            palavras.RemoveAll(p => p.Contains('4'));
            palavras.RemoveAll(p => p.Contains('5'));
            palavras.RemoveAll(p => p.Contains('6'));
            palavras.RemoveAll(p => p.Contains('7'));
            palavras.RemoveAll(p => p.Contains('8'));
            palavras.RemoveAll(p => p.Contains('9'));
            palavras.RemoveAll(p => p.Count() < 3);

            return palavras;
        }
    }

    class ContadorGeral
    {
        public string Palavra { get; set; }
        public int Quantidade { get; set; }
        public List<int> Topico { get; set; }
    }
}
