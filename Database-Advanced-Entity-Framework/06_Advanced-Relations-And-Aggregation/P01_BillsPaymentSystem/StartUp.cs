using System;
using System.Globalization;
using System.Linq;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using Type = P01_BillsPaymentSystem.Data.Models.Type;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();

            Seed(db);

            var userId = int.Parse(Console.ReadLine());

            var user = db.Users
                     .Where(u => u.UserId == userId)
                     .Select(u => new
                     {
                         Name = $"{u.FirstName} {u.LastName}",

                         BankAccounts = u.PaymentMethods
                         .Where(pm => pm.Type == Type.BankAccount)
                         .Select(pm => pm.BankAccount).ToList(),

                         CreditCards = u.PaymentMethods
                         .Where(pm => pm.Type == Type.CreditCard)
                         .Select(pm => pm.CreditCard).ToList()
                     }).FirstOrDefault();

            if (!db.Users.Any(u => u.UserId == userId))
            {
                Console.WriteLine($"User with id {userId} not found!");
                return;
            }

            Console.WriteLine($"User: {user.Name}");

            var bankAccounts = user.BankAccounts;

            if (bankAccounts.Any())
            {
                Console.WriteLine("Bank Accounts:");

                foreach (var ba in bankAccounts)
                {
                    Console.WriteLine($"-- ID: {ba.BankAccountId}");
                    Console.WriteLine($"--- Balance: {ba.Balance:f2}");
                    Console.WriteLine($"--- Bank: {ba.BankName}");
                    Console.WriteLine($"--- SWIFT: {ba.SwiftCode}");
                }
            }

            var creditCards = user.CreditCards;

            if (creditCards.Any())
            {
                Console.WriteLine("Credit Cards:");

                foreach (var cc in creditCards)
                {
                    Console.WriteLine($"-- ID: {cc.CreditCardId}");
                    Console.WriteLine($"--- Limit: {cc.Limit:f2}");
                    Console.WriteLine($"--- Limit Left: {cc.LimitLeft:f2}");
                    Console.WriteLine($"--- Expiration Date: {cc.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                }
            }
        }

        private static void Seed(BillsPaymentSystemContext db)
        {
            using (db)
            {
                var user = new User()
                {
                    FirstName = "Pesho",
                    LastName = "Petrov",
                    Email = "peshkata@mail.bg",
                    Password = "peshkata"
                };

                var creditCards = new CreditCard[]
                    {
                         new CreditCard
                         {
                            ExpirationDate =  DateTime.ParseExact("09/03/2005", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Limit = 20000m,
                            MoneyOwed = 1000m
                         },

                        new CreditCard
                        {
                            ExpirationDate =  DateTime.ParseExact("21/01/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Limit = 1000m,
                            MoneyOwed = 200m
                        }

                    };

                var bankAccount = new BankAccount()
                {
                    Balance = 1600m,
                    BankName = "UNICREDIT",
                    SwiftCode = "UNIC"
                };

                var paymentMethods = new[]
                    {
                        new PaymentMethod
                        {
                            User = user,
                            CreditCard = creditCards[0],
                            Type = Type.CreditCard
                        },

                        new PaymentMethod
                        {
                            User = user,
                            CreditCard = creditCards[1],
                            Type = Type.CreditCard
                        },

                        new PaymentMethod
                        {
                            User = user,
                            BankAccount = bankAccount,
                            Type = Type.BankAccount
                        }
                    };

                db.Users.Add(user);
                db.CreditCards.AddRange(creditCards);
                db.BankAccounts.Add(bankAccount);
                db.PaymentMethods.AddRange(paymentMethods);

                db.SaveChanges();
            }
        }
    }
}
