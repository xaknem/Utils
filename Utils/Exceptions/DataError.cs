using System;

namespace Utils.Exceptions
{
    public class DataError : Exception, IUtilError 
    {
        
        public void Throw()
        {
            throw new Exception();
        }
    }
}