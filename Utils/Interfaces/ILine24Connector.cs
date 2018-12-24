using System.Collections.Generic;

namespace Utils.Interfaces
{
    public interface ILine24Connector<T> where T : class
    {
        ICollection<T> GetMissedCalls();
        ICollection<T> GetOutgoingCallsByNumber(string number);
        ICollection<T> GetIncomingCallsByNumber(string number);
    }
}