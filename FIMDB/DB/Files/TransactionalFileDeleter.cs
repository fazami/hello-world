using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace FIMDB.DB.Files
{
    public class TransactionalFileDeleter : IEnlistmentNotification
    {
        private string _movieFullFileName;
        bool _success = false;

        public TransactionalFileDeleter(string movieFullFileName)
        {
            _movieFullFileName = movieFullFileName;
            Transaction.Current.EnlistVolatile(this, EnlistmentOptions.None);
        }
        public void Commit(Enlistment enlistment)
        {
            File.Move(_movieFullFileName, _movieFullFileName + ".deleted");
            _success = true;
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment)
        {
            enlistment.Done();
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Rollback(Enlistment enlistment)
        {
            if (_success)
            {
                File.Move(_movieFullFileName + ".deleted", _movieFullFileName);
            }
            enlistment.Done();
        }
    }
}
