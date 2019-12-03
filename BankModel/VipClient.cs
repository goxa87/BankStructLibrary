using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    /// <summary>
    /// Представляет физ лицо )) привелигированного пользователя 
    /// </summary>
    public class VipClient : Client
    {
        /// <summary>
        /// Событие изменения баланса
        /// </summary>
        public event AccauntHandler BalanceHandler;

        /// <summary>
        /// Наблюдение за изменениями в балансе из методов которые меняют баланс
        /// </summary>
        public Action<string> BalanceMetodChange;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Имя</param>
        /// <param name="last">Фамилия</param>
        /// <param name="depozit">первонач депозит</param>
        /// <param name="depRate">процентная ставка</param>
        /// <param name="dt">тип депозита</param>
        /// <param name="depositBonus">процент бонуса на депозит</param>
        /// <param name="creditMinusTax">минус на проценты по кредиту(константа)</param>
        public VipClient(int id, string name, string last, double depozit, double depRate, DepositType dt,
            AccauntHandler handler, Action<string> bal, double depositBonus, double creditMinusTax)
            : base(id, name, last, depozit, depRate, dt, handler, bal)
        {
            DepositBonus = depositBonus;
            CreditMinusTax = creditMinusTax;
        }

        /// <summary>
        /// Специальный бонус на депозит (просто сумма(не процент))
        /// </summary>
        public double DepositBonus { get; set; }

        /// <summary>
        /// минус к ставке по кредиту
        /// </summary>
        public double CreditMinusTax { get; set; }

        /// <summary>
        /// Инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"{ID} VIP {Name} {LastName} {Balance} {DepType.ToString()} кредитов {credits.Count} шт. деп.бонус {DepositBonus}, - к ставке кредита {CreditMinusTax}";
        }

        /// <summary>
        /// Начисление простых процентов и ивип бонуса
        /// </summary>
        public override void AddDividend()
        {
            base.AddDividend();
            Balance += DepositBonus;
            BalanceHandler?.Invoke(DepositBonus, 0);
            BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {this.ID} увеличен на {DepositBonus}");
        }

        /// <summary>
        /// Вычитает проценты по кредитам за счет депозита VIP
        /// </summary>
        public override void TakeTaxesByBalance()
        {
            foreach (var e in credits)
            {
                double diff = (e as Credit).AddInterest();
                Balance -= diff;
                BalanceHandler?.Invoke(0, diff);
                BalanceMetodChange?.Invoke($"{DateTime.Now} Баланс {ID} уменьшен на {diff}");
            }
        }
    }
}
