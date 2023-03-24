namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------- Jogo da Forca ------- \n");

            string[] palavrasAleatorias = { "ABACATE", "ABACAXI", "ACEROLA", "AÇAÍ", "ARAÇA", "ABACATE", "BACABA", "BACURI", "BANANA", "CAJÁ", "CAJÚ", "CARAMBOLA",
                "CUPUAÇU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MAÇÃ", "MANGABA", "MANGA", "MARACUJÁ", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI",
                "TANGERINA", "UMBU", "UVA", "UVAIA"};

            int valorAleatorio = new Random().Next(0, palavrasAleatorias.Length - 1);
            char[] palavraSecreta = palavrasAleatorias[valorAleatorio].ToCharArray();
            char[] visualizacaoPalavraSecreta = new char[palavraSecreta.Length];
            int erros = 1;
            bool erro = true;
            string validacaoDoPalpite;
            char palpite = ' ';

            DesenharForca(erros);

            DesenharEspacosSecretos(palavraSecreta, visualizacaoPalavraSecreta);

            ValidarPalpite(out validacaoDoPalpite, out palpite);

            while (erros < 5)
            {

                DesenharForca(erros);

                erro = ImprimirTentativa(palavraSecreta, visualizacaoPalavraSecreta, erro, palpite);

                erros = IncrementarErro(erros, erro);

                if (ValidarFimDeJogo(visualizacaoPalavraSecreta, palavraSecreta))
                {
                    break;
                }

                erro = true;

                ValidarPalpite(out validacaoDoPalpite, out palpite);

                Console.WriteLine();

            }

            ValidarDerrota(erros);
        }

        static void DesenharForca(int erros)
        {
            Console.Clear();
            Console.WriteLine("------- Jogo da Forca ------- \n");
            Console.WriteLine("\n");
            Console.WriteLine(" -----------");
            Console.WriteLine(" |/        |");
            Console.WriteLine(" |        {0}", erros >= 2 ? " O " : " ");
            Console.WriteLine(" |        {0}{1}{2}", erros >= 3 ? "/" : " ", erros >= 3 ? "X" : " ", erros >= 3 ? "\\" : " ");
            Console.WriteLine(" |         {0}", erros >= 4 ? "X" : " ");
            Console.WriteLine(" |        {0} {1}", erros == 5 ? "/" : " ", erros == 5 ? "\\" : " ");
            Console.WriteLine(" |");
            Console.WriteLine(" |");
            Console.WriteLine("_|____\n");
        }
        static void DesenharEspacosSecretos(char[] palavraSecreta, char[] visualizacaoPalavraSecreta)
        {
            for (int i = 0; i < palavraSecreta.Length; i++)
            {
                Console.Write("_");
                visualizacaoPalavraSecreta[i] = '_';
            }
        }
        static void ValidarPalpite(out string validacaoDoPalpite, out char palpite)
        {
            Console.Write("\n\nDigite uma letra: ");
            validacaoDoPalpite = Console.ReadLine().ToUpper();

            while (validacaoDoPalpite == "")
            {
                Console.WriteLine("\nA letra não pode ser vazia");
                Console.Write("\nDigite outra letra: ");
                validacaoDoPalpite = Console.ReadLine().ToUpper();
            }

            palpite = Convert.ToChar(validacaoDoPalpite);
        }
        static bool ImprimirTentativa(char[] palavraSecreta, char[] visualizacaoPalavraSecreta, bool erro, char palpite)
        {
            for (int i = 0; i < palavraSecreta.Length; i++)
            {
                if (palpite != palavraSecreta[i])
                {
                    if (visualizacaoPalavraSecreta[i] == '_')
                    {
                        visualizacaoPalavraSecreta[i] = '_';
                    }
                }

                else if (palpite == palavraSecreta[i])
                {
                    visualizacaoPalavraSecreta[i] = palavraSecreta[i];
                    erro = false;
                }

                Console.Write(visualizacaoPalavraSecreta[i]);
            }

            return erro;
        }
        static int IncrementarErro(int erros, bool erro)
        {
            if (erro == true)
            {
                Console.WriteLine("\n\nLetra Errada!");
                Console.Write("\nPressione ENTER para tentar novamente");
                Console.ReadKey();
                erros++;
            }

            return erros;
        }
        static bool ValidarFimDeJogo(char[] visualizacaoPalavraSecreta, char[] palavraSecreta)
        {
            bool ehIgual = visualizacaoPalavraSecreta.SequenceEqual(palavraSecreta);

            if (ehIgual == true)
            {
                Console.Clear();
                Console.WriteLine("------- Jogo da Forca ------- \n");
                Console.WriteLine("*************************************");
                Console.WriteLine("* Você encontrou a palavra secreta! *");
                Console.WriteLine("*             Parabéns!             *");
                Console.WriteLine("*************************************\n");
            }

            return ehIgual;
        }
        static void ValidarDerrota(int erros)
        {
            if (erros == 5)
            {
                Console.Clear();
                DesenharForca(erros);
                Console.WriteLine("Você perdeu! :(");
                Console.ReadKey();
            }
        }

    }
}