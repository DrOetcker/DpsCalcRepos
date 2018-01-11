using System;
using System.Runtime.Serialization;

namespace Oetcker.ServiceLocation
{
    /// <summary>
    /// Diese Klasse repräsentiert eine Exception die im Contexte des ServiceLocator geworfen wird.
    /// </summary>
    /// <author>Seiler</author> 
    [Serializable]
    public class ServiceLocatorException : Exception
    {
        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <author>Seiler</author> 
        public ServiceLocatorException()
        {
        }

        /// <summary>
        /// Konstruktor mit Exception-Nachricht
        /// </summary>
        /// <author>Seiler</author>  
        public ServiceLocatorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Konstruktor mit Exception-Nachricht und beinhaltetr Exception
        /// </summary>
        /// <author>Seiler</author>  
        public ServiceLocatorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Konstruktor mit SerializationInfo und StreamingContext
        /// </summary>
        /// <author>Seiler</author> 
        protected ServiceLocatorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
