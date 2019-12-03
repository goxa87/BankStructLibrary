using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BankModel
{
    public class CreditList<T> : IEnumerable
       where T : Credit
    {
        /// <summary>
        /// порядковый индексатор
        /// </summary>
        /// <param name="index">номер</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return Ts[index];
            }
            set
            {
                Ts[index] = value;
            }
        }

        public ObservableCollection<T> Ts { get; set; }

        /// <summary>
        /// инициализация
        /// </summary>
        public CreditList()
        {
            Ts = new ObservableCollection<T>();
        }

        /// <summary>
        /// Возвращает количество элементов
        /// </summary>
        public int Count
        {
            get
            {
                return Ts.Count;
            }
        }

        /// <summary>
        /// Возвращает итератор к коллекции кредитов
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var e in Ts)
                yield return e;
        }

        /// <summary>
        /// Добавление кредита
        /// </summary>
        /// <param name="newT"></param>
        public void Add(T newT)
        {
            Ts.Add(newT);
        }
    }
}
