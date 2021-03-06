﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Validations.Exceptions;
using Validations.Impl;
using Validations.Test.Domain;

namespace Validations.Test
{
    [TestFixture]
    public class OperationsTest
    {
        [Test]
        [Category("MakeSimpleOperation")]
        public void MakeSimpleOperationTest()
        {
            string target = "Nome";
            string description = "Description error";

            var operation = new SimpleOperation<Salesman>(target, description, salesman => salesman.Name != null);

            Assert.IsNotNull(operation);
            Assert.IsTrue(operation.Target.Equals(target));
            Assert.IsTrue(operation.MessageOnError.Equals(description));
        }

        [Test]
        [Category("MakeSimpleOperation")]
        public void MakeSimpleOperationTest2()
        {
            string target = "Nome";

            var operation = new SimpleOperation<Salesman>(target, null, salesman => salesman.Name != null);

            Assert.IsNotNull(operation);
            Assert.IsTrue(operation.Target.Equals(target));
            Assert.IsTrue(operation.MessageOnError != null);
            Assert.IsNotEmpty(operation.MessageOnError);
        }


        [Test]
        [Category("MakeComplexOperation")]
        public void MakeComplexOperationTest()
        {
            string target = "Nome";
            string description = "Description error";
            var operation = new ComplexOperation<Salesman, Agency>(target, description, (salesman, agency) => salesman.Name != null && agency.Name != salesman.Name);

            Assert.IsNotNull(operation);
            Assert.IsTrue(operation.Target.Equals(target));
            Assert.IsTrue(operation.MessageOnError.Equals(description));
        }

        [Test]
        [Category("MakeComplexOperation")]
        public void MakeComplexOperationTest2()
        {
            string target = "Nome";
            var operation = new ComplexOperation<Salesman, Agency>(target, "    ", (salesman, agency) => salesman.Name != null && agency.Name != salesman.Name);

            Assert.IsNotNull(operation);
            Assert.IsTrue(operation.Target.Equals(target));
            Assert.IsTrue(operation.MessageOnError != null);
            Assert.IsNotEmpty(operation.MessageOnError);
            
        }

        [Test]
        [Category("MakeComplexOperation")]
        [ExpectedException(typeof(OnBuildingOperationException))]
        public void WrongCustomOperationResultTest()
        {
            // runtimeError state cannot be passed into constructor,
            // you need to pass an exception if you intend to indicate an exception on constructor overloading.
            new CustomOperationResult("my target", "my message", ResultState.RuntimeError);
        }

        [Test]
        [Category("MakeSimpleOperation")]
        [ExpectedException(typeof(OnBuildingOperationException))]
        public void FailedMakeSimpleOperationTest()
        {
            // target cannot be null or empty..
            new SimpleOperation<Salesman>(null, "descr", salesman => salesman.Name != null);
        }

    }
}
