using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Impl;
using Validations.Test.Domain;


namespace Validations.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomValidators
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SimpleValidator<Salesman> GetConsultantValidator()
        {
            IList<SimpleOperation<Salesman>> operations = new List<SimpleOperation<Salesman>>();
            operations.Add(new SimpleOperation<Salesman>("Consultant.Name", "Consultant name cannot be null or empty.", n => !string.IsNullOrEmpty(n.Name)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Surname", "Consultant Surname must be title case type.", n => CheckHelper.CheckStringTitleCase(n.Surname)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Email", "Consultant Email has a wrong format.", n => CheckHelper.CheckEmail(n.Email)));
            operations.Add(new SimpleOperation<Salesman>("Consultant.Identifier", "Consultant identifier must be great than 100.", n => CheckHelper.CheckIdentifier(n)));
            operations.Add(new SimpleOperation<Salesman>("Cons.Name <> Cons.Surname", "Consultant name cannot be equals Consultant surname.", n => !n.Name.Equals(n.Surname)));
            operations.Add(new SimpleOperation<Salesman>("Contracts", "Contracts must be referenced", n => n.Contracts.Count() > 0));
            return new SimpleValidator<Salesman>(operations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ComplexValidator<Salesman, Agency> GetDefaultConsAggValidator()
        {
            List<ComplexOperation<Salesman, Agency>> operations = new List<ComplexOperation<Salesman, Agency>>();
            operations.Add(new ComplexOperation<Salesman, Agency>("A.Name <> C.Name", "Agency name cannot be equals to consultant name", (n, c) => !n.Name.Equals(c.Name)));
            operations.Add(new ComplexOperation<Salesman, Agency>("A.Name & C.Name title case", "Name is no title case like Agency name", (n, c) => CheckHelper.CheckStringTitleCase(c.Name) && CheckHelper.CheckIdentifier(n)));
            return new ComplexValidator<Salesman, Agency>(operations);
        }
    }
}
