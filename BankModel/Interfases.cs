using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    /// <summary>
    /// методы для всех кредитов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ICredit<T>
        where T : Credit
    {
        /// <summary>
        /// Переопределить процентную ставку по кредиту
        /// </summary>
        /// <param name="value"></param>
        void SetTax(double value);
        /// <summary>
        /// Начислить проценты по кредиту
        /// </summary>
        /// <param name="tax"></param>
        double AddInterest();
        /// <summary>
        /// Получить информацию в виде строки
        /// </summary>
        /// <returns></returns>
        string GetInfo();
    }
}
