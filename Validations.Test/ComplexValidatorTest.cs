using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Repower.Common.Validations.Exceptions;
using Validations.Test.Domain;

namespace Validations.Test
{
    [TestFixture]
    public class ComplexValidatorTest
    {
        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a simple validator, and gets successfully results only.")]
        public void ValidateTest1()
        {
            Salesman cons = MockDataSource.GetRightConsultant();
            Agency ag = MockDataSource.GetRightAgency();

            IComplexValidator<Salesman, Agency> validator = CustomValidators.GetDefaultConsAggValidator();

            var result = validator.Validate(cons, ag);

            Assert.IsTrue(result.All(n => n.State == ResultState.Successfully));
        }

        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a simple validator, and gets unsuccessfully results only.")]
        public void ValidateTest2()
        {
            Salesman cons = MockDataSource.GetWrongConsultant();
            Agency ag = MockDataSource.GetWrongAgency();

            IComplexValidator<Salesman, Agency> validator = CustomValidators.GetDefaultConsAggValidator();

            var result = validator.Validate(cons, ag);

            Assert.IsTrue(result.All(n => n.State == ResultState.Unsuccessfully));
        }

        [Test]
        [Category("ComplexValidator")]
        [Description("Executes a simple validator, and gets a result operation with RuntimeError state.")]
        public void ValidateTest3()
        {

            Salesman cons = new Salesman();
            cons.Name = "";
            cons.Surname = null;
            cons.IdentityCode = 70;
            cons.Email = "chico_herbal-hotmail.com";

            ISimpleValidator<Salesman> validator = CustomValidators.GetConsultantValidator();

            var result = validator.Validate(cons);

            Assert.IsTrue(result.Any(n => n.State == ResultState.RuntimeError));
        }

        [Test]
        [Category("FailedComplexValidator")]
        [Description("Executes a simple validator, and throws an exception because instance to validate is null.")]
        [ExpectedException(typeof(ValidationParameterException))]
        public void FailedValidateTest()
        {
            Salesman cons = null;
            ISimpleValidator<Salesman> validator = CustomValidators.GetConsultantValidator();

            var result = validator.Validate(cons);

            Assert.IsFalse(result.All(n => n.State == ResultState.Unsuccessfully));
        }
    }
}
