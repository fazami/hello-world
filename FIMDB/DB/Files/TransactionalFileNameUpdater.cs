using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace FIMDB.DB.Files
{
    public class TransactionalFileNameUpdater : IEnlistmentNotification
    {
        private string _oldMovieFullFileName;
        private string _newMovieFullFileName;
        bool _success = false;

        public TransactionalFileNameUpdater(string oldMovieFullFileName, string newMovieFullFileName)
        {
            _oldMovieFullFileName = oldMovieFullFileName;
            _newMovieFullFileName = newMovieFullFileName;
            Transaction.Current.EnlistVolatile(this, EnlistmentOptions.None);
        }
        public void Commit(Enlistment enlistment)
        {
            File.Move(_oldMovieFullFileName, _newMovieFullFileName);
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
                File.Move(_newMovieFullFileName, _oldMovieFullFileName);
            }
            enlistment.Done();
        }
    }
}
