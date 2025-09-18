using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.ViewModels
{
    public class MultiViewModel : BaseViewModel
    {
        public TransactionViewModel TVM { get; }

        public AddTransactionViewModel ATVM { get; }

        public MultiViewModel (TransactionViewModel tvm, AddTransactionViewModel  atvm)
        {
            ATVM = atvm;
            TVM = tvm;
        }

    }
}
