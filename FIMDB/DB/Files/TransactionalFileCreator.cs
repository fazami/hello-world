using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace FIMDB.DB.Files
{
    public class TransactionalFileCreator : IEnlistmentNotification
    {
        private string _fileName;
        private byte[] _fileBytes;
        private bool _success = false;

        public TransactionalFileCreator(string fileName, byte[] fileBytes)
        {
            _fileName = fileName;
            _fileBytes = fileBytes;
            Transaction.Current.EnlistVolatile(this, EnlistmentOptions.None);
        }
        public void Commit(Enlistment enlistment)
        {
            File.WriteAllBytes(_fileName, _fileBytes);
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
                File.Delete(_fileName);
            }
            enlistment.Done();
        }
    }
}
