﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Xunit;

namespace Microsoft.Framework.WebEncoders
{
    public class UnicodeRangeTests
    {
        [Theory]
        [InlineData(-1, 16)]
        [InlineData(0x10000, 16)]
        public void Ctor_FailureCase_FirstCodePoint(int firstCodePoint, int rangeSize)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new UnicodeRange(firstCodePoint, rangeSize));
            Assert.Equal("firstCodePoint", ex.ParamName);
        }

        [Theory]
        [InlineData(0x0100, -1)]
        [InlineData(0x0100, 0x10000)]
        public void Ctor_FailureCase_RangeSize(int firstCodePoint, int rangeSize)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new UnicodeRange(firstCodePoint, rangeSize));
            Assert.Equal("rangeSize", ex.ParamName);
        }

        [Fact]
        public void Ctor_SuccessCase()
        {
            // Act
            var range = new UnicodeRange(0x0100, 128); // Latin Extended-A

            // Assert
            Assert.Equal(0x0100, range.FirstCodePoint);
            Assert.Equal(128, range.Length);
        }

        [Fact]
        public void FromSpan_FailureCase()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => UnicodeRange.Create('\u0020', '\u0010'));
            Assert.Equal("lastChar", ex.ParamName);
        }

        [Fact]
        public void FromSpan_SuccessCase()
        {
            // Act
            var range = UnicodeRange.Create('\u0180', '\u024F'); // Latin Extended-B

            // Assert
            Assert.Equal(0x0180, range.FirstCodePoint);
            Assert.Equal(208, range.Length);
        }

        [Fact]
        public void FromSpan_SuccessCase_All()
        {
            // Act
            var range = UnicodeRange.Create('\u0000', '\uFFFF');

            // Assert
            Assert.Equal(0, range.FirstCodePoint);
            Assert.Equal(0x10000, range.Length);
        }
    }
}
