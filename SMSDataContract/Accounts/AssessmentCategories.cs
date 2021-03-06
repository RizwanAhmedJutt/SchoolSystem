﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
   public class AssessmentCategories
    {

       public AssessmentCategories()
       {
           AssessmentCategoryId = 0;
           AssessmentName = string.Empty;
           CreatedById = string.Empty;
           CreateDate = DateTime.Now;
           ModifiedById = string.Empty;
           ModifiedDate = null;
       }
       [Display(Name = "Assessment Category Id")]
       public int AssessmentCategoryId { get; set; }
       [Display(Name = "Assessment Name")]
       [Required(ErrorMessage = "Please Enter Assessment Name")]
       public string AssessmentName { get; set; }
       public string CreatedById { get; set; }
       public DateTime CreateDate { get; set; }
       public string ModifiedById { get; set; }
       public DateTime? ModifiedDate { get; set; }


    }
}
