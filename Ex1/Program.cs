using System;

namespace Ex1
{
    // Реализовать классы для описания клиентов двух типов: ИП и ООО.
    // Каждый тип клиента имеет идентификатор, основной телефон, сумма заказа.
    // У ИП указывается ФИО, дата рождения.
    // У ООО указывается название, банковский счет.
    // Реализовать метод, который возвращает отформатированное название и сумму заказа.

    public class Client
    {
        public long Id { get; }
        public string MainContact { get; set; }
        public decimal SumOfOrder { get; set; }

        public Client(long id, string mainContact, decimal sumOfOrder)
        {
            Id = id;
            MainContact = mainContact;
            SumOfOrder = sumOfOrder;
        }

        public virtual string ClientInformation => $"сумма заказа {SumOfOrder} руб.";
    }

    public class LimitedLiabilityCompany : Client
    {
        public string Name { get; set; }
        public string BankAccount { get; set; }

        public LimitedLiabilityCompany(long id, string mainContact, decimal sumOfOrder, string name, string bankAccout)
            : base(id, mainContact, sumOfOrder)
        {
            Name = name;
            BankAccount = bankAccout;
        }

        public override string ClientInformation => $"{Name}, " + base.ClientInformation;
    }

    public class SoleProprietor : Client
    {
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }

        public SoleProprietor(long id, string mainContact, long sumOfOrder, string fullName, string dateOfBirth)
            : base(id, mainContact, sumOfOrder)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
        }

        public override string ClientInformation => $"{FullName}, " + base.ClientInformation;
    }

    class Program
    {
        static void Main()
        {
            LimitedLiabilityCompany clientLtd = new LimitedLiabilityCompany(11, "999-99-99", 2100, "ООО Брест", "223888172727");
            SoleProprietor clientSP = new SoleProprietor(2, "22-33-22", 980, "Иван Михайлович", "28-01-1970");

            Console.WriteLine(clientSP.ClientInformation);
            Console.WriteLine(clientLtd.ClientInformation);
        }
    }
}
