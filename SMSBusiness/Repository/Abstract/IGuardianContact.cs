﻿using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IGuardianContact
    {
      GuardianContacts GetGuardianContactInfoByGuardianId(int GuardianId);
       int GuardianContactAddChanges(GuardianContacts gContact);

    }
}
