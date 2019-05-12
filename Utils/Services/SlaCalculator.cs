using System;
using System.Collections;
using LiteDB;

namespace Utils.Services
{
    public class SlaCalculator
    {
        private Sla GetSla(IEnumerable _db, DateTime _gridDate)
        {
            var timeCondition = new TimeSpan(0, 15, 0);
            var gridCalls = _db.Where(x => x.Date.Contains(_gridDate)).ToList();
            var callsWithoutSberAndKorus = gridCalls
                .Where(x => !x.Phone.Contains("84996731111"))
                .Where(x => !x.Phone.Contains("84996731077"))
                .Where(x => !x.Phone.Contains("88126102600"))
                .Where(x => !x.Phone.Contains("3343812"))
                .Where(x => !x.Phone.Contains("84996731009"))
                .Where(x => DateTime.Parse(x.Date).TimeOfDay > DateTime.Parse("08:59").TimeOfDay)
                .Where(x => DateTime.Parse(x.Date).TimeOfDay < DateTime.Parse("18:00").TimeOfDay)
                .Where(x => x.Phone.Length == 11).ToList();
            var countForSla = callsWithoutSberAndKorus.Count;
            var fixedSla = callsWithoutSberAndKorus.Where(x => x.Sla?.Length > 0);
            foreach (var call in fixedSla)
            {
                if (call.DelaySpan == null)
                {
                    call.DelaySpan = "00:01:01";
                }
            }
            fixedSla = fixedSla.Where(x =>
                DateTime.Parse(x.Sla).TimeOfDay - (DateTime.Parse(x.Date).TimeOfDay + TimeSpan.Parse(x.DelaySpan)) <
                timeCondition).ToList();

            var sla = fixedSla.Count();
            if (sla == 0)
            {
                return new Sla();
            }

            var correctSla = Convert.ToDecimal(sla) / Convert.ToDecimal(countForSla) * Convert.ToDecimal(100);
            var slaNot = 100 - Convert.ToInt32(correctSla);
            return new Sla
            {
                SlaOk = Convert.ToInt32(correctSla),
                SlaNot = slaNot
            };
        }

        private class Sla
        {
            public int SlaOk { get; set; } 
            public int SlaNot { get; set; }
        }

    }
}