using System;
using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.DataLayer.Util
{
    //list of custom comparer per class

    //comparer for Subject class
    public class ComparerSubject : IEqualityComparer<Subject>
    {
       
        public bool Equals(Subject x, Subject y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.SubjectId == y.SubjectId;
        }

        public int GetHashCode(Subject obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashSubjectId = obj.SubjectId.GetHashCode();

            return hashSubjectId;
        }
    }

    public class ComparerStudent : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.StudentId == y.StudentId;
        }

        public int GetHashCode(Student obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashStudentId = obj.StudentId.GetHashCode();
            
            return hashStudentId;
        }
    }
}
