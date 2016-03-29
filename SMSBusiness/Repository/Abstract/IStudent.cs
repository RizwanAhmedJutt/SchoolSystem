using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IStudent
    {
        List<Student> GetAllStudents();
        List<Student> GetALLDisActiveStudents();
        List<Student> GetAllStudentByName();
        Student GetStudentById(int StudentId);
        int StudentAddChanges(Student student);
        int DeleteStudent(Student student);

    }
}
