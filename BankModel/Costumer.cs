using System;
using System.Diagnostics;

namespace BankModel
{
    /// <summary>
    /// Описывает потребительский кредит
    /// </summary>
    public class Costumer : Credit
    {
        /// <summary>
        /// Возвращает начисленный процент в этом периоде по кредиту
        /// </summary>
        /// <param name="sum">База для начисления</param>
        /// <returns></returns>
        public override double AddInterest()
        {
            Debug.WriteLine($"Списано {CreditSum * Tax / 100}");
            return CreditSum * Tax / 100;
        }

        public override string GetInfo()
        {
            return $"Потр кредит :{CreditSum} под {Tax}%, на {CreditTarget}. Сроком до{LoanTerm.ToString("dd.MM.yyyy")}";
        }

        /// <summary>
        /// инициализация кредита
        /// </summary>
        /// <param name="sum">Сумма</param>
        /// <param name="tax">процентная ставка</param>
        /// <param name="term">дата создания</param>
        /// <param name="terget">цель кредита</param>
        public Costumer(double sum, double tax, DateTime term, string target)
        {
            CreditSum = sum;
            Tax = tax;
            LoanTerm = term;
            CreditTarget = target;
        }
    }    
}
