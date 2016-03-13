using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class GuardianContactBLL : IGuardianContact
    {
        public GuardianContacts GetGuardianContactInfoByGuardianId(int GuardianId)
        {
            var objgConatactsDao = new GuardianContactDAO(new SqlDatabase());
            DataTable stdgContactDetail;
            GuardianContacts gContact = new GuardianContacts();
            try
            {
                stdgContactDetail = objgConatactsDao.GetGuardianContactInfoByGuardianId(GuardianId);
                if (stdgContactDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdgContactDetail.Rows)
                    {
                        gContact.GuardianContactId = int.Parse(item["GuardianContactId"].ToString());
                        gContact.GuardianId = int.Parse(item["GuardianId"].ToString());
                        gContact.FirstContact = item["Contact1"].ToString();
                        gContact.SecondContact = item["Contact2"].ToString();


                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return gContact;



        }

        public int GuardianContactAddChanges(GuardianContacts gContact)
        {
            var objgContactDao = new GuardianContactDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objgContactDao.InsertUpdateGuardianContact(gContact);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }






    }
}
