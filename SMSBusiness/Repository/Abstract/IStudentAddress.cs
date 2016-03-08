using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IStudentAddress
    {
        StudentAddress GetStudentAddressByStudentId(int StudentId);
        int StudentAddressAddChanges(StudentAddress stdAdd);
    }
}
