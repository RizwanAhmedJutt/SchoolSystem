﻿using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface ITeacherRepositry
    {
        List<Teacher> GetAllStudents();
        Teacher GetTeacherById(int TeacherId);
        int InsertUpdateTeacher(Teacher teacher);
        int DeleteTeacher(Teacher teacher);
    }
}
