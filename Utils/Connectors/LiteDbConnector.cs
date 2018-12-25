using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Utils.Entities;
using Utils.Interfaces;

namespace Utils.Connectors
{
    public class LiteDbConnector : IRepositoryAsync<Call>, IDisposable
    {
        public List<Call> Last250CallsFromDatabase { get; private set; }
        private readonly LiteDatabase _db = new LiteDatabase(@"line24.db");
        private int CurrentDbSize { get; set; }
        public LiteCollection<Call> LastStateFromDatabase { get; set; }

        /// <summary>
        /// При инициализации получает данные из line24.db. Если не находит базу - создаёт новую.
        /// </summary>
        public LiteDbConnector()
        {
            LastStateFromDatabase = _db.GetCollection<Call>("missed_calls");
            CurrentDbSize = LastStateFromDatabase.Count();
            Last250CallsFromDatabase = LastStateFromDatabase
                .FindAll()
                .OrderByDescending(x => x.Id).Take(250)
                .ToList();
            CurrentDbSize = Last250CallsFromDatabase.Count;
        }

        /// <summary>
        /// Добавляет отсутствующие данные в базу.
        /// </summary>
        /// <param name="callsFromLine24">Список звонков для добавления.</param>
        /// <returns>Возвращает true если что-то удалось добавить.</returns>
        public bool TryToUpdateCalls(List<Call> callsFromLine24)
        {
            var exceptionResult = CheckNewMissedCalls(callsFromLine24);
            if (exceptionResult.Count <= 0) return false;
            LastStateFromDatabase.InsertBulk(exceptionResult);
            UpdateAll();
            return true;
        }

        /// <summary>
        /// Изменяет статус звонка на "Обработанный" и обновляет элемент в базе.
        /// </summary>
        /// <param name="existedCall">Звонок, который необходимо обновить в базе</param>
        public void UpdateCallWithNewStatus(Call existedCall)
        {
            existedCall.Status = true;
            LastStateFromDatabase.Update(existedCall);
            UpdateAll();
        }

        /// <summary>
        /// Изменяет статус звонка на "Обработанный", добавляет комментарий и обновляет элемент в базе.
        /// </summary>
        /// <param name="existedCall">Звонок, который необходимо обновить в базе</param>
        /// <param name="comment">Комментарий, который необходимо добавить звонку</param>
        public void UpdateCallWithNewStatusAndCommentWithSla(Call existedCall, string comment, string sla)
        {
            existedCall.Status = true;
            existedCall.Comment = comment;
            existedCall.Sla = sla;
            LastStateFromDatabase.Update(existedCall);
            UpdateAll();
        }

        /// <summary>
        /// Изменяет статус звонков на "Обработанный" и обновляет элемент в базе.
        /// </summary>
        /// <param name="existedCalls">Звонки, которые необходимо обновить в базе</param>
        public void UpdateCallWithNewStatus(IEnumerable<Call> existedCalls)
        {
            foreach (var call in existedCalls)
            {
                UpdateCallWithNewStatus(call);
            }
        }
        
        /// <summary>
        /// Проверяет начилие элементов коллекции в базе
        /// </summary>
        /// <param name="callsFromLine24Page">Новые элементы, наличие которыех необходимо проверить</param>
        /// <returns>Возвращает то, чего нет в последних 250 элементах базы</returns>
        private List<Call> CheckNewMissedCalls(List<Call> callsFromLine24Page)
        {
            var result = callsFromLine24Page
                .Except(Last250CallsFromDatabase, new CallComparer())
                .ToList();
            return result;
        }

        /// <summary>
        /// Забирает обновления из базы
        /// </summary>
        private void UpdateAll()
        {
            LastStateFromDatabase = _db.GetCollection<Call>("missed_calls");
            CurrentDbSize = LastStateFromDatabase.Count();
            Last250CallsFromDatabase = LastStateFromDatabase.FindAll().OrderByDescending(x => x.Id).Take(250)
                .ToList();
            CurrentDbSize = Last250CallsFromDatabase.Count;
        }

        /// <summary>
        /// Компаратор для класса Call
        /// </summary>
        public class CallComparer : IEqualityComparer<Call>
        {
            public bool Equals(Call x, Call y)
            {
                Debug.Assert(x != null, nameof(x) + " != null");
                Debug.Assert(y != null, nameof(y) + " != null");
                if (x.Phone.Trim().Equals(y.Phone.Trim()))
                {
                    if (x.Date.Trim().Equals(y.Date.Trim()))
                    {
                        return true;
                    }
                }
                return false;
            }
            public int GetHashCode(Call obj)
            {
                return obj.Date.ToString().GetHashCode();
            }
        }

        public Call Get(Func<Call, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Call>> GetManyAsync(Func<Call, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Call item)
        {
            throw new NotImplementedException();
        }

        public void AddMany(ICollection<Call> items)
        {
            throw new NotImplementedException();
        }

        public void Edit(Call item)
        {
            throw new NotImplementedException();
        }

        public void Drop(Call item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}