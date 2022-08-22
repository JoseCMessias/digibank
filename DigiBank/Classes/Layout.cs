using System;
using System.Globalization;

namespace DigiBank.Classes
{
    public class Layout 
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int Opcao = 0;

        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;

            Console.Clear();

            Console.WriteLine("                                                            ");
            Console.WriteLine("              Digite a Opção desejada:                      ");
            Console.WriteLine("             ================================               ");
            Console.WriteLine("              1 - Criar Conta                               ");
            Console.WriteLine("             ================================               ");
            Console.WriteLine("              2 - Entrar com CPF e Senha                    ");
            Console.WriteLine("             ================================               ");

            Opcao = int.Parse(Console.ReadLine());
            switch (Opcao)
            {
                case 1:
                    TelaDeCriarConta();
                    break;
                case 2:
                    TelaDeLogin();
                    break;
                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }
        }

        private static void TelaDeCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                                 ");
            Console.Write("                   Digite o seu nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("                   =======================================        ");
            Console.Write("                   Digite o seu CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                   =======================================        ");
            Console.Write("                   Digite a sua senha: ");
            string senha = Console.ReadLine();
            Console.WriteLine("                   =======================================        ");

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("                   Conta cadastrada com sucesso.                  ");
            Console.WriteLine("                   =======================================        ");

            // Esse código espera 1 segundo para ir a TelaDeLogada
            Thread.Sleep(1000);

            TelaDaContaLogada(pessoa);
        }

        private static void TelaDeLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                              ");
            Console.Write("                  Digite seu CPF: ");
            string Cpf = Console.ReadLine();
            Console.WriteLine("                   ====================================        ");
            Console.Write("                  Digite a sua senha: ");
            string Senha = Console.ReadLine();
            Console.WriteLine("                   ====================================        ");

            //Logar no sistema

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == Cpf && x.Senha == Senha);

            if(pessoa != null)
            {
                TelaDeBoasVindas(pessoa);

                TelaDaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("                   Pessoa não cadastrada                          ");
                Console.WriteLine("                   =======================================        ");

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void TelaDeBoasVindas(Pessoa pessoa)
        {
            string msgTelaDeBemVindo
                = $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} " +
                $"| Agência: {pessoa.Conta.GetNumeroDaAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()}";

            Console.WriteLine("");
            Console.WriteLine($"             Seja bem vindo, {msgTelaDeBemVindo}                    ");
            Console.WriteLine("");
        }

        private static void TelaDaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaDeBoasVindas(pessoa);

            Console.WriteLine("                      Digite a Opção desejada:                       ");
            Console.WriteLine("                      =======================================        ");
            Console.WriteLine("                      1 - Realizar um Deposito                       ");
            Console.WriteLine("                      =======================================        ");
            Console.WriteLine("                      2 - Realizar um Saque                          ");
            Console.WriteLine("                      =======================================        ");
            Console.WriteLine("                      3 - Consultar o Saldo                          ");
            Console.WriteLine("                      =======================================        ");
            Console.WriteLine("                      4 - Extrato                                    ");
            Console.WriteLine("                      =======================================        ");
            Console.WriteLine("                      5 - Sair                                       ");
            Console.WriteLine("                      =======================================        ");

            Opcao = int.Parse(Console.ReadLine());

            switch (Opcao)
            {
                case 1:
                    TelaDeDeposito(pessoa);
                    break;
                case 2:
                    TelaDeSaque(pessoa);
                    break;
                case 3:
                    TelaDeSaldo(pessoa);
                    break;
                case 4:
                    TelaDeExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("                      Opção Inválida!                    ");
                    Console.WriteLine("                      =============================      ");
                    break;
            }
        }

        private static void TelaDeDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaDeBoasVindas(pessoa);
            Console.Write("                       Digite o valor do deposito: ");
            double valorDoDeposito = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("                   ==============================                ");

            pessoa.Conta.Deposita(valorDoDeposito);

            Console.Clear();

            TelaDeBoasVindas(pessoa);

            Console.WriteLine("                                                                 ");
            Console.WriteLine("                                                                 ");
            Console.WriteLine("                  Deposito realizado com sucesso!                ");
            Console.WriteLine("                  ================================               ");
            Console.WriteLine("                                                                 ");
            Console.WriteLine("                                                                 ");

            OpcãoVoltarLogado(pessoa);
        }

        private static void TelaDeSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaDeBoasVindas(pessoa);
            Console.Write("                       Digite o valor do Saque: ");
            double valorDoSaque = double.Parse(Console.ReadLine());
            Console.WriteLine("");
            Console.WriteLine("                   ==============================                ");

            bool okSaque = pessoa.Conta.Saca(valorDoSaque);

            Console.Clear();

            TelaDeBoasVindas(pessoa);

            Console.WriteLine("                                                                 ");
            Console.WriteLine("                                                                 ");

            if (okSaque)
            {
                Console.WriteLine("                  Saque realizado com sucesso!               ");
                Console.WriteLine("                  ================================           ");
            }
            else
            {
                Console.WriteLine("                  Saldo Insuficiente!                        ");
                Console.WriteLine("                  ================================           ");
            }

            Console.WriteLine("                                                                 ");
            Console.WriteLine("                                                                 ");

            OpcãoVoltarLogado(pessoa);
        }

        private static void TelaDeSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaDeBoasVindas(pessoa);

            Console.WriteLine($"                  Seu saldo é: {pessoa.Conta.ConsultaSaldo()}   ");
            Console.WriteLine("                  ==============================                 ");
            Console.WriteLine("                                                                 ");

            OpcãoVoltarLogado(pessoa);
        }

        private static void TelaDeExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaDeBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any())
            {
                //Mostrar extrato
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach(Extrato extrato in pessoa.Conta.Extrato())
                {
                    
                    Console.WriteLine("                                                                 ");
                    Console.WriteLine($"                  Date: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"                  Tipo de Movimentação: {extrato.Descricao}     ");
                    Console.WriteLine($"                  Valor: {extrato.Valor}                        ");
                    Console.WriteLine("                  ================================               ");
                }

                Console.WriteLine("                                                                 ");
                Console.WriteLine("                                                                 ");
                Console.WriteLine($"                  SUB TOTAL: {total}                            ");
                Console.WriteLine("                  ================================               ");
            }
            else
            {
                //mostrar uma mensagem que não há extrato

                Console.WriteLine("                   Não há extrato a ser exibido!            ");
                Console.WriteLine("                  ==============================            ");
            }

            OpcãoVoltarLogado(pessoa);
        }

        private static void OpcãoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                  Entre com uma opção abaixo!                    ");
            Console.WriteLine("                  ==============================                 ");
            Console.WriteLine("                  1 - Voltar para a minha conta                  ");
            Console.WriteLine("                  ==============================                 ");
            Console.WriteLine("                  2 - Sair                                       ");
            Console.WriteLine("                  ==============================                 ");

            Opcao = int.Parse(Console.ReadLine());
            if(Opcao == 1)
            {
                TelaDaContaLogada(pessoa);
            }
            else
            {
                TelaPrincipal();
            }
        }

        private static void OpcãoVoltarDeslogado()
        {
            Console.WriteLine("                  Entre com uma opção abaixo!                    ");
            Console.WriteLine("                  =================================              ");
            Console.WriteLine("                  1 - Voltar para ao menu principal              ");
            Console.WriteLine("                  =================================              ");
            Console.WriteLine("                  2 - Sair                                       ");
            Console.WriteLine("                  =================================              ");

            Opcao = int.Parse(Console.ReadLine());
            if (Opcao == 1)
            {
                TelaPrincipal();
            }
            else
            {
                Console.WriteLine("                  Opção Invalida!                            ");
                Console.WriteLine("               =================================             ");
            }  
        }
    }
}
