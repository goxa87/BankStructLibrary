using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    /// <summary>
    /// Класс пердставляет общую схему кредитования
    /// </summary>
    public abstract class Credit : ICredit<Credit>
    {
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public double CreditSum { get; set; }

        /// <summary>
        /// Процент по кредиту (можно менять)
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        /// Срок выдачи кредита
        /// </summary>
        public DateTime LoanTerm { get; set; }

        /// <summary>
        /// Цель кредитования
        /// </summary>
        public string CreditTarget { get; set; }

        /// <summary>
        /// возвращает процент за период
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public abstract double AddInterest();

        /// <summary>
        /// Установить процентную ставку
        /// </summary>
        /// <param name="value"></param>
        public void SetTax(double value)
        {
            this.Tax = value;
        }

        /// <summary>
        /// Получить инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public abstract string GetInfo();
    }
}
