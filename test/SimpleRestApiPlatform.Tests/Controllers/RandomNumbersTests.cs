using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleRestApiPlatform.Tests.Controllers
{
    public class RandomNumbersTests
    {
        [Fact]
        public void When_parameterless_GetRandomInteger_is_called()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers_but_min_is_greater_than_max()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_null_or_empty()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_not_an_integer()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_above_intMaxValue()
        { }



        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_null_or_empty()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_not_an_integer()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_below_intMinValue()
        { }
    }
}
