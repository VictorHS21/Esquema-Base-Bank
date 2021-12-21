using System;

namespace classes
{
    class Program
    {
        static void Valida()
        {
            Console.Clear();
            Console.WriteLine("Deseja criar uma conta neste banco? [sim/nao]");
            string criarResposta = Console.ReadLine();

            if (criarResposta == "sim")
            {
                CriarConta();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Agradecemos a sua visita.");
            }
        }
        static void CriarConta()
        {
            Console.Clear();
            Console.WriteLine("Por favor nos informe seu nome e a quantinha que deseja adicionar.");
            Console.WriteLine();
            Console.WriteLine("Nome:");
            string nomeCliente = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Valor:");
            decimal valorCliente = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();
            try
            {
                var cliente = new BankAccount(nomeCliente, valorCliente);
                Console.WriteLine($"Olá {cliente.Owner}, foi depositado {cliente.Balance} em sua conta.");
                Console.ReadLine();
                Menu(cliente);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.ReadLine();
                CriarConta();
            }
        }
        static void Menu(BankAccount cliente)
        {
            
            bool sair = false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"Seja Bem-Vindo {cliente.Owner} ao nosso banco virtual.");
                    Console.WriteLine();
                    Console.WriteLine("Qual tansação gostaria de fazer?");
                    Console.WriteLine();
                    Console.WriteLine("Digite: 1 - Saque");
                    Console.WriteLine("Digite: 2 - Deposito");
                    Console.WriteLine("Digite: 3 - Extrato");
                    Console.WriteLine("Digite: 4 - Sair");
                    Console.WriteLine();
                    int transação = Convert.ToInt32(Console.ReadLine());
                    switch (transação)
                    {
                        case 1:
                            {
                                Sacar(cliente);
                                break;
                            }
                        case 2:
                            {
                                Depositar(cliente);
                                break;
                            }
                        case 3:
                            {
                                Extrato(cliente);
                                break;
                            }
                        case 4:
                            {
                                sair = true;
                                break;
                            }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            } while (sair == false);

        }
        static void Main()
        {
            Valida();
        }






        static void Sacar(BankAccount cliente)
        {
            Console.Clear();
            Console.WriteLine("Qual a quantia que deseja retirar a sua conta?");
            Console.WriteLine();
            decimal valor = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Qual o motivo para esse saque?");
            Console.WriteLine();
            string descricao = Console.ReadLine();
            cliente.MakeWithdrawal(valor, DateTime.Now, descricao);
            Console.Clear();
            Console.WriteLine("O saque foi executado com sucesso");
            Console.ReadLine();
        }
        static void Depositar(BankAccount cliente)
        {
            Console.Clear();
            Console.WriteLine("Qual a quantia que deseja adicionar a sua conta?");
            Console.WriteLine();
            decimal valor = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Qual o motivo para esse deposito?");
            Console.WriteLine();
            string descricao = Console.ReadLine();
            cliente.MakeDeposit(valor, DateTime.Now, descricao);
            Console.Clear();
            Console.WriteLine("O deposito foi executado com sucesso");
            Console.ReadLine();
        }
        static void Extrato(BankAccount cliente)
        {
            Console.Clear();
            Console.WriteLine(cliente.GetAccountHistory());
            Console.ReadLine();
        }









        static void Conseito()
        {
            var account = new BankAccount("Victor", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);
            Console.WriteLine(account.GetAccountHistory());

            var a = new BankAccount("Victor h", 1000);
            Console.WriteLine($"Account {a.Number} was created for {a.Owner} with {a.Balance} initial balance.");
            a.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(a.Balance);
            a.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(a.Balance);

            // Test for a negative balance.
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine();
            // Test that the initial balances must be positive.
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }
}
