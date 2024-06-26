﻿using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

[ParametersData(
    typeof(IsEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsEqualReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsEqualReturningViewModel(CompareValuesReturningModel<IsEqualReturningCondition> model) : CompareValuesReturningViewModel<IsEqualReturningCondition>(model)
{
}