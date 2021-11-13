using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ncl.Common.Wpf.ViewModels;
using Xunit;

namespace Ncl.Common.Wpf.Tests.ViewModels
{
    public class DataErrorHandlingViewModelBaseTests
    {
        private const string PropertyName = "testProperty";
        private const string DefaultErrorMessage = "test error";
        
        /// <summary>
        ///     Mock implementation of <see cref="DataErrorHandlingViewModelBase" /> to allow unit testing.
        /// </summary>
        private class DataErrorHandlingViewModelBaseMock : DataErrorHandlingViewModelBase
        {
            /// <summary>
            ///     Adds an error message for a given property.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <param name="errorMessage">The error message to add.</param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public void AddErrorImpl(string propertyName, string errorMessage)
            {
                AddError(propertyName, errorMessage);
            }
            
            /// <summary>
            ///     Adds error messages for a given property.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <param name="errorMessages">The error messages to add.</param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> or <paramref name="errorMessages" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public void AddErrorsImpl(string propertyName, IEnumerable<string> errorMessages)
            {
                AddErrors(propertyName, errorMessages);
            }

            /// <summary>
            ///     Removes an error message for a given property.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <param name="errorMessage">The error message to remove.</param>
            /// <returns>
            ///     <see langword="true" /> if the error message was removed, otherwise, <see langword="false" />.
            /// </returns>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public bool RemoveErrorImpl(string propertyName, string errorMessage)
            {
                return RemoveError(propertyName, errorMessage);
            }

            /// <summary>
            ///     Replaces all the errors for a given property with the new error.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <param name="errorMessage">The error message to replace with.</param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> or <paramref name="errorMessage" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public void ReplaceErrorsImpl(string propertyName, string errorMessage)
            {
                ReplaceErrors(propertyName, errorMessage);
            }

            /// <summary>
            ///     Replaces all the errors for a given property with the new errors.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <param name="errorMessages">The error messages to replace with.</param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> or <paramref name="errorMessages" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public void ReplaceErrorsImpl(string propertyName, IEnumerable<string> errorMessages)
            {
                ReplaceErrors(propertyName, errorMessages);
            }

            /// <summary>
            ///     Clears/Removes all the error message for a given property.
            /// </summary>
            /// <param name="propertyName">The property's name.</param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="propertyName" /> is <see langword="null"/>.
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="propertyName" /> is empty.
            /// </exception>
            public void ClearErrorsImpl(string propertyName)
            {
                ClearErrors(propertyName);
            }

            /// <summary>
            ///     Clears/Removes all the error message for the view model.
            /// </summary>
            public void ClearAllErrorsImpl()
            {
                ClearAllErrors();
            }
        }

        [Fact]
        public void AddError_WithNewError_ShouldAddErrorAndHasErrorsTrue()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            
            //Act
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            
            //Assert
            Assert.True(instance.HasErrors);
        }
        
        [Fact]
        public void AddError_WithNewError_ShouldAddTheError()
        {
            //Arrange
            DataErrorHandlingViewModelBaseMock instance = GetDefaultInstance();
            
            //Act
            instance.AddErrorImpl(PropertyName, DefaultErrorMessage);
            
            //Assert
            IEnumerable errors = instance.GetErrors(PropertyName);
            List<object> collection = errors.Cast<object>().ToList();
            
            Assert.Single(collection);
            Assert.Equal(collection[0], DefaultErrorMessage);
        }

        private static DataErrorHandlingViewModelBaseMock GetDefaultInstance()
        {
            return new DataErrorHandlingViewModelBaseMock();
        }
    }
}