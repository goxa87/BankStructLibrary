using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    /// <summary>
    /// Представляет аккаунт юридического лица
    /// </summary>
    public class Firm : Account
    {
        /// <summary>
        /// юридический адрес
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Событие изменения баланса
        /// </summary>
        public event AccauntHandler BalanceHandler;

        /// <summary>
        /// Наблюдение за изменениями в балансе из методов которые меняют баланс
        /// </summary>
        public Action<string> BalanceMetodChange;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="adress">адрес предприятия</param>
        /// <param name="depozit">первонач депозит</param>
        /// <param name="depRate">процентная ставка</param>
        /// <param name="dt">тип депозита</param>
        public Firm(int id, string name, string adress, double depozit, double depRate, DepositType dt,
            AccauntHandler handler, Action<string> bal)
        {
            ID = id;
            Name = name;
            Adress = adress;
            Deposit = depozit;
            Balance = depozit;
            DepositRate = depRate;
            DepType = dt;
            credits = new CreditList<Credit>();
            BalanceHandler += handler;
            BalanceMetodChange += bal;
        }

        /// <summary>
        /// индексатор для доступа к списку кредитов
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Credit this[int index]
        {
            get
            {
                return credits[index];
            }
            set
            {
                credits[index] = value;
            }
        }

        /// <summary>
        /// Список кредитов
        /// </summary>
        public override CreditList<Credit> credits { get; set; }

        /// <summary>
        /// Инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"{ID} {Name} {Adress} {Balance} {DepType.ToString()} лизингов {credits.Count} шт.";
        }

        /// <summary>
        /// Вычитает проценты по кредитам за счет депозита фирмы
        /// </summary>
        public override void TakeTaxesByBalance()
        {
            foreach (var e in credits)
            {
                double diff = (e as Lizing).AddInterest();
                Balance -= diff;
                BalanceHandler?.Invoke(0, diff);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} уменьшен на {diff}");
            }
        }

        /// <summary>
        /// Начисление простых и сложных процентов на вклад
        /// </summary>
        public override void AddDividend()
        {
            if (DepType == DepositType.Simple)
            {
                //на депозит
                double diff = Deposit * DepositRate / 100;
                Balance += diff;
                BalanceHandler?.Invoke(diff, 0);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} увеличен на {diff}");
            }
            else
            {
                //на баланс
                double diff = Balance * DepositRate / 100;
                Balance += diff;
                BalanceHandler?.Invoke(diff, 0);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} увеличен на {diff}");
            }
        }

        /// <summary>
        /// Добавление лизинга
        /// </summary>
        /// <param name="newCredit"></param>
        public override void AddCredit(Credit newCredit)
        {
            credits.Add(newCredit as Lizing);
        }
    }
}
