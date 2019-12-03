using System;

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    public class Bank : IEnumerable
    {
        /// <summary>
        /// коллекция клиентов
        /// </summary>
        public ObservableCollection<Account> accounts { get; set; }

        /// <summary>
        /// Индексатор, находит по ID аккаунта
        /// </summary>
        /// <param name="index">ID аккаунта</param>
        /// <returns></returns>
        public Account this[int index]//*
        {
            get
            {
                return accounts[index];                
            }
            set
            {
                accounts[index] = value;                
            }
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        public Bank()
        {
            accounts = new ObservableCollection<Account>();
        }//*

        /// <summary>
        /// Добавление аккаунта
        /// </summary>
        /// <param name="newAcc"></param>
        public void Add(Account newAcc) //*
        {
            accounts.Add(newAcc);
        }

        /// <summary>
        /// Энумератор для аккаунтов
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() //*
        {
            //for (int i = 0; i < this.accounts.Count; i++)
            //{
            //    yield return this.accounts[i];
            //}

            foreach (var e in accounts)
            {
                yield return e;
            }
        }

        /// <summary>
        /// находит свободный ID для аккаунта
        /// </summary>
        /// <returns></returns>
        public int GetFreeID() //*
        {
            int n = 1;
            bool flag;  // найден или нет
            while (true)
            {
                flag = false;
                foreach (var e in accounts)
                {
                    if (e.ID == n) //если совпадает, то значение уже есть в базе
                        flag = true;
                }
                if (!flag)
                {
                    return n;
                }
                n++;
            }
        }
        /// <summary>
        /// начисление дивидентов по всем аккаунтам
        /// </summary>
        public void AddDividend()
        {
            foreach (var e in accounts)
                e.AddDividend();
        }

        /// <summary>
        /// вычитает проценты по кредиту с баланса клиента
        /// </summary>
        public void TakeMargeFromBalance()
        {
            foreach (var e in accounts)
            {
                if (e is Client)
                {
                    (e as Client).TakeTaxesByBalance();
                }
                else if (e is VipClient)
                {
                    (e as VipClient).TakeTaxesByBalance();
                }
                else
                {
                    (e as Firm).TakeTaxesByBalance();
                }
            }
        }

        /// <summary>
        /// Возвращает данные в виде массива цифр
        /// </summary>
        /// <returns></returns>
        public double[] GetCommonInfo()
        {
            // 0й общее количество клиентов
            // 1й количество фирм
            // 2й колво частных
            // 3й сумма балансов
            // 4й сумма кредитов
            double[] rez;

            int totCl = 0;
            int firms = 0;
            int clients = 0;
            double depSum = 0;
            double credSum = 0;

            foreach (var e in this.accounts)
            {
                // общее число
                totCl++;
                //фирмы или клиенты
                if (e is Firm)
                    firms++;
                else
                    clients++;
                // депозиты
                depSum += e.Balance;
                //кредиты
                if (e is Client)
                {
                    foreach (var i in (e as Client).credits)
                    {
                        credSum += (i as Costumer).CreditSum;
                    }

                }
                else
                {
                    foreach (var i in (e as Firm).credits)
                    {
                        credSum += (i as Lizing).CreditSum;
                    }
                }

            }

            rez = new double[] { totCl, firms, clients, depSum, credSum };
            return rez;
        }

    }
}
