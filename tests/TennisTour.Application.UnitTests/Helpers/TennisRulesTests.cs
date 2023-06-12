using System.Collections.Generic;
using FluentAssertions;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Helpers;
using Xunit;

namespace TennisTour.Application.UnitTests.Services;

public class TennisRulesTests
{
    private readonly TennisRules _tennisRules;

    public TennisRulesTests()
    {
        _tennisRules = new TennisRules();
    }

    [Fact]
    public void IsSetScoreIsValid_ShouldWorkProperly()
    {
        _tennisRules.IsSetScoreValid(2, 0, null, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(0, 0, 9, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(3, 2, 9, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(7, 6, 2, false).Should().Be(true);
        _tennisRules.IsSetScoreValid(7, 7, null, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(6, 7, null, false).Should().Be(true);
        _tennisRules.IsSetScoreValid(7, 1, null, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(7, 2, 2, false).Should().Be(false);
        _tennisRules.IsSetScoreValid(6, 2, null, true).Should().Be(true);
        _tennisRules.IsSetScoreValid(7, 6, null, true).Should().Be(false);
        _tennisRules.IsSetScoreValid(6, 0, null, true).Should().Be(true);
        _tennisRules.IsSetScoreValid(7, 5, null, true).Should().Be(true);
        _tennisRules.IsSetScoreValid(7, 4, null, true).Should().Be(false);
        _tennisRules.IsSetScoreValid(7, 5, 3, true).Should().Be(false);
    }
}

