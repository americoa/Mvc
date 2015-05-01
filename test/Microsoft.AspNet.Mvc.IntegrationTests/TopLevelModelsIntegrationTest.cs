// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using Xunit;

namespace Microsoft.AspNet.Mvc.IntegrationTests
{
    public class TopLevelModelIntegrationTest
    {
        private class PersonController
        {
            public Address Address { get; set; }
        }

        private class Address
        {
            public string Street { get; set; }
        }

        [Fact(Skip = "Extra entries in model state dictionary. #2466")]
        public async Task ControllerProperty_MutableComplexObject_GetsBound()
        {
            // Arrange
            var argumentBinder = ModelBindingTestHelper.GetArgumentBinder();
            var parameter = new ParameterDescriptor()
            {
                Name = "Address",
                BindingInfo = new BindingInfo()
                {
                    BindingSource = BindingSource.ModelBinding
                },

                ParameterType = typeof(Address)
            };

            var operationContext = ModelBindingTestHelper.GetOperationBindingContext(request =>
            {
                request.QueryString = new QueryString("Address.Street", "SomeStreet");
            });

            var modelState = new ModelStateDictionary();

            // Act
            var modelBindingResult = await argumentBinder.BindModelAsync(parameter, modelState, operationContext);

            // Assert

            // ModelBindingResult
            Assert.NotNull(modelBindingResult);
            Assert.True(modelBindingResult.IsModelSet);

            // Model
            var boundAddress = Assert.IsType<Address>(modelBindingResult.Model);
            Assert.NotNull(boundAddress);
            Assert.Equal(1, boundPerson.Address.Zip);

            // ModelState
            Assert.True(modelState.IsValid);

            Assert.Equal(1, modelState.Keys.Count);
            var key = Assert.Single(modelState.Keys, k => k == "CustomParameter.Address.Street");
            Assert.NotNull(modelState[key].Value);
            Assert.Equal("1", modelState[key].Value.AttemptedValue);
            Assert.Equal(1, modelState[key].Value.RawValue);
            Assert.NotNull(modelState[key].Value);
            Assert.Empty(modelState[key].Errors);
            Assert.Equal(ModelValidationState.Valid, modelState[key].ValidationState);
        }


        private class PersonController2
        {
            public Address Address { get; }
        }

        [Fact]
        public async Task ControllerProperty_ImmutableComplexObject_GetsBound()
        {
        }

        private class PersonController3
        {
            public int Id { get; set; }
        }

        [Fact]
        public async Task ControllerProperty_ValueType_GetsBound()
        {
        }

        private class PersonController4
        {
            public ICollection<Address> Address { get; set; }
        }

        [Fact]
        public async Task ControllerProperty_SettableCollection_GetsBound()
        {
        }

        private class PersonController5
        {
            // Set IsReadonly to true.
            public SortedSet<int> ChildIds { get; }
        }

        [Fact]
        public async Task ControllerProperty_ReadOnlyCollection_GetsBound()
        {
        }

        private class PersonController6
        {
            public ICollection<Address> Address { get; }
        }

        [Fact]
        public async Task ControllerProperty_ReadOnlyCollection_NoSetter_GetsBound()
        {
        }

        [Fact]
        public async Task ControllerProperty_Array_GetsBound()
        {
        }

        [Fact]
        public async Task ControllerProperty_Array_NoSetter_GetsBound()
        {
        }
    }
}