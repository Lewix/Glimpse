﻿using System;
using System.Collections.Generic;
using Glimpse.Core2.SerializationConverter;
using Xunit;
using Xunit.Extensions;

namespace Glimpse.Test.Core2.SerializationConverter
{
    public class CSharpTypeConverterShould
    {
        [Theory]
        [InlineData(typeof(int), "int")]
        [InlineData(typeof(uint), "uint")]
        [InlineData(typeof(bool), "bool")]
        [InlineData(typeof(float), "float")]
        [InlineData(typeof(string), "string")]
        [InlineData(typeof(object), "object")]
        [InlineData(typeof(double), "double")]
        [InlineData(typeof(decimal), "decimal")]
        [InlineData(typeof(long), "long")]
        [InlineData(typeof(char), "char")]
        [InlineData(typeof(sbyte), "sbyte")]
        [InlineData(typeof(byte), "byte")]
        [InlineData(typeof(long), "long")]
        [InlineData(typeof(ulong), "ulong")]
        [InlineData(typeof(short), "short")]
        [InlineData(typeof(ushort), "ushort")]
        [InlineData(typeof(DateTime), "DateTime")]
        [InlineData(typeof(CSharpTypeConverterShould), "CSharpTypeConverterShould")]
        //Converter supports N levels of generics
        [InlineData(typeof(IDictionary<string, object>), "IDictionary<string, object>")]
        [InlineData(typeof(IDictionary<string, List<int>>), "IDictionary<string, List<int>>")]
        [InlineData(typeof(IDictionary<string, IDictionary<int, IEnumerable<CSharpTypeConverterShould>>>), "IDictionary<string, IDictionary<int, IEnumerable<CSharpTypeConverterShould>>>")]
        public void ConvertToDisplayString(Type intput, string output)
        {
            var converter = new CSharpTypeConverter();
            var result = converter.Convert(intput);

            Assert.Equal(output, result);
        }
    }
}