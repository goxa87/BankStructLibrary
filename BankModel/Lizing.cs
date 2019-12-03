using System;
using System.Diagnostics;

namespace BankModel
{
    /// <summary>
    /// класс для кредита для юр лиц - лизинга
    /// </summary>
    public class Lizing : Credit
    {
        /// <summary>
        /// Процент возмещения по налоговой базе (устанавливается статически)
        /// </summary>
        public double TaxComp { get; }

        /// <summary>
        /// BИнициализация
        /// </summary>
        /// <param name="sum">Сумма</param>
        /// <param name="tax">процентная ставка</param>
        /// <param name="term">дата создания</param>
        /// <param name="terget">цель кредита</param>
        /// <param name="compense">Налоговая компенсация</param>
        public Lizing(double sum, double tax, DateTime term, string target, double compense)
        {
            CreditSum = sum;
            Tax = tax;
            LoanTerm = term;
            CreditTarget = target;
            TaxComp = compense;
        }

        /// <summary>
        /// Возвращает начисленный процент
        /// </summary>
        /// <param name="sum">База для начисления</param>
        /// <returns></returns>
        public override double AddInterest()
        {
            //вычтиает компенсацию
            if ((CreditSum * Tax / 100) < TaxComp)
            {
                Debug.WriteLine($"Списано 0");
                return 0;

            }
            else
            {
                Debug.WriteLine($"Списано {((CreditSum * Tax) / 100) - TaxComp}");
                return ((CreditSum * Tax) / 100) - TaxComp;

            }
        }

        /// <summary>
        /// Получить инфо в виде строки
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return $"Лизинг:{CreditSum} под {Tax}%, на {CreditTarget}. Сроком до{LoanTerm.ToString("dd.MM.yyyy")} с возмещением {TaxComp}";
        }
    }
}
