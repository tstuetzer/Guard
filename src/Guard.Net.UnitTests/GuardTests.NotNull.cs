﻿using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase("", null, "[parameter] cannot be Null.")]
        [TestCase(" ", null, "[parameter] cannot be Null.")]
        [TestCase(null, null, "[parameter] cannot be Null.")]
        public void NotNull_InvalidInputDefaultExceptionEmptyParamName_ThrowsException(
            string paramName,
            string errorMessage,
            string expectedErrorMessageBase)
        {
            string expectedErrorMessage = null;
#if NET5_0_OR_GREATER
            expectedErrorMessage = expectedErrorMessageBase + ParameterMessageNet50;
#else
            expectedErrorMessage = expectedErrorMessageBase + Environment.NewLine + ParameterMessageNet4X;
#endif
            object input = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNull(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }
        [Test]
        [TestCase("paramName", "custom error message", "custom error message")]
        [TestCase("paramName", null, "[paramName] cannot be Null.")]
        public void NotNull_InvalidInputDefaultExceptionGivenParamName_ThrowsException(
            string paramName,
            string errorMessage,
            string expectedErrorMessageBase)
        {
            string expectedErrorMessage = null;
#if NET5_0_OR_GREATER
            expectedErrorMessage = expectedErrorMessageBase + ParamNameMessageNet50;
#else
            expectedErrorMessage = expectedErrorMessageBase + Environment.NewLine + ParamNameMessageNet4X;
#endif
            object input = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNull(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNull_InvalidInputCustomException_ThrowsException()
        {
            object input = null;
            var expectedErrorMessage = "error message" + ParamNameMessageNet50;
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(() => Guard.NotNull(input, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNull_InvalidNullCustomException_ThrowsException()
        {
            object input = null;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNull(input, exception));
        }

        [Test]
        [TestCase(null)]
        [TestCase("custom message")]
        public void NotNull_InvalidNullCustomException2_ThrowsException(string message)
        {
            object input = null;

            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotNull<object, InvalidOperationException>(input));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotNull<object, InvalidOperationException>(input, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        public void NotNull_ValidInput_DoesNotThrowException()
        {
            var input = new object();

            Should.NotThrow(() => Guard.NotNull(input, nameof(input), null));
        }
    }
}
