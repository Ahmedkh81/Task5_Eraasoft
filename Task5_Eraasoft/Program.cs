using System.Xml.Linq;

namespace Task5_Eraasoft
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Account operator+(Account lhs, Account rhs)
        {
            Account ac = new(lhs.Name + " " + rhs.Name, lhs.Balance + rhs.Balance);
            return ac;
        }
    }
    public class SavingAccount : Account
    {
        public double IntRate { get; set; }

        public SavingAccount(double intRate = 0.0 , string name = "Unnamed Account", double balance = 0.0) : base(name, balance)
        {
            IntRate = intRate;
        }

        public bool Withdarw(double amount)
        {
            return base.Withdraw(amount + (amount * IntRate));
        }
    }
    public class CheckingAccount : Account
    {
        public readonly double Fee;

        public CheckingAccount(string name = "Unnamed Account", double balance = 0.0) : base(name, balance)
        {
            Fee = 1.50;
        }
        public bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Fee);
        }
    }
    public class TrustAccount : SavingAccount
    {
        public TrustAccount(double intRate = 0.0, string name = "Unnamed Account", double balance = 0) : base(intRate, name, balance)
        {

        }
        public bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Balance += amount;
                Balance += 50;
                return true;
            }
            else
                return base.Deposit(amount);
        }
        private int withdrawCount = 0;
        public bool Withdraw(double amount)
        {
            int currentYear = DateTime.Now.Year;

            if (withdrawCount < 3)
            {
                if (amount < Balance * 0.2)
                {
                    withdrawCount++;
                    base.Withdraw(amount);
                    return true;
                }
            }
            return false;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SavingAccount s1 = new(0.02, "Ahmed", 3500);
            CheckingAccount c1 = new("Ahmed", 1000);
            TrustAccount t1 = new(0.02, "Mohamed", 1000);


            //Console.WriteLine(s1.Withdarw(300));
            //Console.WriteLine(s1.Balance);
            //Console.WriteLine(c1.Withdraw(100));
            //Console.WriteLine(c1.Balance);
            //Console.WriteLine(t1.Deposit(5000));
            //Console.WriteLine(t1.Balance);
            //Console.WriteLine(t1.Withdraw(2000));
            //Console.WriteLine(t1.Balance);
            //Console.WriteLine(t1.Withdraw(100));
            //Console.WriteLine(t1.Balance);
            //Console.WriteLine(t1.Withdraw(100));
            //Console.WriteLine(t1.Balance);
            //Console.WriteLine(t1.Withdraw(100));
            //Console.WriteLine(t1.Balance);

            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingAccount>();
            savAccounts.Add(new SavingAccount());
            savAccounts.Add(new SavingAccount(name: "Superman"));
            savAccounts.Add(new SavingAccount(name: "Batman", balance: 2000));
            savAccounts.Add(new SavingAccount(name: "Wonderwoman", balance: 5000, intRate: 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.DepositChecking(checAccounts, 1000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount(name: "Superman2"));
            trustAccounts.Add(new TrustAccount(name: "Batman2",  balance: 2000));
            trustAccounts.Add(new TrustAccount(name: "Wonderwoman2", balance: 5000, intRate: 5.0));

            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);
            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine();
        }
    }

    public static class AccountUtil
    {
        // Utility helper functions for Account class
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for SavingsAccount
        public static void DepositSavings(List<SavingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawSavings(List<SavingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for CheckingAccount
        public static void DepositChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for TrustAccount
        public static void DepositTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
    
}
    

