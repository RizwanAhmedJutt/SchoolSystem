using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IAcadmicClassBLL
    {
        List<AcadmicClass> GetALLAcadmicClassies();
        int AddChangesAcadmicClass(AcadmicClass acadmicClass);
        List<AcadmicClass> GetTeacherAssignedAcadmicClassies(int TeacherId);
    }
}
