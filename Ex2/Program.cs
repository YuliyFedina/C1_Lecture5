using System;
using System.Reflection;

namespace Ex2
{
    //Реализовать классы для управления банковскими счетами.
    //Каждый счет должен иметь номер, владельца, текущую сумму на счету не меньше нуля.
    //Типы счетов:
    //● сберегательный - возможность пополнения и изъятия денег со счета
    //● накопительный - возможность пополнения и изъятия денег со счета, но не меньше первоначального взноса, наличие процентной ставки, капитализация процентов за месяц
    //● расчетный - возможность пополнения и изъятия денег со счета, наличие платы за обслуживание, списание суммы за обслуживание со счета
    //● обезличенный металлический счет - тип металла, количество грамм, стоимость за грамм (текущий курс), возможность пополнять и обналичивать счет по текущему курсу
    //Реализовать метод закрытия счета. С закрытым счетом нельзя проводить никакие операции. Счет не может быть закрыт, если он имеет положителыный баланс.

    public class Account
    {
        public string Number { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public bool IsClosed { get; set; }


        public Account(string number, string owner, decimal balance)
        {
            Number = number;
            Owner = owner;
            Balance = balance;
        }

        public static void ValidationAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Сумма операции должна быть больше нуля");
            }
        }
        private void EnsureAccountNotClosed()
        {
            if (IsClosed)
            {
                throw new Exception("Операция невозможна для закрытого счета");
            }
        }
        protected virtual void DepositeCore(decimal amount)
        {
            Balance += amount;
        }
        public void Deposit(decimal amount)
        {
            EnsureAccountNotClosed();
            ValidationAmount(amount);
            DepositeCore(amount);
        }

        protected virtual void WithdrawCore(decimal amount)
        {
            Balance -= amount;
        }

        public void Withdraw(decimal amount)
        {
            EnsureAccountNotClosed();
            ValidationAmount(amount);
            if (Balance > amount)
            {
                WithdrawCore(amount);
            }
        }
    }

    public class SavingsAccount : Account
    {
        public SavingsAccount(string number, string owner, decimal balance)
            : base(number, owner, balance)
        {

        }
    }

    public class AccumulationAccount : Account
    {
        public decimal InitialInstalment { get; set; }
        public decimal Rate { get; set; }

        public AccumulationAccount(string number, string owner, decimal balance, decimal initialInstalment, decimal rate)
            : base(number, owner, balance)
        {
            InitialInstalment = initialInstalment;
            Rate = rate;
        }

        protected override void WithdrawCore(decimal amount)
        {
            if (Balance - InitialInstalment < amount)
            {
                throw new Exception("Нельзя снять с накопительного счета сумму больше первоначального взноса");
            }
            base.WithdrawCore(amount);
        }

        public void Capitalization()
        {
            Balance += Balance * Rate / 12;
        }
    }

    public class CurrentAccount : Account
    {
        public decimal MonthlyCost { get; set; }

        public CurrentAccount(string number, string owner, decimal balance, decimal monthlyCost)
            : base(number, owner, balance)
        {
            MonthlyCost = monthlyCost;
        }

        public void WithdrawMonthlyCost()
        {
            Balance -= MonthlyCost;
        }
    }

    public class BullionAccount : Account
    {
        public enum Metals
        {
            Gold,
            Silver
        }

        public decimal Course { get; set; }
        public Metals Metal { get; }

        public BullionAccount(string number, string owner, decimal balance, Metals metal, decimal course)
            : base(number, owner, balance)
        {
            Metal = metal;
            Course = course;
        }

        protected override void DepositeCore(decimal amount)
        {
            if (Course == 0)
            {
                throw new InvalidOperationException("Курс не может быть равен 0");
            }
            base.DepositeCore(amount / Course);
        }

        protected override void WithdrawCore(decimal amount)
        {
            if (Course == 0)
            {
                throw new InvalidOperationException("Курс не может быть равен 0");
            }
            base.WithdrawCore(amount / Course);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
