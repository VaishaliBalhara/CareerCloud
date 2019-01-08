using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {

            public void Add(SystemCountryCodePoco[] pocos)
            {
                Verify(pocos);
                
            }

            public void Update(SystemCountryCodePoco[] pocos)
            {
                Verify(pocos);
                
            }

            protected void Verify(SystemCountryCodePoco[] pocos)
            {
                List<ValidationException> exceptions = new List<ValidationException>();
                foreach (SystemCountryCodePoco item in pocos)
                {
                    if (item.Name == null)
                    {
                        exceptions.Add(new ValidationException(901, $"Name {item.Name} can not be empty"));
                    }

                    if (item.Code == null)
                    {
                        exceptions.Add(new ValidationException(900, $"Code {item.Code} can not be empty"));
                    }

                if (exceptions.Count > 0)
                    {
                        throw new AggregateException(exceptions);
                    }
                }
            }
        }
    }
