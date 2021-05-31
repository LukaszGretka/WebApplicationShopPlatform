
using System;

namespace WebApplicationShopPlatform.Catalog.Models
{
    public class DatabaseActionResult<T> where T: class
    {
        internal bool Success { get; private set; }

        internal string Message { get; private set; }

        internal T Obj { get; private set; }

        internal Exception Exception { get; private set; }

        public DatabaseActionResult(bool success, string message = "", T obj = null, Exception exception = null)
        {
            Success = success;
            Message = message;
            Obj = obj;
            Exception = exception;
        }
    }
}
