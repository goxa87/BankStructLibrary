using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    /// <summary>
    /// Представляет клиента физическое лицо
    /// </summary>
    public class Client : Account
    {
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Событие при установлении наблюдения за изменениями в данном аккаунте
        /// </summary>
        /// <param name="s"></param>
        public delegate void ObserverSet(string s);
        /// <summary>
        /// Событие при установлении наблюдения за изменениями в данном аккаунте
        /// </summary>
        public event ObserverSet ObserverString;
        /// <summary>
        /// Событие изменения баланса
        /// </summary>
        public event AccauntHandler balanceHandler;

        /// <summary>
        /// Событие изменения баланса(сеттер вызыват событие)
        /// </summary>
        public event AccauntHandler BalanceHandler
        {
            add
            {
                balanceHandler += value;
                ObserverString?.Invoke($"Установлено наблюдение за аккаунтом ID {ID}/ Метод наблюдения {value.Method.Name}");
            }
            remove
            {
                balanceHandler -= value;
                ObserverString?.Invoke($"Снято наблюдение за аккаунтом ID {ID}/ {value.Method.Name}");
            }
        }

        /// <summary>
        ///  Наблюдение за изменениями в балансе из методов которые меняют баланс 
        /// </summary>
        protected Action<string> BalanceMetodChange;

        /// <summary>
        /// Индексатор для доступа к списку кредитов
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
        /// Инициализация нового физ лица
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="last">Фамилия</param>
        /// <param name="depozit">первонач депозит</param>
        /// <param name="depRate">процентная ставка</param>
        /// <param name="dt">тип депозита</param>
        public Client(int id, string name, string last, double depozit, double depRate, DepositType dt, AccauntHandler handler, Action<string> bal)
        {
            ID = id;
            Name = name;
            LastName = last;
            Deposit = depozit;
            Balance = depozit;
            DepositRate = depRate;
            DepType = dt;
            credits = new CreditList<Credit>();
            BalanceMetodChange += bal;
            //ObserverString += OperationCollector.ObserveNotification; // Это устанавливается здесь, хотя должно по идее в клиентской части
            BalanceHandler += handler;
        }

        /// <summary>
        /// Установка метода наблюдения за изменения в балансе
        /// </summary>
        /// <param name="observer"></param>
        public void SetObserving(ObserverSet observer)
        {
            ObserverString += observer;
        }

        /// <summary>
        /// Инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"{ID} {Name} {LastName} {Balance} {DepType.ToString()} кредитов {credits.Count} шт.";
        }

        /// <summary>
        /// Вычитает проценты по кредитам за счет депозита
        /// </summary>
        public override void TakeTaxesByBalance()
        {
            foreach (var e in credits)
            {
                double diff = (e as Credit).AddInterest();

                Balance -= diff;
                //(this as Account).BalanceOperation?.Invoke();
                //оповещение о списании по кредиту
                balanceHandler?.Invoke(0, diff);
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
                double diff = Deposit * DepositRate / 100;
                Balance += diff;
                //оповещение о Начислении на депозит
                balanceHandler?.Invoke(diff, 0);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} увеличен на {diff}");
            }
            else
            {
                double diff = Balance * DepositRate / 100;
                Balance += diff;
                //оповещение о Начислении на депозит
                balanceHandler?.Invoke(diff, 0);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} увеличен на {diff}");
            }
        }

        /// <summary>
        /// Добавление кредита
        /// </summary>
        /// <param name="newCredit">кредит для добавления</param>
        public override void AddCredit(Credit newCredit)
        {
            credits.Add(newCredit as Costumer);
        }
    }
}
