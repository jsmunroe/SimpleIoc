using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc
{
    public class ContainerException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_message">Exception message.</param>
        public ContainerException(string a_message)
            : base(a_message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_message">Exception message.</param>
        /// <param name="a_innerException">Inner exception.</param>
        public ContainerException(string a_message, Exception a_innerException)
            : base(a_message, a_innerException)
        {

        }
    }

}
