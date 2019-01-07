using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic:BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantSkillPoco item in pocos)
            {

                if (item.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, "Start month can not be greater than 12"));
                }

                if (item.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, "End month can not be greater than 12"));
                }

                if (item.StartYear<1900)
                {
                    exceptions.Add(new ValidationException(103, "Start year can not be less than 1900"));
                }

                if (item.EndYear<item.StartYear)
                {
                    exceptions.Add(new ValidationException(104, "End year can not be less than start year"));
                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
