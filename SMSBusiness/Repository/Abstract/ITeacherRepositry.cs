using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface ITeacherRepositry
    {
        List<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int TeacherId);
        int InsertUpdateTeacher(Teacher teacher);
        int DeleteTeacher(Teacher teacher);
        /// <summary>
        /// Teacher Address Repositry
        /// </summary>
        /// <returns></returns>
        List<TeacherAddress> GetAllTeacherAddress();
        TeacherAddress GetTeacherAddressByTeacherId(int TeacherId);
        int InsertUpdateTAddress(TeacherAddress tAddress);
        int DeleteTeacher(TeacherAddress tAddress);

        /// <summary>
        /// Teacher Contact Repositry interface
        /// </summary>
        /// <returns></returns>
        List<TeacherContact> GetAllTeacherContact();
        TeacherContact GetTContactsById(int TeacherId);
        int InsertUpdateTContact(TeacherContact tContact);
        int DeleteContact(TeacherContact tContact);

        /// <summary>
        /// Teacher Profile Repositry interface
        /// </summary>
        /// <returns></returns>
        List<TeacherProfile> GetAllTeacherProfile();
        TeacherProfile GetTProfileById(int TeacherId);
        int InsertUpdateTProfile(TeacherProfile tProfile);
        int DeleteProfile(TeacherProfile tProfile);
    }
}
