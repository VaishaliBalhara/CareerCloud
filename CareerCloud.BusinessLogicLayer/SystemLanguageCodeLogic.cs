using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }
        public virtual SystemLanguageCodePoco Get(string LanguageID)
        {
            return _repository.GetSingle(c => c.LanguageID == LanguageID);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);

        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);

        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SystemLanguageCodePoco item in pocos)
            {
                if (item.LanguageID == null)
                {
                    exceptions.Add(new ValidationException(1000, $"Language ID {item.LanguageID} can not be empty"));
                }

                if (item.Name == null)
                {
                    exceptions.Add(new ValidationException(1001, $"Name {item.Name} can not be empty"));
                }

                if (item.NativeName == null)
                {
                    exceptions.Add(new ValidationException(1002, $"Native Name {item.NativeName} can not be empty"));
                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
