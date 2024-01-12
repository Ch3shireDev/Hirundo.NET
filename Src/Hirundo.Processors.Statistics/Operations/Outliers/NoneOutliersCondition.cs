using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations.Outliers;

[TypeDescription("None")]
public class NoneOutliersCondition : IOutliersCondition
{
    public bool RejectOutliers => false;
}