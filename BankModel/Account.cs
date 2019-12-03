using System;

namespace BankModel
{
    /// <summary>
    /// Тип депозита с простыми или сложными процентами
    /// </summary>
    public enum DepositType { Simple, Complited }

    /// <summary>
    /// Делегат операций по начислениям и списаниям
    /// </summary>
    /// <param name="a">сумма по депозиту</param>
    /// <param name="b">сумма по кредиту</param>
    public delegate void AccauntHandler(double a, double b);

    /// <summary>
    /// банковский аккаунт база
    /// </summary>
    public abstract class Account
    {
        /// <summary>
        /// Наблюдение за балансом(лог)
        /// </summary>
        public Action<string> BalanceObserver;


        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int ID { get; protected set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Начальная сумма вклада
        /// </summary>
        public double Deposit { get; protected set; }

        /// <summary>
        /// Баланс счета
        /// </summary>
        double balance;

        /// <summary>
        /// Баланс на депозите
        /// </summary>
        public double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                this.balance = value;
                //отслеживание изменений в балансе
                BalanceObserver?.Invoke($"{DateTime.Now} Баланс ID{ID} имя {Name} изменен до {value}");
            }
        }

        /// <summary>
        /// Процентная ставка по депозиту
        /// </summary>
        public double DepositRate { get; set; }

        /// <summary>
        /// Тип депозита
        /// </summary>
        public DepositType DepType { get; set; }

        /// <summary>
        /// Список кредитов
        /// </summary>
        public abstract CreditList<Credit> credits { get; set; }

        /// <summary>
        /// Получение информации  о депозите в виде строки
        /// </summary>
        /// <returns></returns>
        public string GetDepositInfo()
        {
            return $"Депозит: {Deposit:00.0000} Баланс {Balance:0.0000} со ставкой {DepositRate * 100} %";
        }
        /// <summary>
        /// Инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public abstract string GetInfo();

        /// <summary>
        /// Вычитает проценты по кредитам за счет депозита
        /// </summary>
        public abstract void TakeTaxesByBalance();

        /// <summary>
        /// начисление дивидентов
        /// </summary>
        public abstract void AddDividend();

        /// <summary>
        /// добавление кредита к аккаунту
        /// </summary>
        /// <param name="newCredit">новый кредит</param>
        public abstract void AddCredit(Credit newCredit);

    }
}
