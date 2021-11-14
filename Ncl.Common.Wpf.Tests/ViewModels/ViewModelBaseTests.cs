using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ncl.Common.Wpf.ViewModels;
using Xunit;

namespace Ncl.Common.Wpf.Tests.ViewModels
{
    public class ViewModelBaseTests
    {
        [Theory]
        [InlineData("Value", "Value")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void OnPropertyChanged_RaisesEventWithGivenName(string name, string expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            ViewModelBaseMock actualSender = null;
            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (sender, args) =>
            {
                actualSender = sender as ViewModelBaseMock;
                actual = args;
            };

            //Act
            instance.OnPropertyChangedImpl(name);

            //Assert
            Assert.Equal(instance, actualSender);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual.PropertyName);
        }

        /// <summary>
        ///     Mock implementation of <see cref="ViewModelBase" /> to allow unit testing.
        /// </summary>
        private class ViewModelBaseMock : ViewModelBase
        {
            /// <summary>
            ///     Raises the PropertyChanged event for the given property name.
            ///     The calling member's name will be used as the parameter, by default.
            /// </summary>
            /// <param name="name">
            ///     The property's name.
            ///     Can specify <see cref="string.Empty" /> or <see langword="null" /> to
            ///     signal all properties have changed.
            /// </param>
            public void OnPropertyChangedImpl([CallerMemberName] string name = null)
            {
                OnPropertyChanged(name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetImpl<T>(ref T current, T value, [CallerMemberName] string name = null)
            {
                return Set(ref current, value, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetImpl<T>(ref T current, T value, Action onSetAction,
                [CallerMemberName] string name = null)
            {
                return Set(ref current, value, onSetAction, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the
            ///     <see cref="ViewModelBase.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <typeparam name="T">The field's type.</typeparam>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetAndCallFirstImpl<T>(ref T current, T value, Action onSetAction,
                [CallerMemberName] string name = null)
            {
                return SetAndCallFirst(ref current, value, onSetAction, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleImpl(ref double current, double value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleImpl(ref double current, double value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the
            ///     <see cref="ViewModelBase.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleAndCallFirstImpl(ref double current, double value, Action onSetAction,
                int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDoubleAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatImpl(ref float current, float value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatImpl(ref float current, float value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the
            ///     <see cref="ViewModelBase.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatAndCallFirstImpl(ref float current, float value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloatAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleImpl(ref double? current, double? value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleImpl(ref double? current, double? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDouble(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the
            ///     <see cref="ViewModelBase.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetDoubleAndCallFirstImpl(ref double? current, double? value, Action onSetAction,
                int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetDoubleAndCallFirst(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatImpl(ref float? current, float? value, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatImpl(ref float? current, float? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloat(ref current, value, onSetAction, decimals, name);
            }

            /// <summary>
            ///     Sets a field to a given value if its different than the current value.
            ///     Calls the given <see cref="Action" /> when the value is set before raising the
            ///     <see cref="ViewModelBase.PropertyChanged" />
            ///     event.
            /// </summary>
            /// <param name="current">The reference to the field.</param>
            /// <param name="value">The value to set, if different.</param>
            /// <param name="onSetAction">The action to call when the field is set.</param>
            /// <param name="decimals">The number of decimals to use for the comparison. Minimum value allowed is zero (0).</param>
            /// <param name="name">The name of the property.</param>
            /// <returns><see langword="true" /> if the field's value changed, otherwise, <see langword="false" />.</returns>
            public bool SetFloatAndCallFirstImpl(ref float? current, float? value, Action onSetAction, int decimals = 3,
                [CallerMemberName] string name = null)
            {
                return SetFloatAndCallFirst(ref current, value, onSetAction, decimals, name);
            }
        }

        #region Normal Set

        [Fact]
        public void Set_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void Set_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Set1_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void Set1_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 0;
            instance.SetImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Set1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Set1_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 5;
            instance.SetImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_SetsValueWithDifferentValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            int initialValue = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 0;
            instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetAndCallFirst_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 0;
            instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_NoChangeWithSameValue()
        {
            //Arrange
            const int expected = 10;
            int actual = 0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            int initialValue = 10;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetAndCallFirstImpl(ref initialValue, 10, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetAndCallFirst_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            int initialValue = 5;
            instance.SetAndCallFirstImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetAndCallFirst_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            int initialValue = 5;
            instance.SetAndCallFirstImpl(ref initialValue, 5, CallOnSet, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion

        #region Double Set

        [Theory]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble_SetsValueWithDifferentValue(double actual, double expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble_ReturnsTrueWithDifferentValue(double initialValue, double setValue)
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble_RaisesEventWithDifferentValue(double initialValue, double setValue)
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetDoubleImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Theory]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble_NoChangeWithSameValue(double actual, double expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            double initialValue = actual;
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(initialValue, expected);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble_ReturnsFalseWithSameValue(double initialValue, double setValue)
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble_NoEventWithSameValue(double initialValue, double setValue)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetDoubleImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble1_SetsValueWithDifferentValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double initialValue = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 0.0;
            instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDouble1_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 0.0;
            instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_NoChangeWithSameValue()
        {
            //Arrange
            const double expected = 10.0;
            double actual = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double initialValue = 10.5;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble1_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_SetsValueWithDifferentValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double initialValue = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 0.0;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDoubleAndCallFirst_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 0.0;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_NoChangeWithSameValue()
        {
            //Arrange
            const double expected = 10.5;
            double actual = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double initialValue = 10.5;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDoubleAndCallFirst_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double initialValue = 5.5;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 5.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion

        #region Float Set

        [Theory]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat_SetsValueWithDifferentValue(float actual, float expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetFloatImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat_ReturnsTrueWithDifferentValue(float initialValue, float setValue)
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat_RaisesEventWithDifferentValue(float initialValue, float setValue)
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetFloatImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Theory]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat_NoChangeWithSameValue(float actual, float expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            double initialValue = actual;
            instance.SetFloatImpl(ref actual, expected);

            //Assert
            Assert.Equal(initialValue, expected);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat_ReturnsFalseWithSameValue(float initialValue, float setValue)
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat_NoEventWithSameValue(float initialValue, float setValue)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetFloatImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloat1_SetsValueWithDifferentValue()
        {
            //Arrange
            const float expected = 10.5f;
            float actual = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            float initialValue = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float initialValue = 0.0f;
            instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetFloat1_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float initialValue = 0.0f;
            instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat1_NoChangeWithSameValue()
        {
            //Arrange
            const float expected = 10.0f;
            float actual = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            float initialValue = 10.5f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float initialValue = 5.5f;
            instance.SetFloatImpl(ref initialValue, 5.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloat1_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float initialValue = 5.5f;
            instance.SetFloatImpl(ref initialValue, 5.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_SetsValueWithDifferentValue()
        {
            //Arrange
            const float expected = 10.5f;
            float actual = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            float initialValue = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float initialValue = 0.0f;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetFloatAndCallFirst_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float initialValue = 0.0f;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_NoChangeWithSameValue()
        {
            //Arrange
            const float expected = 10.5f;
            float actual = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            float initialValue = 10.5f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float initialValue = 5.5f;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 5.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloatAndCallFirst_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float initialValue = 5.5f;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 5.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion

        #region Nullable Double Set

        [Theory]
        [InlineData(null, 10.5)]
        [InlineData(0.0, null)]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble2_SetsValueWithDifferentValue(double? actual, double? expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, 10.5)]
        [InlineData(0.0, null)]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble2_ReturnsTrueWithDifferentValue(double? initialValue, double? setValue)
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, 10.5)]
        [InlineData(0.0, null)]
        [InlineData(0.0, 10.5)]
        [InlineData(0.0, double.NaN)]
        [InlineData(0.0, double.NegativeInfinity)]
        [InlineData(0.0, double.PositiveInfinity)]
        public void SetDouble2_RaisesEventWithDifferentValue(double? initialValue, double? setValue)
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetDoubleImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble2_NoChangeWithSameValue(double? actual, double? expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            double? initialValue = actual;
            instance.SetDoubleImpl(ref actual, expected);

            //Assert
            Assert.Equal(initialValue, expected);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble2_ReturnsFalseWithSameValue(double? initialValue, double? setValue)
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5, 10.5)]
        [InlineData(double.NaN, double.NaN)]
        [InlineData(double.NegativeInfinity, double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void SetDouble2_NoEventWithSameValue(double? initialValue, double? setValue)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetDoubleImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble3_SetsValueWithDifferentValue()
        {
            //Arrange
            double? expected = 10.5;
            double? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble3_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble3_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDouble3_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double? initialValue = 0.0;
            instance.SetDoubleImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble3_NoChangeWithSameValue()
        {
            //Arrange
            double? expected = null;
            double? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble3_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleImpl(ref initialValue, null, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDouble3_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDouble3_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_SetsValueWithDifferentValue()
        {
            //Arrange
            double? expected = null;
            double? actual = 0.0;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            double? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, 10.5, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_NoChangeWithSameValue()
        {
            //Arrange
            const double expected = 10.5;
            double? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetDoubleAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            double? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetDoubleAndCallFirstImpl(ref initialValue, null, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetDoubleAndCallFirst1_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            double? initialValue = null;
            instance.SetDoubleAndCallFirstImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion

        #region Nullable Float Set

        [Theory]
        [InlineData(null, 10.5f)]
        [InlineData(0.0f, null)]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat2_SetsValueWithDifferentValue(float? actual, float? expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            instance.SetFloatImpl(ref actual, expected);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, 10.5f)]
        [InlineData(0.0f, null)]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat2_ReturnsTrueWithDifferentValue(float? initialValue, float? setValue)
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, 10.5f)]
        [InlineData(0.0f, null)]
        [InlineData(0.0f, 10.5f)]
        [InlineData(0.0f, float.NaN)]
        [InlineData(0.0f, float.NegativeInfinity)]
        [InlineData(0.0f, float.PositiveInfinity)]
        public void SetFloat2_RaisesEventWithDifferentValue(float? initialValue, float? setValue)
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetFloatImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat2_NoChangeWithSameValue(float? actual, float? expected)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            //Act
            float? initialValue = actual;
            instance.SetFloatImpl(ref actual, expected);

            //Assert
            Assert.Equal(initialValue, expected);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat2_ReturnsFalseWithSameValue(float? initialValue, float? setValue)
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, setValue);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(10.5f, 10.5f)]
        [InlineData(float.NaN, float.NaN)]
        [InlineData(float.NegativeInfinity, float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity, float.PositiveInfinity)]
        public void SetFloat2_NoEventWithSameValue(float? initialValue, float? setValue)
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            //Act
            instance.SetFloatImpl(ref initialValue, setValue, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloat3_SetsValueWithDifferentValue()
        {
            //Arrange
            float? expected = 10.5f;
            float? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat3_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            float? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat3_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float? initialValue = null;
            instance.SetFloatImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetFloat3_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float? initialValue = 0.0f;
            instance.SetFloatImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat3_NoChangeWithSameValue()
        {
            //Arrange
            float? expected = null;
            float? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat3_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            float? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatImpl(ref initialValue, null, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloat3_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float? initialValue = null;
            instance.SetFloatImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloat3_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float? initialValue = null;
            instance.SetFloatImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_SetsValueWithDifferentValue()
        {
            //Arrange
            float? expected = null;
            float? actual = 0.0f;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_ReturnsTrueWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            float? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_RaisesEventWithDifferentValue()
        {
            //Arrange
            var expected = new PropertyChangedEventArgs("Value");
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float? initialValue = null;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.PropertyName, actual.PropertyName);
        }

        [Fact]
        public void SetFloatAndCallFirst1_CallActionWithDifferentValue()
        {
            //Arrange
            const bool expected = true;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float? initialValue = null;
            instance.SetFloatAndCallFirstImpl(ref initialValue, 10.5f, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_NoChangeWithSameValue()
        {
            //Arrange
            const float expected = 10.5f;
            float? actual = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            instance.SetFloatAndCallFirstImpl(ref actual, expected, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_ReturnsFalseWithSameValue()
        {
            //Arrange
            const bool expected = false;
            float? initialValue = null;
            var instance = new ViewModelBaseMock();

            static void CallOnSet()
            {
            }

            //Act
            bool actual = instance.SetFloatAndCallFirstImpl(ref initialValue, null, CallOnSet);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void SetFloatAndCallFirst1_NoEventWithSameValue()
        {
            //Arrange
            var instance = new ViewModelBaseMock();

            PropertyChangedEventArgs actual = null;
            instance.PropertyChanged += (_, args) => { actual = args; };

            static void CallOnSet()
            {
            }

            //Act
            float? initialValue = null;
            instance.SetFloatAndCallFirstImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void SetFloatAndCallFirst1_DontCallActionWithSameValue()
        {
            //Arrange
            const bool expected = false;
            var instance = new ViewModelBaseMock();

            bool actual = false;

            void CallOnSet()
            {
                actual = true;
            }

            //Act
            float? initialValue = null;
            instance.SetFloatAndCallFirstImpl(ref initialValue, null, CallOnSet, 3, "Value");

            //Assert
            Assert.Equal(actual, expected);
        }

        #endregion
    }
}