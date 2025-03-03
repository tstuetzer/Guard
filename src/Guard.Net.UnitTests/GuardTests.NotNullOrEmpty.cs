﻿using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "", null, "[parameter] cannot be Null or empty.")]
        [TestCase(null, " ", null, "[parameter] cannot be Null or empty.")]
        [TestCase(null, null, null, "[parameter] cannot be Null or empty.")]
        [TestCase("", "", null, "[parameter] cannot be Null or empty.")]
        [TestCase("", " ", null, "[parameter] cannot be Null or empty.")]
        [TestCase("", null, null, "[parameter] cannot be Null or empty.")]
        public void NotNullOrEmpty_InvalidInputDefaultException_ThrowsException(
            string input,
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
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrEmpty(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }
        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message")]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null or empty.")]
        [TestCase("", "paramName", "custom error message", "custom error message")]
        [TestCase("", "paramName", null, "[paramName] cannot be Null or empty.")]
        public void NotNullOrEmpty_InvalidInputDefaultExceptionGivenParamName_ThrowsException(
            string input,
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
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrEmpty(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrEmpty_InvalidInputCustomException_ThrowsException()
        {
            string input = null;
            var expectedErrorMessage = "error message";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotNullOrEmpty(input, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrEmpty_InvalidInputNullCustomException_ThrowsException()
        {
            string input = null;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNullOrEmpty(input, exception));
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "custom message")]
        [TestCase("", "custom message")]
        public void NotNullOrEmpty_InvalidNullCustomException2_ThrowsException(string input, string message)
        {
            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotNullOrEmpty<InvalidOperationException>(input));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotNullOrEmpty<InvalidOperationException>(input, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        [TestCase("input")]
        [TestCase(" ")]
        public void NotNullOrEmpty_ValidInput_DoesNotThrowException(string input)
        {
            Should.NotThrow(() => Guard.NotNullOrEmpty(input, nameof(input), null));
        }
    }
}
