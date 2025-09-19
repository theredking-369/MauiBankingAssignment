using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.Exceptions
{
    public class BankApiFailedException : Exception
    {
        public BankApiFailedException(string message)
            : base(message)
        {
        }
    }
}
